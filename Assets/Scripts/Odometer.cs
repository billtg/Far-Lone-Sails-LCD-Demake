using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Odometer : MonoBehaviour
{
    public static Odometer instance;
    public Text odometerText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateOdometer(int odometerReading)
    {
        string odometerReadingString = "";
        if (odometerReading < 10)
            odometerReadingString = "000" + odometerReading.ToString();
        else if (odometerReading < 100)
            odometerReadingString = "00" + odometerReading.ToString();
        else if (odometerReading < 1000)
            odometerReadingString = "0" + odometerReading.ToString();
        else
            odometerReadingString = odometerReading.ToString();


        odometerText.text = odometerReadingString;
    }
}
