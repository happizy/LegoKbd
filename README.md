# LegoKbd

https://github.com/happizy/LegoKbd/assets/61943115/56a38247-d9cf-49f5-ad8f-5a3ccf177c58

### the companion for LEGO + keyboard enjoyers
LegoKbd is a simple Windows application that plays old school lego games building sounds whenever a key is pressed on the keyboard. It runs silently in the background, with no visible user interface.
While not being a garbage collector expert, the use of disposable events prevents the application from using more than a few Mbs of memory.

## Features

- **Silent operation**: No visible UI or console window.
- **Plays sound on key press**: Each key press triggers a sound to play. Around 20 differents sounds randomly played.
- **Global keyboard hook**: Uses low level keyboard hooks to detect keystrokes globally without consuming too much resources.

## Installation

To install LegoKbd on your system, follow these steps:

1. Download the latest release from the [Releases](https://github.com/happizy/LegoKbd/releases) page.
2. Run the `setup.exe` file and follow the installation instructions.
3. You must add the app to your startup folder if you want LegoKbd to always be active when booting up you computer.
4. Once launched, LegoKbd will run silently in the background.

## Usage

LegoKbd requires no user interaction once launched. Simply start typing, and it will play a sound for each key press.

## Credits
LegoKbd uses the following libraries:

- NAudio: For audio playback.
- Inno Setup: For creating the installer

## Development

If you want to contribute to LegoKbd or customize it for your needs, follow these steps:

1. Clone this repository to your local machine. `git clone https://github.com/happizy/LegoKbd.git`
2. Open the project in your preferred IDE (e.g., Visual Studio or Rider).
3. Make your changes or additions.
