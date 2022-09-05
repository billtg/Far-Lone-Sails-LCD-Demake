using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sails : MonoBehaviour
{
    public static Sails instance;
    public List<GameObject> lcdSails;
    public List<GameObject> lcdButton5;

    private void Awake()
    {
        instance = this;
    }

    public void SetSails(int state)
    {
        ClearSails(false);
        switch (state)
        {
            case 0:
                //Button unpressed, sails down
                lcdSails[0].SetActive(true);
                lcdButton5[0].SetActive(true);
                lcdButton5[1].SetActive(true);
                break;
            case 1:
                //Button pressing, forestay out
                lcdSails[0].SetActive(true);
                lcdSails[1].SetActive(true);
                lcdButton5[0].SetActive(true);
                lcdButton5[1].SetActive(true);
                break;
            case 2:
                //Button pressed, all sail up
                lcdSails[0].SetActive(true);
                lcdSails[1].SetActive(true);
                lcdSails[2].SetActive(true);
                lcdSails[3].SetActive(true);
                lcdSails[4].SetActive(true);
                lcdButton5[0].SetActive(false);
                lcdButton5[1].SetActive(true);
                lcdButton5[2].SetActive(true);
                break;
            default:
                Debug.LogError("Invalid state sent to SetSails: " + state.ToString());
                break;
        }
    }

    public void ClearSails(bool active)
    {
        foreach (GameObject sailObject in lcdSails)
            sailObject.SetActive(active);
        foreach (GameObject buttonObject in lcdButton5)
            buttonObject.SetActive(active);
    }
}
