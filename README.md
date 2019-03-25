# Another UnitySerial Project

I know there are many of these in existence, but I've tried a bunch and they do not work. This C# script detected my serial port automatically and allowed my Arduino/Teensy based hardware to communicate with the Unity environment through the serial port.  You can change the baud rate in Unity's inspector. 

This code was originally created for use with Erin Gee's [biodataDuo](https://github.com/eringee/biodataDuo) Arduino-based hardware system for measuring physiological markers of emotion.

# WINDOWS SYSTEM 

This code was made in order to avoid the Unity bug wherein the serial chokes in Windows OS.
It has been tested thus far on MacOS 10.14.3 Mojave. 

# UNITY

This code was tested originally in Unity 2017.3.1 

# SET UP

1. Make sure that the correct platform is selected in File | Build Settings: "PC, Mac & Linux standalone".

2. Go to Edit | Project Settings | Player | Other Settings | API Compatibility Level and select ".Net 2.0"

In some older version of Unity, you would find this option in: File | Build Settings | Optimization | API Compatibility Level: .Net 2.0

3. Create an empty GameObject and add the SerialBridge script to the object. 

4. Verify the name of the serial port you would like to access and update the public variable field "Port" on your object.

5. Verify your baudrate and modify the public variable input field "Baudrate"

# DATA AGREEMENT

You may add, rename, and change the type of variable as you like, so long as they correspond to tab separated values in your Arduino serial output.

# Error CS0234 Ports does not exist in the namespace

You may at some point get the following error:

`error CS0234: The type or namespace name 'Ports' does not exist in the namespace 'System.IO'. Are you missing an assembly reference?`

Solution:

You need to enable .Net 2.0 in the project settings.  Sometimes this resets when you install a new version of Unity.  Just go click the box again.

# AUTHORS

* [Sofian Audry](http://sofianaudry.com/en)
* [Erin Gee](https://eringee.net)

Code for automatically populating the serial port has been adapted from [abstractmachine's](https://github.com/abstractmachine) project [Unity Serial Port](https://github.com/abstractmachine/UnitySerialPort)

# COPYING
Released under GNU GPL 3.0 License.  Copyright Sofian Audry and Erin Gee 2017.
