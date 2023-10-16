# CS_OnReady Plugin for Godot

This plugin provides an extension to Godot's functionality, emulating the `@onready` directive from GDScript within C# scripts.

## Installation

1. Clone this repository or download the ZIP.
2. Copy the `cs_onready_plugin` folder into your `addons` directory in your Godot project.

## Usage

To use the plugin:

1. Ensure the plugin is active.
2. In your C# script, add the necessary `OnReadyCs` namespace at the top.
3. In your C# script, use the `[OnReady("Path/To/Your/Node")]` attribute before any field you wish to initialize using the specified node.
4. In your script's `_Ready()` method, call `this.InitializeOnReadyFields();`.

Example:

First, add the necessary `using` directive:

```csharp
using Godot;
using OnReadyNameSpace;
```

Then, in your script:

```csharp
[OnReady("Path/To/SomeNode")]
private SomeNodeType myNode;

public override void _Ready()
{
    this.InitializeOnReadyFields();
    // Now, myNode is initialized and ready to use.
}
```

## Contributing

1. **Fork** the repository on GitHub.
2. **Clone** the forked repository to your local machine.
3. **Commit** your changes to your fork.
4. **Push** your work back up to your fork on GitHub.
5. Submit a **Pull request** so that we can review your changes.

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
