# SSH Plugin for PowerToys Run

A [PowerToys Run](https://aka.ms/PowerToysOverview_PowerToysRun) plugin for connecting to SSH clients that configured at `~/.ssh/config` file.

Checkout the [Template](https://github.com/8LWXpg/PowerToysRun-PluginTemplate) for a starting point to create your own plugin.

## Features

### Open SSH connection

![screenshot](./assets/screenshot.png)

## Settings

### Open mode

Controls how a new terminal session is opened. Note that certain terminals may not support all opening modes. For unsupported modes, the terminal will revert to the default mode, as listed below:

- WezTerm: Quake

## Installation

### Manual

1. Download the latest release of the from the releases page.
2. Extract the zip file's contents to `%LocalAppData%\Microsoft\PowerToys\PowerToys Run\Plugins`
3. Restart PowerToys.

### Via [ptr](https://github.com/8LWXpg/ptr)

```shell
ptr add SSH 8LWXpg/PowerToysRun-SSH
```

## Usage

1. Open PowerToys Run (default shortcut is <kbd>Alt+Space</kbd>).
2. Type `ssh`.

## Building

1. Clone the repository and the dependencies in `/lib` with `SSH/copyLib.ps1`.
2. run `dotnet build -c Release`.

## Debugging

1. Clone the repository and the dependencies in `/lib` with `SSH/copyLib.ps1`.
2. Build the project in `Debug` configuration.
3. Make sure you have [gsudo](https://github.com/gerardog/gsudo) installed in the path.
4. Run `debug.ps1` (change `$ptPath` if you have PowerToys installed in a different location).
5. Attach to the `PowerToys.PowerLauncher` process in Visual Studio.

## Contributing

### Localization

If you want to help localize this plugin, please check the [localization guide](./Localizing.md)

### Support other Terminal

Check [`CONTRIBUTING.md`](./CONTRIBUTING.md)