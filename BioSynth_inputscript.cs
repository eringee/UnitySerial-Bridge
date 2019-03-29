//BioData Library
#include <Heart.h>
#include <Respiration.h>
#include <SkinConductance.h>

//declare variables for storing biodata
float heartSig;
float heartAmp;
float heartBPM;
float heartBPMchange;
float scSig;

//declare and initialize biodata pins
SkinConductance sc(A6);
Heart heart(A8);

//reference LEDS for BioData visualization
const int ledGSR = 4;
const int ledHeart = 5;

//variables for attenuating data flow to serial port 
unsigned long lastMillis = 0;
int serialDelay = 20; // is is important that data only flow at a maximum of 50 Hz 

void setup() {
  Serial.begin(9600);
}

void loop() {
  
  //update biosensors
  heart.update();
  sc.update();
 
  //get data from biosensors
  heartSig = heart.getNormalized();
  heartBPM = heart.getBPM();
  heartAmp = heart.amplitudeChange();
  heartBPMchange = heart.bpmChange();
  scSig = sc.getSCR();

  //map data to LEDs
  int heartSigLED = heartSig*100;
  int heartOutput = map(heartSigLED,0,100,0,255);
  analogWrite(ledHeart, heartOutput);
  
  int scSigLED = scSig*100;
  int scOutput = map(scSigLED,0,100,0,255);
  analogWrite(ledGSR, scOutput);

  //if threshold is passed, send data to serial port
  if (abs(millis() - lastMillis) > serialDelay) {
    lastMillis = millis();
    Serial.print(heartSig);
    Serial.print("\t");

    Serial.print(heartBPM);
    Serial.print("\t");

    Serial.print(heartBPMchange);
    Serial.print("\t");

    Serial.print(heartAmp);
    Serial.print("\t");

    Serial.println(scSig);   
  } 

}
