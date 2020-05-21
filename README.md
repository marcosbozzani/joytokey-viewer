# joytokey-viewer
A viewer for JoyToKey - https://joytokey.net/en/

## Requirements
Powershell 5+ - https://aka.ms/powershell

## Usage
1. Clone or download this repository
2. Open Powershell and navigate to the downloaded folder
3. Run `joystick $profile $config` where:
  - `$profile = profile name inside joysticks folder (e.g. dualshock)`
  - `$config = path to joytokey configuration file (e.g. c:\joytokey\game.cfg)`

## Options
- You can change the settings for a profile or create a new one
- A profile is defined by 3 files inside `.\joystick` folder: `profile.map`, `profile.ini` and `profile.png`
- `.\joysticks\profile.map`: buttons names
- `.\joysticks\profile.ini`: buttons positions
- `.\joysticks\profile.png`: background image
- You move the buttons label texts with the mouse and they're positions are automatically persisted to `.\joysticks\profile.ini`
