## HotDecalPluginTemplate
This is a decal plugin template with hot code reloading.  Any time the plugin changes on disk it will automatically be reloaded ingame. This plugin requires the [Virindi View System](http://virindi.net/wiki/index.php/Virindi_Views) to work properly.

### Setup
1. Download / Clone this repository.
2. Open up `HotDecalPluginTemplate.sln`.
3. Update guids in both `HotDecalPluginLoader\Properties\AssemblyInfo.cs` and `HotDecalPlugin\Properties\AssemblyInfo.cs`.  You can find an online guid generator [here](https://www.guidgenerator.com/).
4. Build solution and add `bin\HotDecalPluginLoader.dll` to decal.

### Additional Info
Most if not all of your edits will take place in the `HotDecalPlugin` project.

**Important Note**: When the plugin is reloaded (when you are already ingame) it will miss decal events like Login.  This can be important if your plugin relies on events to setup state.

`HotDecalPluginLoader` is loaded as a network filter, this means it is started right when ac client is started (even before logging in to a character).  It listens for decal's `Core_PluginInitComplete` event to load the actual plugin assembly (this happens as soon as you enter world).  Once in-game, it will monitor the file system for changes to `HotDecalPlugin.dll` and will reload it as required.

### Renaming your plugin
1. Update the `FriendlyName` class decorator at the top of `HotDecalPluginLoader`.
2. You will need to update a few properties in `HotDecalPluginLoader`, namely `PluginAssemblyNamespace` and `PluginAssemblyName`.
3. If you change namespaces, you will also need to update the `HotDecalPlugin.CreateView` method to load the view from the proper namespace.