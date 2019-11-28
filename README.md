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
+ Added Dependency Injection for .net core 3.0, configured AppSettings for injection
+ Auto-Refreshing SortQueue ListBox contents on detected changes
```

```diff 
- Removed: Static object in App.XAML.cs which held program settings
```

# Coming Soon! v2.0 in development

### Some expected changes
```diff
! Program Settings visible from menu
! Library Settings visible and editable from menu
! Classification Info windows for classified sort media items
! File Info windows for classified and unclassified/non-classifiable media items
! Stylized buttons for UI interface, Cleaned up Main Window data table stylized and interactive
```
