#!/bin/bash
set -o errexit

# uncomment the line below to get debug output
#set -o xtrace

helpFunction()
{
   echo ""
   echo "Error: Usage: $0 -a applicationPath -e entitlements -i identity -v provisionprofile"
   echo -e "\t-a path to the built app"
   echo -e "\t-i the name of your codesigning identity"
   echo -e "\t-e path to the entitlements file"
   echo -e "\t-v path to the provisioning profile"
   exit 1 # Exit script after printing help
}

while getopts "a:i:e:v:" opt
do
   case "$opt" in
      a ) applicationPath="$OPTARG" ;;
      i ) identity="$OPTARG" ;;
      e ) entitlements="$OPTARG" ;;
      v ) profile="$OPTARG" ;;
      ? ) helpFunction ;; # Print helpFunction in case parameter is non-existent
   esac
done

# Print helpFunction in case parameters are empty
if [ -z "$applicationPath" ] || [ -z "$identity" ] || [ -z "$entitlements" ] || [ -z "$profile" ]
then
   echo "Some or all of the parameters are empty";
   helpFunction
fi

# Begin script in case all parameters are correct
# echo "$applicationPath"
# echo "$identity"
# echo "$entitlements"
# echo "$profile"

echo "Code signing application at '$applicationPath'"

echo "Copying provisioning profile to application directory..."

echo "copying \"$profile\" to \"$applicationPath/Contents/embedded.provisionprofile\""

cp "$profile" "$applicationPath/Contents/embedded.provisionprofile" || { echo "Error: failed to copy provisionprofile into application"; exit 1; }

echo "Delete Unity Meta Files."
find "$applicationPath/Contents/Plugins" -name '*.meta' -print -delete
echo "Finished deleting meta files"

echo "Resigning dylibs..."
for i in $(find $applicationPath/Contents/Frameworks/* -name '*.dylib'); do # Whitespace-safe and recursive
  [ -f "$i" ] || break
   codesign --force --verbose=4 --verify --sign "$identity" --preserve-metadata=identifier,entitlements,flags $i || { echo "Error: failed to codesign $i"; exit 1; }
done

echo "Resigning bundles..."
for i in $(find $applicationPath/Contents/Frameworks/* -name '*.bundle'); do
  [ -f "$i" ] || break
  codesign --force --verbose=4 --verify --sign "$identity" $i | { echo "Error: failed to codesign $i"; exit 1; }
done

echo "Signing application..."
codesign --force --verbose=4 --verify --sign "$identity" --entitlements "$entitlements" "$applicationPath" || { echo "Error: failed to resign the application. the mostly likely cause of this is that your signing identity is not contained in your provisionprofile"; exit 1; }

echo "Resigning Complete"
