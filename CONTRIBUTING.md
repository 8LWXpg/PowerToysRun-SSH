# Contributing

## Adding a New Terminal

Check [`WindowsTerminal.cs`](./SSH/Terminal/WindowsTerminal.cs)

1. Add a new combobox item in `AdditionalOptions` at [Main.cs](./SSH/Main.cs).
1. Create a new class `<TerminalName>.cs` under `./SSH/Terminal`.
1. Add a new terminal name in `TerminalType` enum and add a switch case in `OpenTerminal` under [Terminal.cs](./SSH/Terminal/Terminal.cs).
1. Implement interface `ITerminalHandler` that returns result of `Helper.OpenInShell`.
