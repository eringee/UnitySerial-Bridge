using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

// this scrips was last tested on Mac using 2017.3.1 with High Sierra

// Feel free to update the file to let others know if it is still working in later versions of Unity or other OS :)

public class Serial_inputscript : MonoBehaviour {
        
	SerialPort serial;
	private string portName;     //variable for name of serial port
	public int baudRate = 9600;  //Change this to suit your baudrate.  Can be done in the editor
	
	Thread myThread;

	//You can name or add to these variables as you like, they are just the variables, in order, that are coming from the serialPort
	//of your microcontroller, separated by Tabs. 

	public float arduinoVal1; 
	public float arduinoVal2;  
	public float arduinoVal3;  

	// Initialization
	void Start () {

		string portName = GetPortName(); //Figure out which serial port the microcontroller is attached to

		if (portName == "") {
			print ("Error: Couldn't find serial port.");
			return;
		} else {
	       Debug.Log("Using Serial:" + portName);
		}

		serial = new SerialPort(portName, baudRate);  //initialize serial port
		serial.Open();
		serial.ReadTimeout = 10;

		// Start thread.
		myThread = new Thread(new ThreadStart(GetArduino));
		myThread.Start();
	} 

	// Read data from the Arduino
	private void GetArduino(){
		while (myThread.IsAlive)
		{
			string line = serial.ReadLine();

			try
			{
				string[] values = line.Split('\t');  //using tab separated values.  See comments for Arduino code.
				int arg = 0;                         
				arduinoVal1 = float.Parse(values[arg++]);  //Serial.print(mySensor1); Serial.print("\t")
				arduinoVal2 = float.Parse(values[arg++]);  //Serial.print(mySensor2); Serial.print("\t")
				arduinoVal3 = float.Parse(values[arg++]);  //Serial.println(mySensor3);  
			}
			catch (Exception e)      // this is needed because the Arduino throws weird artifacts sometimes
			{
				Debug.Log(e.Message);
			}
		}
	}

	string GetPortName() {       

		string[] portNames;

		switch (Application.platform) {

		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.LinuxPlayer:

			portNames = System.IO.Ports.SerialPort.GetPortNames();

			if (portNames.Length ==0) {
				portNames = System.IO.Directory.GetFiles("/dev/");                
			}

			foreach (string portName in portNames) {                                
				if (portName.StartsWith("/dev/tty.usb") || portName.StartsWith("/dev/ttyUSB")) return portName;
			}                
			return ""; 

		default: // Windows

			portNames = System.IO.Ports.SerialPort.GetPortNames();

			if (portNames.Length > 0) return portNames[0];
			else return "COM3";
		}
	}

	// close the serial ports if quitting program
	private void OnApplicationQuit()
	{
		Cleanup();
	}

	public void OnDestroy()
	{
		Cleanup();
	}

	void Cleanup()  // close the serial ports if there is no Arduino activity
	{
		if (serial != null)
		{
			if (serial.IsOpen)
				serial.Close();
		}
		serial = null;
	}

}
