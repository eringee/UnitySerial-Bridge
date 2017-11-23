# A UnitySerial Bridge for biodataDuo

This C# script will allow your biodataDuo hardware to communicate with your Unity environment through the serial port.  You can change the baud rate and port name either in the script itself or in Unity's inspector. 

This code was originally created for use with Erin Gee's [biodataDuo](https://github.com/eringee/biodataDuo) Arduino-based hardware system for measuring physiological markers of emotion.

# WINDOWS SYSTEM 

This code was made in order to avoid the Unity bug wherein the serial chokes in Windows OS

# UNITY

This code was tested originally on Unity 2017.2.0f3

# SET UP

1. Make sure that the correct platform is selected in File | Build Settings: "PC, Mac & Linux standalone".

2. Go to Edit | Project Settings | Player | PC, Mac & Linux Standalone settings | Other Settings | Optimization | API Compatibility Level and select ".Net 2.0"

In some older version of Unity, you would find this option in: File | Build Settings | Optimization | API Compatibility Level: .Net 2.0

3. Create an empty GameObject and add the SerialBridge script to the object. 

You will see that there are several public variables that correspond to your incoming Arduino data.  You may add, rename, and change the type of variable as you like.

# Error CS0234 Ports does not exist in the namespace

You may get the following error:

`error CS0234: The type or namespace name 'Ports' does not exist in the namespace 'System.IO'. Are you missing an assembly reference?`

Solution:

You need to enable .Net 2.0 in the project settings.  Sometimes this resets when you install a new version of Unity.  Just go click the box again.

# AUTHOR

* [Sofian Audry](http://sofianaudry.com/en)

# COPYING
Released under GNU GPL 3.0 License.  Copyright Sofian Audry and Erin Gee 2017.
