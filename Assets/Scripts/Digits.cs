using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digits : MonoBehaviour
{
    public static Digits instance;
    public List<GameObject> lcdDigits0;
    public List<GameObject> lcdDigits10;
    public List<GameObject> lcdDigits100;
    public List<GameObject> lcdDigits1000;

    private void Awake()
    {
        instance = this;
    }

    public void ClearDigitLCDs(bool active)
    {
        foreach (GameObject lcdObject in lcdDigits0)
            lcdObject.SetActive(active);
        foreach (GameObject lcdObject in lcdDigits10)
            lcdObject.SetActive(active);
        foreach (GameObject lcdObject in lcdDigits100)
            lcdObject.SetActive(active);
        foreach (GameObject lcdObject in lcdDigits1000)
            lcdObject.SetActive(active);
    }

    public void UpdateOdometer(int odometer)
    {
        //Break odometer down into individual numbers
        int thousands = odometer / 1000;
        int hundreds = (odometer % 1000) / 100;
        int tens = (odometer % 1000 % 100) / 10;
        int ones = (odometer % 1000 % 100 % 10);

        //Debug.Log("Thousands: " + thousands.ToString() + ", Hundreds: " + hundreds.ToString() + ", Tens: " + tens.ToString() + ", ones: " + ones.ToString());


        //Send those numbers to a function that activates the correct LCDs
        UpdateDigit(lcdDigits0, ones);
        UpdateDigit(lcdDigits10, tens);
        UpdateDigit(lcdDigits100, hundreds);
        UpdateDigit(lcdDigits1000, thousands);
    }

    void UpdateDigit(List<GameObject> digit, int numeral)
    {
        switch (numeral)
        {
            case 0:
                digit[0].SetActive(true);
                digit[1].SetActive(false);
                digit[2].SetActive(true);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(true);
                digit[6].SetActive(true);
                break;
            case 1:
                digit[0].SetActive(false);
                digit[1].SetActive(false);
                digit[2].SetActive(false);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(false);
                digit[6].SetActive(false);
                break;
            case 2:
                digit[0].SetActive(true);
                digit[1].SetActive(true);
                digit[2].SetActive(true);
                digit[3].SetActive(true);
                digit[4].SetActive(false);
                digit[5].SetActive(false);
                digit[6].SetActive(true);
                break;
            case 3:
                digit[0].SetActive(true);
                digit[1].SetActive(true);
                digit[2].SetActive(true);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(false);
                digit[6].SetActive(false);
                break;
            case 4:
                digit[0].SetActive(false);
                digit[1].SetActive(true);
                digit[2].SetActive(false);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(true);
                digit[6].SetActive(false);
                break;
            case 5:
                digit[0].SetActive(true);
                digit[1].SetActive(true);
                digit[2].SetActive(true);
                digit[3].SetActive(false);
                digit[4].SetActive(true);
                digit[5].SetActive(true);
                digit[6].SetActive(false);
                break;
            case 6:
                digit[0].SetActive(true);
                digit[1].SetActive(true);
                digit[2].SetActive(true);
                digit[3].SetActive(false);
                digit[4].SetActive(true);
                digit[5].SetActive(true);
                digit[6].SetActive(true);
                break;
            case 7:
                digit[0].SetActive(true);
                digit[1].SetActive(false);
                digit[2].SetActive(false);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(false);
                digit[6].SetActive(false);
                break;
            case 8:
                digit[0].SetActive(true);
                digit[1].SetActive(true);
                digit[2].SetActive(true);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(true);
                digit[6].SetActive(true);
                break;
            case 9:
                digit[0].SetActive(true);
                digit[1].SetActive(true);
                digit[2].SetActive(true);
                digit[3].SetActive(true);
                digit[4].SetActive(true);
                digit[5].SetActive(true);
                digit[6].SetActive(false);
                break;
            default:
                Debug.LogError("Incorrect numeral sent to UpdateDigitList: " + numeral.ToString());
                break;

        }
            
    }

}
