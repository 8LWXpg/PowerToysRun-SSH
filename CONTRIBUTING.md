# Contributing

## Adding a New Terminal

1. Add a New Combobox Item in `AdditionalOptions` within [`Main.cs`](./SSH/Main.cs)
1. Create a new class named `<TerminalName>.cs` under `./SSH/Terminal`.
1. Implement interface `ITerminalHandler` that returns result of `Helper.OpenInShell`, check [`WindowsTerminal.cs`](./SSH/Terminal/WindowsTerminal.cs).
1. Add a new terminal name in `TerminalType` enum and add a switch case in `OpenTerminal` under [`Terminal.cs`](./SSH/Terminal/Terminal.cs).
