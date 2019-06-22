#include <Metro.h>

Metro olc = Metro(100);
const int ledPin = 3;       //Led pin değeri
const int arkaTrigPin = 6;  //Arkadaki mesafe sensörü Trig pini
const int arkaEchoPin = 7;  //Arkadaki mesafe sensörü Echo pini
const int onEchoPin = 8;    //Öndeki mesafe sensörü Trig pini
const int onTrigPin = 9;    //Öndeki mesafe sensörü Echo pini
const int in1Pin = 10;      //Ön motor geri yön pini
const int in2Pin = 11;      //Ön motor ileri yön pini    
const int in3Pin = 12;      //Arka motor geri yön pini
const int in4Pin = 13;      //Arka motor ileri yön pini
int onMesafe = 0;           //On mesafe sensörünün okuduğu değerin tutulacağı değişken
int arkaMesafe = 0;     //On mesafe sensörünün okuduğu değerin tutulacağı değişken
char RData;             //Bluetooth üzerinden alınan veri

void setup()
{
  pinMode(ledPin, OUTPUT);
  Serial.begin(38400);
  pinMode(in1Pin, OUTPUT);
  pinMode(in2Pin, OUTPUT);  
  pinMode(in3Pin, OUTPUT);
  pinMode(in4Pin, OUTPUT);
  pinMode(arkaTrigPin, OUTPUT);
  pinMode(arkaEchoPin, INPUT);
  pinMode(onTrigPin, OUTPUT);
  pinMode(onEchoPin, INPUT);
}

void loop() 
{
  if(olc.check()){
    onMesafe = onMesafeOlc();
    arkaMesafe = arkaMesafeOlc();
    olc.reset();
    Serial.println(String(onMesafe)+","+String(arkaMesafe));
  }
  if(onMesafe<7 || arkaMesafe<7)
    digitalWrite(ledPin,HIGH);
  else
    digitalWrite(ledPin,LOW);
  if (Serial.available())
  {
    RData=(Serial.read());
    if (RData=='w')
    {
      digitalWrite(in1Pin,LOW);
      digitalWrite(in2Pin,HIGH);
    }
    if (RData=='s')
    {
      digitalWrite(in1Pin,HIGH);
      digitalWrite(in2Pin,LOW);
    }
    if (RData=='a')
    {
      digitalWrite(in3Pin,LOW);
      digitalWrite(in4Pin,HIGH);
    }
    if (RData=='d')
    {
      digitalWrite(in3Pin,HIGH);
      digitalWrite(in4Pin,LOW);
    }
    if (RData=='z')
    {
      digitalWrite(in1Pin,LOW);
      digitalWrite(in2Pin,LOW);
      digitalWrite(in3Pin,LOW);
      digitalWrite(in4Pin,LOW);
    }
  }
}

int arkaMesafeOlc()
{
  digitalWrite(arkaTrigPin, LOW);
  delayMicroseconds(5);
  digitalWrite(arkaTrigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(arkaTrigPin, LOW);
  return (pulseIn(arkaEchoPin, HIGH)/2) / 29.1;
}
int onMesafeOlc(){
  digitalWrite(onTrigPin, LOW);
  delayMicroseconds(5);
  digitalWrite(onTrigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(onTrigPin, LOW);
  return (pulseIn(onEchoPin, HIGH)/2) / 29.1;
}
