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

### Voice Search User Control inside a canvas named "Canvas"
Most themes have a root Canvas element named "Canvas" so the following chunk can be added to the bottom of the theme view just inside the closing canvas tag.  This assumes there is a canvas element named "Canvas": 

```xaml
<!-- VOICE SEARCH PLUGIN -->
<BigBoxVoiceSearch:MainWindowView HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Panel.ZIndex="1000"
                              Height="{Binding ElementName=Canvas, Path=ActualHeight}" 
                              Width="{Binding ElementName=Canvas, Path=ActualWidth}"/>
```

### Voice Search User Control inside a grid
Some themes have a root grid element instead of a canvas so the following chunk can be added inside the grid element.  This assumes a grid that has 5 rows.  

```xaml
<!-- VOICE SEARCH PLUGIN -->
<BigBoxVoiceSearch:MainWindowView Grid.Row="0" Grid.RowSpan="5" Panel.ZIndex="1000"
    HorizontalAlignment="Stretch" VerticalAlignment="Stretch" />
```
### Example - Unified Redux PlatformWheel1FiltersView 
The following example shows the plugin added to the PlatformWheel1FilterView in the Unified Redux theme.  Note the BigBoxVoiceSearch reference near the top and bottom of the theme.
```xaml
<UserControl xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:transitions="clr-namespace:Unbroken.LaunchBox.Windows.Transitions;assembly=Unbroken.LaunchBox.Windows"
             xmlns:coverFlow="clr-namespace:Unbroken.LaunchBox.Windows.Controls.CoverFlow;assembly=Unbroken.LaunchBox.Windows"
      			 xmlns:wpf="clr-namespace:Unbroken.LaunchBox.Windows;assembly=Unbroken.LaunchBox.Windows"
			       xmlns:controls="clr-namespace:Unbroken.LaunchBox.Windows.Controls;assembly=Unbroken.LaunchBox.Windows"
             xmlns:Unified="clr-namespace:Unified.HyperBox;assembly=Unified.HyperBox"
             xmlns:sys="clr-namespace:System;assembly=mscorlib"
             xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
             xmlns:cal="http://www.caliburnproject.org"
             xmlns:BigBoxVoiceSearch="clr-namespace:BigBoxVoiceSearch.View;assembly=BigBoxVoiceSearch"
             mc:Ignorable="d"
             d:DesignHeight="562" d:DesignWidth="1000" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Style="{DynamicResource UserControlStyle}">

<!-- ANIMATIONS -->
    <UserControl.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://siteoforigin:,,,/Themes/Unified Redux/Styles/HorizontalListBoxStyle.xaml" />
            </ResourceDictionary.MergedDictionaries>

		<FontFamily x:Key="FontBebasNeue">/Unified.HyperBox;Component/Fonts/#Bebas Neue</FontFamily>
		<wpf:RemoveNewLineConverter x:Key="NoNewline"/>

	
	<!-- ON PLATFORM CHANGE -->
        <Storyboard x:Key="ChangePlatform">
		
		<!-- POINTER -->
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName="PointerSize" Storyboard.TargetProperty="(ScaleTransform.ScaleX)" >
			    <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="1"/>
				<EasingDoubleKeyFrame KeyTime="0:0:0.15" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:0.3" Value="0.75"/>
			</DoubleAnimationUsingKeyFrames>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName="PointerSize" Storyboard.TargetProperty="(ScaleTransform.ScaleY)" >
			    <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="1"/>
				<EasingDoubleKeyFrame KeyTime="0:0:0.15" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:0.3" Value="0.75"/>
			</DoubleAnimationUsingKeyFrames>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetName="Pointer" Storyboard.TargetProperty="(Image.Opacity)" >
			    <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="1"/>
				<EasingDoubleKeyFrame KeyTime="0:0:0.6" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.9" Value="0"/>
			</DoubleAnimationUsingKeyFrames>

		<!-- FADING WHEEL -->
			<DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.Opacity)" Storyboard.TargetName="FlowControl">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:0.8" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.9" Value="0"/>
            </DoubleAnimationUsingKeyFrames>
			
		<!-- UPPER GLASSBAR TRIANGLE Y-AXIS MOVEMENT -->
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName="TriangleGlassBar">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="-200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.5" Value="-200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:2.0" Value="0"/>
            </DoubleAnimationUsingKeyFrames>

		<!-- CLOCK Y-AXIS MOVEMENT -->
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName="DateTimeWeather">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="-200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.5" Value="-200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:2.0" Value="0"/>
            </DoubleAnimationUsingKeyFrames>					
			
		<!-- LOWER GLASSBAR Y-AXIS MOVEMENT -->
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName="LowerGlassBar">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.5" Value="200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:2.0" Value="0"/>
            </DoubleAnimationUsingKeyFrames>
		
		<!-- PLATFORM TITLE Y-AXIS MOVEMENT -->
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName="PlatformName">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.5" Value="200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:2.0" Value="0"/>
            </DoubleAnimationUsingKeyFrames>	
			
		<!-- SCROLING NOTES Y-AXIS MOVEMENT -->
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName="ScrollingNotes">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:1.5" Value="200"/>
                <EasingDoubleKeyFrame KeyTime="0:0:2.0" Value="0"/>
            </DoubleAnimationUsingKeyFrames>
			
		<!-- FADING PLATFORM TITLE -->
			<DoubleAnimationUsingKeyFrames Storyboard.TargetName="PlatformName" Storyboard.TargetProperty="Opacity" RepeatBehavior="Forever">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:4.0" Value="1"/>
                <EasingDoubleKeyFrame KeyTime="0:0:5.0" Value="0"/>
				<EasingDoubleKeyFrame KeyTime="0:0:11.0" Value="0"/>
				<EasingDoubleKeyFrame KeyTime="0:0:12.0" Value="1"/>
            </DoubleAnimationUsingKeyFrames>
			
		<!-- FADING METADATA -->
			<DoubleAnimationUsingKeyFrames Storyboard.TargetName="Metadata_1" Storyboard.TargetProperty="Opacity" RepeatBehavior="Forever">
                <EasingDoubleKeyFrame KeyTime="0:0:0.0" Value="0"/>
                <EasingDoubleKeyFrame KeyTime="0:0:5.0" Value="0"/>
                <EasingDoubleKeyFrame KeyTime="0:0:6.0" Value="1"/>
				<EasingDoubleKeyFrame KeyTime="0:0:10.0" Value="1"/>
				<EasingDoubleKeyFrame KeyTime="0:0:11.0" Value="0"/>
				<EasingDoubleKeyFrame KeyTime="0:0:12.0" Value="0"/>
            </DoubleAnimationUsingKeyFrames>
							
		</Storyboard>

            </ResourceDictionary>
</UserControl.Resources>			 
			 

<!-- VIEW START -->
	<Canvas Name="Canvas">


	<!-- MAIN GRID -->
		<Grid Height="{Binding ElementName=Canvas, Path=ActualHeight}" Width="{Binding ElementName=Canvas, Path=ActualWidth}">
			<Grid.ColumnDefinitions>
				<ColumnDefinition Width="151*" />
				<ColumnDefinition Width="776*" />
				<ColumnDefinition Width="109*" />
				<ColumnDefinition Width="390*" />
				<ColumnDefinition Width="190*" />
				<ColumnDefinition Width="190*" />
				<ColumnDefinition Width="104*" />
			</Grid.ColumnDefinitions>		
			<Grid.RowDefinitions>
				<RowDefinition Height="315*" />
				<RowDefinition Height="52*" />
				<RowDefinition Height="200*" />
				<RowDefinition Height="149*" />
				<RowDefinition Height="190*" />
				<RowDefinition Height="64*" />
				<RowDefinition Height="60*" />
				<RowDefinition Height="50*" />
			</Grid.RowDefinitions>
      
      <!-- 
      <BigBoxVoiceSearch:MainWindowView Grid.ColumnSpan="7" Grid.RowSpan="8" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Panel.ZIndex="1000" 
                                        Height="{Binding ElementName=Canvas, Path=ActualHeight}" Width="{Binding ElementName=Canvas, Path=ActualWidth}"/>
			-->

		<!-- VIDEO -->
			<transitions:TransitionPresenter Grid.ColumnSpan="7" Grid.RowSpan="8" TransitionSelector="{Binding ImageVideoTransitionSelector}" Content="{Binding ImageVideoView}" IsContentVideo="true" StretchVideo="true" Panel.ZIndex="2" />


		<!-- UPPER GLASSBAR -->
			<Grid Grid.Row="0" Grid.Column="4" Grid.ColumnSpan="3" Panel.ZIndex="15" >
				<Grid.ColumnDefinitions>
					<ColumnDefinition Width="45*" />
					<ColumnDefinition Width="424*" />
					<ColumnDefinition Width="15*" />
				</Grid.ColumnDefinitions>
				<Grid.RowDefinitions>
					<RowDefinition Height="55*" />
					<RowDefinition Height="260*" />
				</Grid.RowDefinitions>
				<Viewbox Grid.Row="0" Grid.Column="0" Grid.ColumnSpan="3" Stretch="UniformToFill" >
					<Polygon x:Name="TriangleGlassBar" Points="0,0 45,55 2000,55 2000,0" Stroke="Black" StrokeThickness="0" Fill="Black" Opacity="0.5" >
						<Polygon.RenderTransform>
							<TransformGroup>
								<ScaleTransform/>
								<SkewTransform/>
								<RotateTransform/>
								<TranslateTransform/>
							</TransformGroup>
						</Polygon.RenderTransform>
					</Polygon>
				</Viewbox>
			
			
			<!-- DATE, TIME AND WEATHER -->        
				<Viewbox x:Name="DateTimeWeather" Grid.Row="0" Grid.Column="1" >
					<DockPanel Height="45" Width="349" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" >            
						<TextBlock Text="{Binding CurrentTime}" FontFamily="{StaticResource FontBebasNeue}" FontWeight="Bold" VerticalAlignment="Center" DockPanel.Dock="Right" FontSize="35" Foreground="Crimson" />
						<TextBlock Text="  " FontFamily="{StaticResource FontBebasNeue}" FontSize="35" VerticalAlignment="Center" DockPanel.Dock="Right" />
						<TextBlock Name="tbArrivalDateTime" Text="{Binding Source={x:Static sys:DateTime.Today}, StringFormat='{}{0:MMMM dd, yyyy}'}" FontFamily="{StaticResource FontBebasNeue}"  VerticalAlignment="Center" HorizontalAlignment="Right" DockPanel.Dock="Right" FontSize="35" Foreground="Gold" />
						<TextBlock Text="  " FontFamily="{StaticResource FontBebasNeue}" FontSize="35" VerticalAlignment="Center" DockPanel.Dock="Right" />
           
					</DockPanel>
					<Viewbox.RenderTransform>
						<TransformGroup>
							<ScaleTransform/>
							<SkewTransform/>
							<RotateTransform/>
							<TranslateTransform/>
						</TransformGroup>
					</Viewbox.RenderTransform>
				</Viewbox>        
      </Grid>

		
		<!-- LOWER GLASSBAR -->			
			<Border x:Name="LowerGlassBar" Grid.Row="6" Grid.RowSpan="2" Grid.Column="0" Grid.ColumnSpan="7" Background="Black" Opacity="0.5" Panel.ZIndex="4" SnapsToDevicePixels="True" >
				<Border.RenderTransform>
					<TransformGroup>
						<ScaleTransform/>
						<SkewTransform/>
						<RotateTransform/>
						<TranslateTransform/>
					</TransformGroup>
				</Border.RenderTransform>
			</Border>
			
			
		<!-- PLATFORM TITLE -->	
			<Viewbox x:Name="PlatformName" Grid.ColumnSpan="7" Grid.Row="6" Panel.ZIndex="20" Margin="25,0,25,5" >
				<DockPanel Height="55" Width="1870" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" >
					<TextBlock Text="{Binding Path=ActivePlatform.Name}" Foreground="Yellow" DockPanel.Dock="Left"  FontFamily="{StaticResource FontBebasNeue}" FontSize="50" TextAlignment="Left" />
				</DockPanel>
				<Viewbox.RenderTransform>
					<TransformGroup>
						<ScaleTransform/>
						<SkewTransform/>
						<RotateTransform/>
						<TranslateTransform/>
					</TransformGroup>
				</Viewbox.RenderTransform>
				<Viewbox.Opacity>1</Viewbox.Opacity>
			</Viewbox>
			
			
		<!-- METADATA -->
			<Viewbox x:Name="Metadata_1" Grid.ColumnSpan="7" Grid.Row="6" Panel.ZIndex="20" Margin="25,0,25,5" >
				<DockPanel Height="55" Width="1870" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" >
					<TextBlock Text="Year: " FontFamily="{StaticResource FontBebasNeue}" FontSize="50" Foreground="Red" DockPanel.Dock="Left" TextAlignment="Left">
						<TextBlock.Style>
							<Style TargetType="TextBlock">
								<Style.Triggers>
									<DataTrigger Binding="{Binding ActivePlatform.ReleaseDate}" Value="{x:Null}">
										<Setter Property="Visibility" Value="Collapsed" />
									</DataTrigger>
								</Style.Triggers>
							</Style>
						</TextBlock.Style>
					</TextBlock>
					<TextBlock Text="{Binding Path=ActivePlatform.ReleaseDate, StringFormat=yyyy}" FontFamily="{StaticResource FontBebasNeue}" FontSize="50" Foreground="White" DockPanel.Dock="Left" TextAlignment="Left" Padding="0,0,30,0">
						<TextBlock.Style>
							<Style TargetType="TextBlock">
								<Style.Triggers>
									<DataTrigger Binding="{Binding ActivePlatform.ReleaseDate}" Value="{x:Null}">
										<Setter Property="Visibility" Value="Collapsed" />
									</DataTrigger>
								</Style.Triggers>
							</Style>
						</TextBlock.Style>
					</TextBlock>
					<TextBlock Text="Manufacturer: " FontFamily="{StaticResource FontBebasNeue}" FontSize="50" Foreground="Red" DockPanel.Dock="Left" TextAlignment="Left">
						<TextBlock.Style>
							<Style TargetType="TextBlock">
								<Style.Triggers>
									<DataTrigger Binding="{Binding ActivePlatform.Manufacturer}" Value="{x:Null}">
										<Setter Property="Visibility" Value="Collapsed" />
									</DataTrigger>
									<DataTrigger Binding="{Binding ActivePlatform.Manufacturer}" Value="">
										<Setter Property="Visibility" Value="Collapsed" />
									</DataTrigger>
								</Style.Triggers>
							</Style>
						</TextBlock.Style>
					</TextBlock>
					<TextBlock Text="{Binding Path=ActivePlatform.Manufacturer}" FontFamily="{StaticResource FontBebasNeue}" FontSize="50" Foreground="White" DockPanel.Dock="Left" TextAlignment="Left" Padding="0,0,30,0">
						<TextBlock.Style>
							<Style TargetType="TextBlock">
								<Style.Triggers>
									<DataTrigger Binding="{Binding ActivePlatform.Manufacturer}" Value="{x:Null}">
										<Setter Property="Visibility" Value="Collapsed" />
									</DataTrigger>
									<DataTrigger Binding="{Binding ActivePlatform.Manufacturer}" Value="">
										<Setter Property="Visibility" Value="Collapsed" />
									</DataTrigger>
								</Style.Triggers>
							</Style>
						</TextBlock.Style>
					</TextBlock>
					<TextBlock Text="Total Games: " FontFamily="{StaticResource FontBebasNeue}" FontSize="50" Foreground="Red" DockPanel.Dock="Left" TextAlignment="Left" />
					<TextBlock Text="{Binding GamesCount}" FontFamily="{StaticResource FontBebasNeue}" FontSize="50" Foreground="White" DockPanel.Dock="Left"  />          
				</DockPanel>
				<Viewbox.RenderTransform>
					<TransformGroup>
						<ScaleTransform/>
						<SkewTransform/>
						<RotateTransform/>
						<TranslateTransform/>
					</TransformGroup>
				</Viewbox.RenderTransform>
				<Viewbox.Opacity>0</Viewbox.Opacity>
			</Viewbox>
		
		
		<!-- HORIZONTAL SCROLLING NOTES -->
			<Viewbox x:Name="ScrollingNotes" Grid.Row="7" Grid.ColumnSpan="7" Margin="30,0,30,5" Panel.ZIndex="20" >
				<DockPanel Height="45" Width="1920" HorizontalAlignment="Stretch" VerticalAlignment="Top" >
					<Canvas Name="ScrollingTextBlockCanvas" ClipToBounds="True" >
						<controls:HorizontalScrollableTextBlock TextWrapping="Wrap" Text="{Binding Path=ActivePlatform.Notes, Converter={StaticResource NoNewline}}" FontFamily="{StaticResource FontBebasNeue}" FontSize="40" Foreground="White" Height="45" ScrollSpeed="95" />
					</Canvas>
				</DockPanel>
				<Viewbox.RenderTransform>
					<TransformGroup>
						<ScaleTransform/>
						<SkewTransform/>
						<RotateTransform/>
						<TranslateTransform/>
					</TransformGroup>
				</Viewbox.RenderTransform>
			</Viewbox>
		
		
		<!-- WHEEL -->
			<coverFlow:FlowControl x:Name="FlowControl" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Grid.Column="3" Grid.ColumnSpan="4" Grid.Row="0" Grid.RowSpan="8" CoverFactory="{Binding CoverFactory}" ImageType="Clear Logo"
				CurveAmount="-2.5" CameraZPosition="3.2" VisibleCount="16" PageSize="6" Spacing="0.9" ItemZPosition="0.7" SelectedItemZPosition="1.1" Panel.ZIndex="20" RotationAmount="12" Margin="100,0,0,0">
                <coverFlow:FlowControl.Opacity>100</coverFlow:FlowControl.Opacity>
				<coverFlow:FlowControl.Effect>
					<DropShadowEffect BlurRadius="10" Direction="-90" RenderingBias="Quality" ShadowDepth="1" />
				</coverFlow:FlowControl.Effect>
            </coverFlow:FlowControl>


		<!-- POINTER -->
			<TextBlock x:Name="ActivePlatformTitle" Visibility="Collapsed" Text="{Binding SelectedParent.Name}" />
			<TextBlock x:Name="PointerFileName" Visibility="Collapsed">
				<TextBlock.Text>
					<MultiBinding StringFormat="{}pack://siteoforigin:,,,/Themes/Unified Redux/Images/Theme/Pointer/{0}.png">
						<Binding ElementName="ActivePlatformTitle" Path="Text" />
					</MultiBinding>
				</TextBlock.Text>
			</TextBlock>
			<Viewbox Grid.Column="4" Grid.ColumnSpan="3" Grid.Row="2" Grid.RowSpan="2" Panel.ZIndex="5" HorizontalAlignment="Right" VerticalAlignment="Center" >
				<Image x:Name="Pointer" Source="{Binding Text, ElementName=PointerFileName, FallbackValue='pack://siteoforigin:,,,/Themes/Unified Redux/Images/Theme/Pointer/_PlatformViews.png'}" Opacity="100" RenderTransformOrigin="1,0.5" RenderOptions.BitmapScalingMode="HighQuality" >
					<Image.RenderTransform>
						<ScaleTransform x:Name="PointerSize" ScaleX="1" ScaleY="1" />
					</Image.RenderTransform>
				</Image>
			</Viewbox>


		<!-- ANIMATION TRIGGER -->	
			<TextBlock x:Name="TriggerPointer" Text="{Binding Path=SelectedPlatform.Name, NotifyOnTargetUpdated=True}" Visibility="Hidden">
                <TextBlock.Triggers>
                    <EventTrigger RoutedEvent="Binding.TargetUpdated">
                        <BeginStoryboard Storyboard="{StaticResource ChangePlatform}"/>
                    </EventTrigger>
                </TextBlock.Triggers>
            </TextBlock>
			
		</Grid>


	<!-- ALPHANUMERIC BAR -->
        <ListBox Name="Index" Style="{DynamicResource HorizontalListBoxStyle}" Width="{Binding ElementName=Canvas, Path=ActualWidth}" Visibility="{Binding IndexVisibility}">
            <i:Interaction.Triggers>
                <i:EventTrigger EventName="MouseDoubleClick">
                    <cal:ActionMessage MethodName="OnEnter" />
                </i:EventTrigger>
            </i:Interaction.Triggers>
        </ListBox>

    <!-- VOICE SEARCH PLUGIN -->
      <BigBoxVoiceSearch:MainWindowView HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Panel.ZIndex="1000"
                                        Height="{Binding ElementName=Canvas, Path=ActualHeight}" 
                                        Width="{Binding ElementName=Canvas, Path=ActualWidth}"/>
    </Canvas>
</UserControl>
```

## Settings
When BigBox loads for the first time, a settings file will be created that will allow you specify how the plugin should select a game to launch on startup.  If changes to the default settings are desired, make the desired changes and restart BigBox.  The default settings look like this: 

```json
{
  "SearchOnPageUp": true,
  "SearchOnPageDown": false,
  "VoiceSearchTimeoutInSeconds": 5
}
```
### SearchOnPageUp
Set this to true if you want to trigger the voice search function on the page up button

### SearchOnPageDown
Set this to true if you want to trigger the voice search function on the page down button

### VoiceSearchTimeoutInSeconds
Specify the number of seconds that the voice search should stay open before it stops listening

