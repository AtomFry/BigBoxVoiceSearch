# BigBoxVoiceSearch
BigBoxVoiceSearch is a plug-in for BigBox that enables searching for games using your voice and a microphone via microsoft's speech recognition.  To make use of this plug-in, the plug-in components must be installed to the LaunchBox plugins folder and a few lines of XAML code must be added to the themes where you wish to use it.

## Installation
1.  Download the latest version of BigBoxVoiceSearch.zip from the LaunchBox forums or from this github repositories Releases
2.  Extract BigBoxVoiceSearch.zip to a folder.  Inside the BigBoxVoiceSearch folder is a folder called LaunchBox.  Inside the LaunchBox folder is a folder called Plugins.  Copy the plugins folder
3.  Go to your LaunchBox installation folder and paste the copied folder
4.  To verify the installation - confirm the following files exist in your LaunchBox\Plugins folder
  - BigBoxVoiceSearch.dll
  - System.Speech.dll    
5.  You can delete the downloaded zip file and extracted folder once the files have been copied into your LaunchBox plugins folder

## Adding the voice search element to a theme
In order to use the voice recognition function, a few lines of XAML must be added to the views of the themes that you would like to use this with.  Since it's easy to make simple mistakes while tinkering with XAML, it's strongly recommended to make a copy of whatever theme you plan to use this with and make your changes to a copy of the theme so that you can revert to the original theme if mistakes are made.  Inside your copied theme there is a views folder.  Inside the views folder are xaml files that correspond to the views that you use in BigBox.  Select the view(s) for which you wish to include voice searching functionality and edit them in a text editor like notepad or visual studio.

### XMLNS
Each view starts with a UserControl element.  The user control element will include several lines that start with xmlns.  Add the following line to the user control element along with the other xmlns lines.  
```xaml
xmlns:BigBoxVoiceSearch="clr-namespace:BigBoxVoiceSearch.View;assembly=BigBoxVoiceSearch"
```

### Examples of adding the voice search user control to a theme
Once the above XMLNS line has been added to a views user control element, you can add the BigBoxVoiceSearch:MainWindowView anywhere inside the theme that you would like it to appear.  There are a few properties that you can set on the voice search control to specify how it should behave.  First, here are some examples of how it would look to add the voice search control inside a theme:

```xaml
<!-- Trigger voice recognition with page up, the control is always displayed -->
<BigBoxVoiceSearch:MainWindowView ActivationMode="PageUp" 
                                  ShowInitializing="true"
                                  ShowInitializingFailed="true"
                                  ShowInactive="true"
                                  ShowActive="true"
                                  ShowRecognizing="true"/>
```

```xaml
<!-- Trigger voice recognition with page down, the control is only displayed while recognizing -->
<BigBoxVoiceSearch:MainWindowView ActivationMode="PageDown" 
                                  ShowInitializing="false"
                                  ShowInitializingFailed="false"
                                  ShowInactive="false"
                                  ShowActive="false"
                                  ShowRecognizing="true"/>
```


```xaml
<!-- Activate the user control with Up - you will need to press enter to trigger the voice search, the control is always displayed --> 
<BigBoxVoiceSearch:MainWindowView ActivationMode="Up" 
                                  ShowInitializing="true"
                                  ShowInitializingFailed="true"
                                  ShowInactive="true"
                                  ShowActive="true"
                                  ShowRecognizing="true"/>
```

```xaml
<!-- Trigger voice recognition with page up, the control is always displayed, override default images with theme specific images -->
<BigBoxVoiceSearch:MainWindowView ActivationMode="PageUp"
  ShowInitializing="true" InitializingImagePath="Plugins\BigBoxVoiceSearch\Media\MySpecialTheme\Initializing.png"
  ShowInitializingFailed="true" InitializingFailedImagePath="Plugins\BigBoxVoiceSearch\Media\MySpecialTheme\InitializingFailed.png"
  ShowInactive="true" InactiveImagePath="Plugins\BigBoxVoiceSearch\Media\MySpecialTheme\Inactive.png"        
  ShowRecognizing="true" RecognizingImagePath="Plugins\BigBoxVoiceSearch\Media\MySpecialTheme\Recognizing.png"/>
```

## ActivationMode
By specifying the ActivationMode property on the voice search user control, you can control how the voice search control is activated and how the voice search is triggered with the following options: 

