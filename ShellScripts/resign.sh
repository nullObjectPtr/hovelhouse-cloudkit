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

echo "Script executed from: ${PWD}"

echo "Code signing application at '$applicationPath'"

echo "Copying provisioning profile to application directory..."

echo "copying \"$profile\" to \"$applicationPath/Contents/embedded.provisionprofile\""

cp "$profile" "$applicationPath/Contents/embedded.provisionprofile" || { echo "Error: failed to copy provisionprofile into application"; exit 1; }

echo "Delete Unity Meta Files."
find "$applicationPath/Contents/Plugins" -name '*.meta' -print -delete
echo "Finished deleting meta files"

echo "Resigning dylibs..."
find "$applicationPath/Contents/Frameworks/" -name '*.dylib' | while read line; do
  echo "signing -> $line";
  codesign --force --verbose=4 --verify --sign "$identity" --preserve-metadata=identifier,entitlements,flags $line || { echo "Error: failed to codesign $line"; exit 1; }
done

echo "Resigning bundles..."
find "$applicationPath/Contents/Frameworks/" -name '*.bundle' | while read line; do
  echo "signing -> $line";
  codesign --force --verbose=4 --verify --sign "$identity" $line || { echo "Error: failed to codesign $line"; exit 1; }
done

find "$applicationPath/Contents/Plugins/" -name '*.bundle' | while read line; do
  echo "signing -> $line";
  codesign --force --verbose=4 --verify --sign "$identity" $line || { echo "Error: failed to codesign $line"; exit 1; }
done

echo "Signing application..."
codesign --force --verbose=4 --verify --sign "$identity" --entitlements "$entitlements" "$applicationPath" || { echo "Error: failed to resign the application."; exit 1; }

echo "Resigning Complete"
