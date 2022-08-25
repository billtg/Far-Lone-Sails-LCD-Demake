using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public List<GameObject> lcdDigits0minutes;
    public List<GameObject> lcdDigits10seconds;
    public List<GameObject> lcdDigits0seconds;
    public List<GameObject> lcdDigits0tenths;
    public List<GameObject> lcdColon;
    public GameObject lcdDecimal;

    float timerStartTime;
    public float timeInSeconds;
    public bool timerRunning;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!timerRunning) return;

        timeInSeconds = Time.time - timerStartTime;
        UpdateTimer();
    }

    public void StartTimer()
    {
        Debug.Log("Timer Started");
        timerStartTime = Time.time;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
        UpdateTimer();
    }

    public void ActivateDots()
    {
        lcdDecimal.SetActive(true);
        lcdColon[0].SetActive(true);
        lcdColon[1].SetActive(true);
    }
    public void ClearTimerLCDs()
    {
        foreach (GameObject lcdObject in lcdDigits0seconds)
            lcdObject.SetActive(false);
        foreach (GameObject lcdObject in lcdDigits10seconds)
            lcdObject.SetActive(false);
        foreach (GameObject lcdObject in lcdDigits0minutes)
            lcdObject.SetActive(false);
        foreach (GameObject lcdObject in lcdDigits0tenths)
            lcdObject.SetActive(false);
        foreach (GameObject lcdObject in lcdColon)
            lcdObject.SetActive(false);
        lcdDecimal.SetActive(false);
    }

    public void UpdateTimer()
    {
        //Break timer down into individual numbers
        int minutes = (int)timeInSeconds / 60;
        int seconds10 = (int)(timeInSeconds % 60 /10);
        int seconds1 = (int)(timeInSeconds % 60 % 10);
        int tenths = (int)(timeInSeconds % 60 % 1 * 10);

        //Debug.Log(minutes.ToString() + ":" + seconds10.ToString() + seconds1.ToString() + "." + tenths.ToString());


        //Send those numbers to a function that activates the correct LCDs
        UpdateDigit(lcdDigits0minutes, minutes);
        UpdateDigit(lcdDigits10seconds, seconds10);
        UpdateDigit(lcdDigits0seconds, seconds1);
        UpdateDigit(lcdDigits0tenths, tenths);
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
