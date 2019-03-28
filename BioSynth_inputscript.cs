using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class BioSynth_inputscript : MonoBehaviour {

	private string portName;     //variable for name of serial port
	public int baudRate = 9600;  //Change this to suit your baudrate.  Can be done in the editor

	SerialPort serial;

	Thread myThread;

	//You can modify/add to these as you like, they are just the variables, in order, that are coming from the serialPort
	//of your microcontroller, separated by Tabs. 

	public float heart = 0; 
	public float bpm   = 0;  
	public float bvpa  = 0;  
	public float bpma  = 0; 
	public float gsr   = 0;  

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
				heart = float.Parse(values[arg++]);  //Serial.print(heartSignal); Serial.print("\t")
				bpm = float.Parse(values[arg++]);    //Serial.print(bpmSignal); Serial.print("\t")
				bvpa = float.Parse(values[arg++]);   //Serial.print(bvpaSignal); Serial.print("\t")
				bpma = float.Parse(values[arg++]);   //Serial.print(bpmaSignal); Serial.print("\t")
				gsr = float.Parse(values[arg++]);    //Serial.println(gsrSignal); 
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
