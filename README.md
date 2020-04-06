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
 
This plugin is provided as a unity package. You can import via the Package Manager. 
 
## Installing the Unity Package
 
### From Disk
* Clone or download the git project. If you downloaded the project as a zip, unzip it somewhere on your filesystem.
* Open "Window->Package Manager"
* Click the "+" button in the upper left and select "Add Package From Disk"
* Select the "package.json" file in the "CloudKit" folder of inside the unzipped directory
* Unity will now import the package into your project
 
### From Git URL
* You can also import the package from it's git URL: "https://github.com/nullObjectPtr/hovelhouse-cloudkit.git"
* You can find instructions on how to do this here: https://docs.unity3d.com/Manual/upm-git.html
 
There are three libraries provided. One dynamic library for MacOS, one static library for iOS, and one static library for TVOS.
 
## Usage
To use, import the namespace "HovelHouse.CloudKit". No plugin initialization is needed and you do not need to add anything to your scenes. Just start using the classes as you would if this were an objective-c project. Class names and methods very closely match their Objective-C counterparts. See the provided examples for details.
 
## Examples
The plugin comes with some examples that you can run to validate everything is working as intended. Nothing fancy here, just some code and logs to the screen. Add ExampleHub, and examples one through six to you build scenes list to run them on device. The examples will not run in the editor. 
 
## Building
In order to use cloudkit you will need the following
* An active (as in, paid, up to date and not suspended) account with the apple developer program. You will not be able to add the appropriate CloudKit capability to your project without one. Attempting to run your build without it can result in confusing error messages
* Have set a valid bundle identifier in "Player Settings->Other Settings". Being able to sign your app is a requirement since cloud-kit containers are included in your provisioning profile
* Have set the "Target Minimum iOS Version" to 11.0 or higher
 
### iOS and TVOS
Before you build you want to make sure you have set a good bundle identifier in Unity settings. Once you get to the step where you add the CloudKit capability, xcode will automatically generate a container identifier you **cannot** delete. Having set the bundle identifier you want now will save you the pain of having your cloudkit dashboard junked up with a bunch of test container id's. You can read more about containers here: https://developer.apple.com/library/archive/documentation/DataManagement/Conceptual/CloudKitQuickStart/EnablingiCloudandConfiguringCloudKit/EnablingiCloudandConfiguringCloudKit.html 
 
 * The plugin adds the appropriate CloudKit entitlements as a post process build step. On first launch a BuildSettings asset will be created in the folder "Assets/Plugins/HovelHouse/CloudKit/Resources" with default options.
 ** By default, key-value storage and iCloud Documents are disabled.
 ** The default container is automatically added, but can be disabled
 ** You can add custom containers here
 ** If you have your own post process build scripts, and this conflicts with that, you can disable this step by unchecking the "Enable Post Process Build" checkbox.
 
### MacOS
* To build to macOS follow the instructions at http://www.hovelhouse.com/building_for_macos.html
 
# Known Issues
* Again, some of the API isn't yet covered
* When Key-Value-Storage is enabled, some versions of Unity will cause Unity Cloud Build to fail with an error message about the entitlements not matching the provisions profile. The cause of this is currently unknown, as the entitlements are the same in all build versions.
 
# FAQ
* No questions yet. Be the first! Send an e-mail to support@hovelhouse.com
 
# Road Map
 
### P1
* Get continuous integration up and running with UnityCloudBuild
* Unit tests for everything
* Better documentation
* Tutorials

### P2
* Use weak references for storing property callbacks
* Reuse existing C# instances instead of creating new when possible
* Support field arrays in CKRecord
* Remove the type-specific Set(Type)ForKey and replace them with overloaded versions of SetObjectForKey (Breaking Change)
* Array support for CKRecord's setObject forKey methods
 
### P3
* Better code examples
 
### P4
* Add classes / methods not supported on TVOS, and conditionally compile them out of TVOS
* ? you let me know
* CloudSaveManager? - a save system that helps with cloud saves and conforms to apple's save paradigm
