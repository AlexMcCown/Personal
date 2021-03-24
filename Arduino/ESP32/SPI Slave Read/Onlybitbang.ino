#include <Arduino.h>
#define CS 16
#define CLK 17
#define D 18

#define BIT_SIZE 32
bool bit_arr_msbf[BIT_SIZE] = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }; //This can be more dynamically done with a for loop in setup()
bool bit_arr_lsbf[BIT_SIZE] = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };

unsigned int MSBFirstBoolToUint() {
    int bit_arr_size = sizeof(bit_arr_msbf);
    unsigned int result = 0;
    for (int i = 0; i < bit_arr_size; i++) {
        result |= bit_arr_msbf[i] << (bit_arr_size - i - 1);
    }
    return result;
}
unsigned int LSBFirstBoolToUint() {
    int bit_arr_size = sizeof(bit_arr_lsbf);
    unsigned int result = 0;
    for (int i = 0; i < bit_arr_size; i++) {
        result |= bit_arr_lsbf[i] << i;
    }
    return result;
}

//everything below was freehand, untested and might not work 

void ChipSelectInterrupt(){
    int index = 0;
    bool flag = false;
    while(index < BIT_SIZE){
        if(digitalRead(CLK) && !flag){
            //short delay can be added here 
            bit_arr_msbf[index] = digitalRead(D);
            index++;
            flag = true;
        }
        else{
            flag = false;
        }
    }
    Serial.println(MSBFirstBoolToUint());
}

void setup() {
    Serial.begin(115200);
    pinMode(CLK, INPUT);
    pinMode(D, INPUT);
    attachInterrupt(digitalPinToInterrupt(CS), ChipSelectInterrupt, FALLING);
}

void loop(){
    ;; //nothing happens here 
}