1. Off
- The plug-in is effectively disabled
- No button will activate the user control or trigger speech recognition
2. Up
- The up button will activate the user control
- The down button will deactivate the user control 
- Once activated, press enter to start speech recognition
3. Down
- The down button will activate the user control
- The up button will deactivate the user control 
- Once activated, press enter to start speech recognition
4. Left
- The left button will activate the user control
- The right button will deactivate the user control 
- Once activated, press enter to start speech recognition
5. Right
- The right button will activate the user control
- The left button will deactivate the user control 
- Once activated, press enter to start speech recognition
6. PageUp
- The page up button will trigger voice recognition immediately
- You do not need to press enter after pressing Page Up
- You do not need to press any button to deactivate the user control
7. PageDown
- The page down button will trigger voice recognition immediately
- You do not need to press enter after pressing Page Down
- You do not need to press any button to deactivate the user control

## VisibilityMode
The VisibilityMode property from previous versions has been replaced with individual boolean properties ShowInitializing, ShowInitializingFailed, ShowInactive, ShowActive, and ShowRecognizing.

## ShowInitializing
The ShowInitializing property accepts the values "true" or "false" to indicate whether the user control should be displayed while initializing.  When the view is loaded, there is an intialization period where the titles in the launchbox game library are parsed to create the speech recognition grammar.  Setting this property to true can give a visual indication that the speech recognition functionality is not yet ready to use.  This property defaults to false if not specified on the UserControl.  
  
## ShowInitializingFailed
The ShowInitializingFailed property accepts the values "true" or "false" to indicate whether the user control should be displayed if initialization fails.  Failures could occur while the user control is initializing if there is no default audio device for the speech recognition engine to use.  Setting this property to true can give the indication that speech recognition was not setup successfully and is therefore disabled.  If errors are encountered, check the log.txt file in the LaunchBox\Plugins\BigBoxVoiceSearch folder for any error details.  This property defaults to false if not specified on the UserControl.  

## ShowInactive
The ShowInactive property accepts the values "true" or "false" to indicate whether the user control should be displayed while it is inactive.  The user control will be inactive after initialization completes successfully.  Setting this property to true can give the indication that speech recognition is setup and available to use.  The property defaults to false if not specified on the UserControl.  

## ShowActive
The ShowActive property accepts the values "true" or "false" to indicate whether the user control should be displayed while it is active.  The user control is put in an active state when you press Up, Down, Left, or Right and the ActivationMode property is set to Up, Down, Left, or Right.  Setting this property to true can give the indication that the speech recognition user control is active and will perform a search of enter is pressed.  The property defaults to false if not specified on the UserControl.  

## ShowRecognizing
The ShowRecognizing property accepts the values "true" or "false" to indicates whether the usr control should be displayed while recognizing speech.  The user control recognizes speech when you press Page Up or Page Down and the ActivationMode is set to PageUp or PageDown or when you press Enter while the speech recognition user control is active (if ActivationMode set to Up, Down, Left, or Right).  The property defaults to false if not specified on the UserControl.

## Default image paths 
If no custom image paths are specified on the user control, the plugin will look for images with the following path to display in various states: 

| State              | Default image path                                                          |
|--------------------|-----------------------------------------------------------------------------|
| Initializing       | ..\LaunchBox\Plugins\BigBoxVoiceSearch\Media\Default\Initializing.png       |
| InitializingFailed | ..\LaunchBox\Plugins\BigBoxVoiceSearch\Media\Default\InitializingFailed.png |
| Inactive           | ..\LaunchBox\Plugins\BigBoxVoiceSearch\Media\Default\Inactive.png           |
| Active             | ..\LaunchBox\Plugins\BigBoxVoiceSearch\Media\Default\Active.png             |
| Recognizing        | ..\LaunchBox\Plugins\BigBoxVoiceSearch\Media\Default\Recognizing.png        |

## Custom image paths 
Images displayed by the user control can be overridden or customized by specifying a relative path to the image file on the user control with the following properties: 
- InitializingImagePath
- InitializingFailedImagePath
- InactiveImagePath
- ActiveImagePath
- RecognizingImagePath

## Settings
When BigBox loads for the first time, a settings file will be created that will allow you specify how the plugin should behave.  Currently there is only one setting to configure 

```json
{
  "VoiceSearchTimeoutInSeconds": 5
}
```
### VoiceSearchTimeoutInSeconds
Specify the number of seconds that the voice search should stay open before it stops listening

