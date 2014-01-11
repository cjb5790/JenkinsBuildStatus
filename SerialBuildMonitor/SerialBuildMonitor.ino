int red = 13;
int yellow = 12;
int green = 11;

void setup()  
{                
  pinMode(red, OUTPUT);
  pinMode(yellow, OUTPUT);
  pinMode(green, OUTPUT);
  Serial.begin(9600);  
}

void loop()                    
{
  if(Serial.available())
  {
    int response = Serial.read();
    if (response == '1')
    {    
      digitalWrite(red, HIGH);
      digitalWrite(yellow, LOW);
      digitalWrite(green, LOW);
    }
    else if (response == '2')
    {
      digitalWrite(red, LOW);
      digitalWrite(yellow, HIGH);
      digitalWrite(green, LOW);
    }
    else if (response == '3')
    {
      digitalWrite(red, LOW);
      digitalWrite(yellow, LOW);
      digitalWrite(green, HIGH);
    }
    else if (response == '0')
    {
      digitalWrite(red, LOW);
      digitalWrite(yellow, LOW);
      digitalWrite(green, LOW);
    }
  }
}
