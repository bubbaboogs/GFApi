## GFApi is an API for BepInEx to easily create mods for the game Gelli Fields. ##
It's still in development, so expect breaking changes and check for new updates frequently.



## How to build: ##

1. Clone the repository
2. Open the repository in VSCode
3. Open the terminal and run "dotnet restore"
4. Run "dotnet build" to build it, then go to "bin/Debug/netstandard2.1" and find GFApi.dll
5. You've built GFApi!!


## How to use GFApi in your mod: ##
1. Download or build GFApi.dll
2. In your BepInEx plugin's project, find PluginName.csproj (PluginName is the name of your plugin)
3. Add this code next to the Assembly-CSharp reference: `<Reference Include="GFApi">
      <HintPath>Libraries/GFApi.dll</HintPath>
    </Reference>`
4. Put GFApi.dll in a folder called Libraries inside of your project
5. Now you can use GFApi!
