# BigBoxVoiceSearch
BigBoxVoiceSearch is a plug-in for BigBox that enables searching via speech recognition.  Because this plug-in is a BigBox theme element, to make use of it, you must install the plug-in components as well as add a few lines of XAML to the views of the theme that you wish to use this plug-in with.  

## Installation
1.  Download BigBoxVoiceSearch.zip from the LaunchBox forums or from this github repositories Releases
2.  Extract BigBoxVoiceSearch.zip to a folder.  Inside the BigBoxVoiceSearch folder is a folder called LaunchBox.  Inside the LaunchBox folder is a folder called Plugins.  Copy the plugins folder
3.  Go to your LaunchBox installation folder and paste the copied folder
4.  To verify the installation - confirm the following files exist in your LaunchBox\Plugins folder
  - BigBoxVoiceSearch.dll
  - System.Speech.dll
  - WpfAnimatedGif.dll
5.  You can delete the downloaded zip file and extracted folder

## Adding the voice search element to a theme
In order to use the voice recognition function, a few lines of XAML must be added to whatever theme views you would like to use this with.  Since it's easy to make simple mistakes while tinkering with XAML, it's strongly recommended to make a copy of whatever theme you plan to use this with and make your changes to a copy of the theme so that you can revert to the original theme if mistakes are made.  

### XMLNS
Add the following line to the user control element at the top of the view.  This tells the theme that we want to use the voice search user control somewhere in our theme.
```xaml
xmlns:BigBoxVoiceSearch="clr-namespace:BigBoxVoiceSearch.View;assembly=BigBoxVoiceSearch"
```

### Examples of adding the voice search user control to a theme
Once the above XMLNS line has been added to a views user control element, you can add the BigBoxVoiceSearch:MainWindowView anywhere inside the theme that you would like it to appear.  There are a few properties that you can set on the voice search control to specify how it should behave.  First, here are some examples of how it would look to add the voice search control inside a theme:

```xaml
<!-- Trigger voice recognition with page up, the control is always displayed -->
<BigBoxVoiceSearch:MainWindowView ActivationMode="PageUp" VisibilityMode="Always"/>
```

```xaml
<!-- Trigger voice recognition with page down, the control is only displayed while recognizing -->
<BigBoxVoiceSearch:MainWindowView ActivationMode="PageDown" VisibilityMode="Recognizing"/>
```

```xaml
<!-- Activate the user control with Up - you will need to press enter to trigger the voice search, the control is always displayed --> 
<BigBoxVoiceSearch:MainWindowView ActivationMode="Up" VisibilityMode="Always"/>
```

## VisibilityMode
By specifying the VisiblityMode property on the voice search user control, you can control when the control is visible in your theme with the following options: 

- Always - always show the user control
- Active - show the user control when it has been activated (and while recognizing)
- Recognizing - show the user control only while recognizing

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

## Media Folder
A media folder is included in the plug-in directory under ..\LaunchBox\Plugins\BigBoxVoiceSearch\Media. A few sample icons are included with the plugin.  You can replace the images with the following names and they will appear in your theme when the plugin is in use:

- Active.png - appears in the foreground when the user control is active
- ActiveBackground.png - appears in the background when the user control is active
- Recognizing.png - appears in the foreground when the user control is recognizing speech
- RecognizingBackground.png - appears in the background when the user control is recognizing speech
- Inactive.png - appears in the foreground when the user control is inactive
- InactiveBackground.png - appears in the background when the user control is inactive

## Settings
When BigBox loads for the first time, a settings file will be created that will allow you specify how the plugin should behave.  Currently there is only one setting to configure 

```json
{
  "VoiceSearchTimeoutInSeconds": 5
}
```
### VoiceSearchTimeoutInSeconds
Specify the number of seconds that the voice search should stay open before it stops listening

