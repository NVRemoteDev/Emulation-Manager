Check out our project website: http://www.hometheatertablet.com/tritium-emulation-manager-steam/

# Tritium Emulation Manager 
The goal of Tritium Emulation Manager is to provide a user-friendly, budget friendly nostalgia gaming experience in the living room or on the go.

The main feature of Tritium Emulation Manager is it allows ROM files to be launched directly from Steam, including and for this project most importantly over streaming like Nvidia Gamestream or Limelight. This will include default button mappings for a TBD core set of emulators, allowing a controller like the Nvidia Shield Controller to be used remotely. This presents a current challenge since the emulators aren't easily configurable over a stream.

Pre-alpha. The entire project is a work in progress.

Please feel free to fork if you want to help!

Inspired by https://github.com/scottrice/Ice

# Controller Configurations
Inside the release folder (or the ControllerConfigurations folder if you're looking at the source) you will find some basic Shield controller configurations.
These will let you use some basic functionality. You may want to customize the file directly, or in the emulator, to your liking.

## ZSNES
zinput.cfg is the Shield controller ZSNES bindings. Put the file in your ZSNES directory. 
Start and select don't work for some reason, so hold start until the bars come up on the top and bottom of the screen.
Then press y to show the keyboard. Enter on the keyboard is start and PgDn or PgUp is select (I can't remember which). 
I'll look into SNES9x and see if that will have better input detection.

The app.config in the project has a better launch option for ZSNES to get it displaying correctly. 
You may have to launch ZSNES first and make your own custom resolution and video options.

## PCSX2
Load the .lily file for Shield controller support

# License
GPLv2 See LICENSE.txt

# Example Functionality
![Steam Games List Picture](http://i.imgur.com/ABagi9C.png)
![Steam Games Tiles Picture](http://i.imgur.com/UoSqv6n.png)
![Steam Big Picture](http://i.imgur.com/JLEBGkY.jpg)

# UI Example (4/17/2015)(WIP)
![UI Picture](http://i.imgur.com/KLmI1iJ.png)
