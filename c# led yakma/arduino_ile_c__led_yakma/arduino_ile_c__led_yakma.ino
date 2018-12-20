int beklemesuresi=110;
void setup() {
  // put your setup code here, to run once:
pinMode(5,OUTPUT);
pinMode(6,OUTPUT);
pinMode(7,OUTPUT);
pinMode(8,OUTPUT);
pinMode(9,OUTPUT);
pinMode(10,OUTPUT);
pinMode(11,OUTPUT);
pinMode(12,OUTPUT);
pinMode(13,OUTPUT);

Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
if(Serial.available())
{
  int a=Serial.read();
  if(a=='1')
  {
    digitalWrite(5,HIGH);
    delay(beklemesuresi);
    digitalWrite(5,LOW);
  }
  else if(a=='2')
  {
    digitalWrite(6,HIGH);
    delay(beklemesuresi);
    digitalWrite(6,LOW);
  }
  else if(a=='3')
  {
    digitalWrite(7,HIGH);
    delay(beklemesuresi);
    digitalWrite(7,LOW);
  }
  else if(a=='4')
  {
    digitalWrite(8,HIGH);
    delay(beklemesuresi);
    digitalWrite(8,LOW);
  }
  else if(a=='5')
  {
    digitalWrite(9,HIGH);
    delay(beklemesuresi);
    digitalWrite(9,LOW);
  }
  else if(a=='6')
  {
    digitalWrite(10,HIGH);
    delay(beklemesuresi);
    digitalWrite(10,LOW);
  }
  else if(a=='7')
  {
    digitalWrite(11,HIGH);
    delay(beklemesuresi);
    digitalWrite(11,LOW);
  }
  else if(a=='8')
  {
    digitalWrite(12,HIGH);
    delay(beklemesuresi);
    digitalWrite(12,LOW);
  }
  else if(a=='9')
  {
    digitalWrite(13,HIGH);
    delay(beklemesuresi);
    digitalWrite(13,LOW);
  }
}
}
