# Media Manager Toolset

### Program Summary:
* Renaming Files to match proper media filename format<br>
* Classifying files after download completion<br>
* Prioritize certain media over others<br>
* Automate all aspects from file download completion to adding items to media libraries

# v1.5 is available!

### Changes made in V1.5:
```diff  
+ Added appsettings.json configuration file to handle static values such as media library locations, etc.
```
```diff  
+ Added Dependency Injection for .net core 3.0, configured AppSettings for injection
```
```diff  
+ Auto-Refreshing SortQueue ListBox contents on detected changes
```

```diff 
- Removed: Static object in App.XAML.cs which held program settings
```

# v2.0 is now the latest version!

### What v2.0 brings to the table
* Program Settings visible from menu
>Clicking on Settings -> Program Settings will bring up all available settings to the user within the UI.  Settings are loaded on startup from appsettings.json. Changes can be made to user interface and themes, as well as specific user settings such as automatic login, etc.

* Library Settings visible and editable from menu
>View all user libraries and their contents in an easy simple form.  Users can also manage library contents and file storage options, as well.

* Stylized buttons for UI interface, Cleaned up Main Window data table stylized and interactive
>We've been busy adding new features for you guys!  But now our user interface could definitely use a facelift.  UI styling will become a major part of v2 in the second half of the lifecycle, meaning prettier buttons and layouts for the user!

### Changes made in V2.0: - lots of Dependency Injection changes!
```diff  
+ Improvements to Dependency Injection for .net core 3.0.
```
```diff 
+ Fixed/Added: Removed Class based DI containers and moved to App.XAML with public service locater
```
```diff 
+ Added: DI appsettings now available in all classes
```

#v2.5 in the works..
* Classification Info windows for classified sort media items
>The Classification button within the sort selection details will finally come to life!  This will give users a look at the media classification type, as well as in a future release we will add the ability to view Show information and metadata on this window.

* File Info windows for classified and unclassified/non-classifiable media items
>Show all sorts of file and disk information about a sort selection, more info to come..
