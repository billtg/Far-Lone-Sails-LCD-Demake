using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brake : MonoBehaviour
{
    public List<GameObject> lcdButton4;
    public GameObject lcdBrake;

    public static Brake instance;

    private void Awake()
    {
        instance = this;
    }


    public void Button4Pushed(bool pushed)
    {
        ClearBrakeLCDs(false);
        if (pushed)
        {
            //activate brake, press button
            lcdBrake.SetActive(true);
            lcdButton4[0].SetActive(false);
            lcdButton4[1].SetActive(true);
            lcdButton4[2].SetActive(true);
            GameManager.instance.SetBrake(true);
        }
        else
        {
            //Remove brake, extend button
            lcdBrake.SetActive(false);
            lcdButton4[0].SetActive(true);
            lcdButton4[1].SetActive(true);
            lcdButton4[2].SetActive(false);
            GameManager.instance.SetBrake(false);
        }
    }

    public void ClearBrakeLCDs(bool active)
    {
        foreach (GameObject buttonObject in lcdButton4)
            buttonObject.SetActive(active);
        lcdBrake.SetActive(active);
    }
}
