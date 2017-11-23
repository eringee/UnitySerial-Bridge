using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class BioSynth_inputscript : MonoBehaviour {

    public string port = "COM1";   //Change this to suit the name of your port.  Can be done in the editor.
    public int baudRate = 115200;  //Change this to suit your baudrate.  Can be done in the editor

    SerialPort stream;

    Thread myThread;

	//You can modify/add to these as you like, they are just the variables, in order, that are coming from the serialPort
	//of your microcontroller, separated by Tabs. 

	public float temperature = 0;
	public float heart = 0;
	public float bpm   = 0;
	public float bvpa  = 0;
	public float bpma  = 0;
	public float gsr   = 0;

		
	// Use this for initialization
	void Start () {
        stream = new SerialPort(port, baudRate);

        try
        {
            stream.Open();

            Debug.Log("Opened stream: " + stream);
        }
        catch (System.Exception e)
        {
            Debug.Log("Could not open serial port: " + e.Message);

        }

        // Start thread.
        myThread = new Thread(new ThreadStart(GetArduino));
        myThread.Start();
    }

    void Cleanup()
    {
        if (stream != null)
        {
            if (stream.IsOpen)
                stream.Close();
        }

        stream = null;
    }

    private void OnApplicationQuit()
    {
        Cleanup();
    }

    public void OnDestroy()
    {
        Cleanup();
    }

    private void GetArduino()
    {
        while (myThread.IsAlive)
        {
            string line = stream.ReadLine();

            try
            {
                string[] values = line.Split('\t');
                int arg = 0;
				temperature = float.Parse(values[arg++]);
                heart = float.Parse(values[arg++]);
                bpm = float.Parse(values[arg++]);
                bvpa = float.Parse(values[arg++]);
                bpma = float.Parse(values[arg++]);
                gsr = float.Parse(values[arg++]);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

}

