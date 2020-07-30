# Hello
Thank you for downloading the Hovel House CloudKit Plugin
 
The plugin was written specifically with Apple Arcade in mind. It is a thin wrapper over the CloudKit framework and made to work on MacOS, iOS and TVOS. It's aim is to be as close as possible to writing the equivalent in objective-c and makes very few assumptions and how you intend to use it. It simply marshals your data to the appropriate API calls.
 
The CloudKit plugin is a work in progress. While the majority of the API is covered. Some of it is not yet (some things are more difficult to marshal than others). These will become available shortly. I welcome your feedback on what to prioritize.

# Support
## Forums
Support is handled mainly via the forums: http://www.hovelhouse.com/forums but you are welcome to send an e-mail directly to us at support@hovelhouse.com

## Documentation
Setup and installation is covered in this document. Some rudimentary web documentation for the API is available here: http://www.hovelhouse.com/docs. It's thin on explanations, but it does provide an outline of which API methods are currently covered. For the moment, if you need explanations of what the various API methods do please refer to the apple documentation: https://developer.apple.com/documentation/cloudkit?language=objc By and large, the method names and parameters will be the same. 
 
# Setup
 
 IMPORTANT: This plugin is provided as a Unity Package Manager (UPM) package. It must be placed in the /packages directory for it to run correctly. It will NOT install correctly if you copy it into your assets folder.
 
## Installing the Unity Package

* Unzip the archive and place the entire directory in the packages directory (this must be done from the finde0)

OR

* Move the archive to a folder outside of your unity project and unzip it.
* Open "Window->Package Manager"
* Click the "+" button in the upper left and select "Add Package From Disk"
* Select the "package.json" file in the "CloudKit" folder of inside the unzipped directory
* Unity will now import the package into your project
 
 There are three libraries provided. One dynamic library for MacOS, one static library for iOS, and one static library for TVOS.
 
 ## Samples
 The provided example scenes illustrate the many uses of CloudKit. We recommend that you test your build with these scenes to ensure that you have everything set up correctly. 
 
 * In Unity 2019 and later, samples can be imported from the details panel for this plugin, inside the Package Manager window
 * In Unity 2018 the option to import samples is missing from the package manager UI. They can be found in the /CloudKitPlugin/Samples~/ directory. Copy the entire contents of this folder (including the meta files) to your assets folder. The '~' in the hides this folder from the Unity project explorer window, so you must do this from the finder.
 
## Usage
To use, import the namespace "HovelHouse.CloudKit". No plugin initialization is needed and you do not need to add anything to your scenes. Just start using the classes as you would if this were an objective-c project. Class names and methods very closely match their Objective-C counterparts. See the provided examples for details.
 
## Examples
The plugin comes with some examples that you can run to validate everything is working as intended. Nothing fancy here, just some code and logs to the screen. Add ExampleHub, and examples one through six to you build scenes list to run them on device. The examples will not run in the editor. Samples can be imported via the package manager in Unity 2019+.  2018 users should navigate to the Packages/com.hovelhouse.cloudkit/Samples~ folder on the filesystem (the folder is hidden in editor) and copy the directory into your assets director. Be sure to copy the meta files as well. 

In order to run example 7 - Key Value Storage - make sure that you enable Key Value Storage in the build settings. It is not enabled by default. 

## Building

### Required Build Settings - iOS

There are a few required build settings. Open "Player Settings -> Other Settings" and enter or set the following fields in the inspector:

Set *Target Minimum IOS Version" to  11
Set your "Signing Team ID" - this value is used by the MacOS post process build script to do codesigning from the command-line

Recommended:
It's also recommended that you set your bundle identifier, as your apps default container id is derived from this value.
 
### Plugin Settings
The plugin will create a config asset with various settings. These settings are used by the Post Process Build script to add the appropriate settings to the XCode project (if building for iOS or TVOS) or the Standalone Binary (if building for MacOS)

 ** By default, key-value storage and iCloud Documents are disabled.
 ** The default container is automatically added, but can be disabled
 ** You can add custom containers here
 ** If you have your own post process build scripts, and this conflicts with that, you can disable this step by unchecking the "Enable Post Process Build" checkbox.
 
### MacOS
* To build to macOS please follow the instructions at http://www.hovelhouse.com/building_for_macos.html
 
# Known Issues
* Some of the API isn't yet covered
* When Key-Value-Storage is enabled, some versions of Unity will cause Unity Cloud Build to fail with an error message about the entitlements not matching the provisions profile. The cause of this is currently unknown, as the entitlements are the same in all build versions.
 
# FAQ
* No questions yet. Be the first! Send an e-mail to support@hovelhouse.com
 
# Road Map
 
### P1
* Better documentation
* Integration tests for everything
* Video Tutorials

### P2
* A save system layer that conforms to apple arcades save paradigm
* Support array fields in CKRecord
* Remove the type-specific Set(Type)ForKey and replace them with overloaded versions of SetObjectForKey (Breaking Change)
 
### P3
* Add classes / methods not supported on TVOS, and conditionally compile them out of TVOS
