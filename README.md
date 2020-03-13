# Hello
Thank you for downloading the Hovel House CloudKit Plugin
 
The plugin was written specifically with Apple Arcade in mind. It is a thin wrapper over the CloudKit framework and made to work on MacOS, iOS and TVOS. It was written as an alternative to Prime31's and Stan's Assets CloudKit plugins which do not offer full access to the CloudKit API.
 
If you got your cloud saves working with either of those plugins and didn't need access to things like CKRecord's ChangeToken then good on you! But if you were aggravated that you didn't have access to more advanced functions than this plugin is for you. It's aim is to be as close as possible to writing the equivalent in objective-c and makes very few assumptions and how you intend to use it. It simply marshals your data to the appropriate API calls.
 
The CloudKit plugin is a work in progress. While the majority of the API is covered. Some of it is not yet (some things are more difficult to marshal than others). These will become available shortly. I welcome your feedback on what to prioritize.
 
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
Before you build you want to make sure you have set a good bundle identifier in Unity settings. Once you get to the step where you add the CloudKit capability xcode will automatically generate a container identifier you **cannot** delete. Having set the bundle identifier you want now will save you the pain of having your cloudkit dashboard junked up with a bunch of test container id's. You can read more about containers here: https://developer.apple.com/library/archive/documentation/DataManagement/Conceptual/CloudKitQuickStart/EnablingiCloudandConfiguringCloudKit/EnablingiCloudandConfiguringCloudKit.html 
 
 * The plugin adds the appropriate CloudKit entitlements as a post process build step. On first launch a BuildSettings asset will be created in the folder "Assets/Plugins/HovelHouse/CloudKit/Resources" with default options.
 ** By default, key-value storage and iCloud Documents are disabled.
 ** The default container is automatically added, but can be disabled
 ** You can add custom containers here
 ** If you have your own post process build scripts, and this conflicts with that, you can disable this step by unchecking the "Enable Post Process Build" checkbox.
 
### MacOS
* If you've made a MacOS build yet, then you already know that unity doesn't support exporting to an XCode project. It is therefore recommended that you first create an iOS or TVOS build before attempting to create a MacOS build. By doing so, xcode will automatically create the appropriate CloudKit container for your app, and you will not need to create one manually on the apple developer website (which is a big pain)
* Unity's inability to generate an xcode project for macOS causes problems for apps that require custom entitlements to run. Sadly, CloudKit is a framework that requires the appropriate entitlements. Attempting to execute a cloud kit api method without them will result in your application exiting with an error. In order to use this plugin on your MacOS build, you'll need to use a command line program to insert the appropriate entitlements and re-sign your app.
* TODO: specific details about how to do this
* TODO: a tool or script to help with this
 
# Known Issues
Again, some of the API isn't yet covered.
 
# FAQ
No questions yet. Be the first!
 
# Road Map
 
### P1
- Fix compile warnings

### P2
* Get continuous integration up and running with UnityCloudBuild
* Use weak references for storing property callbacks
* Reuse existing C# instances instead of creating new when possible
* Support field arrays in CKRecord
* Remove the type-specific Set(Type)ForKey and replace them with overloaded versions of SetObjectForKey (Breaking Change)
* Array support for CKRecord's setObject forKey methods
* Figure out where example scenes are supposed to live, if you're not supposed to have scenes in packages.
 
### P3
* Support website
* Unit tests for everything
* Better code examples
* Tutorials
* Automated documentation
 
### P4
* Add classes / methods not supported on TVOS, and conditionally compile them out of TVOS
* ? you let me know
* CloudSaveManager? - a save system that helps with cloud saves and conforms to apple's save paradigm
