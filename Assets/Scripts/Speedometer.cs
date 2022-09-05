using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public static Speedometer instance;
    public GameObject middleKnob;
    public List<GameObject> speedNeedles;

    private void Awake()
    {
        instance = this;
    }
    
    public void UpdateSpeedometer(int speed)
    {
        ClearSpeedometerLCDs(false);
        middleKnob.SetActive(true);
        speedNeedles[speed].SetActive(true);
    }

    public void ClearSpeedometerLCDs(bool active)
    {
        foreach (GameObject needleObject in speedNeedles)
            needleObject.SetActive(active);
        middleKnob.SetActive(active);
    }
}
