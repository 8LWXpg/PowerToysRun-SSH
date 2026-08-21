# Changelog

## 1.10.2

- Fixed edge case where config edit changes `Include`.
- Dispose `FileWatcher` and cache in `Dispose`.

## 1.10.1

- Circular include prevention.
- Dispose pattern.

## 1.10.0

- Added ru-RU localization.

## 1.9.0

- Added options to suppress title change, only use this if you don't want to enable global settings (only support Windows Terminal for now).

## 1.8.0

- Support opening session in WezTerm.

## 1.7.0

- Added uk-UA localization.

## 1.6.0

- Combined settings into one combobox.
- Set window title of the session.

## 1.5.0

- Fixed version in dll.

## 1.4.0

- Add `-w 0` when launch with `New Tab` option.
- Bumped to .NET 9.

## 1.3.0

- Support for `Include` keyword.
- Fixed comment in config.

## 1.2.1

- Use file watcher, only update on config file change.

## 1.2.0

- Added tooltip.

## 1.1.0

- Support glob matching on host name.

## 1.0.2

- Fixed release script.

## 1.0.1

- Fixed host name RegEx.

## 1.0.0

- First release.
