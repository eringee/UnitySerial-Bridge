//BioData Library
#include <Heart.h>
#include <Respiration.h>
#include <SkinConductance.h>

//Plaquette is a useful all-purpose Arduino library.  
//  see https://sofapirate.github.io/Plaquette/ 
#include <Plaquette.h>

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
AnalogOut ledGSR = 4;
AnalogOut ledHeart = 5;

//variables for attenuating data flow to serial port 
unsigned long lastMillis = 0;
int serialDelay = 20; // 50 Hz 

void begin() {
  Serial.begin(9600);
}

void step() {
  
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
  heartSig >> ledHeart;
  scSig >> ledGSR;

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
