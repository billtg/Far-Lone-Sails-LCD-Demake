using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamGauge : MonoBehaviour
{
    public static SteamGauge instance;
    public int steamState;
    public List<GameObject> lcdSteam;

    public List<GameObject> lcdButton3;
    public float button3PushedTime;
    public float button3Delay;
    public bool button3Pushed;

    private void Awake()
    {
        instance = this;
    }

    public void SetSteamState(int state)
    {
        ClearSteam(false);
        switch (state)
        {
            case 0:
                break;
            case 1:
                lcdSteam[0].SetActive(true);
                break;
            case 2:
                lcdSteam[0].SetActive(true);
                lcdSteam[1].SetActive(true);
                break;
            case 3:
                lcdSteam[0].SetActive(true);
                lcdSteam[1].SetActive(true);
                lcdSteam[2].SetActive(true);
                break;
            case 4:
                lcdSteam[0].SetActive(true);
                lcdSteam[1].SetActive(true);
                lcdSteam[2].SetActive(true);
                lcdSteam[3].SetActive(true);
                break;
            default:
                Debug.LogError("Invalid State sent to SetSteamState: " + state.ToString());
                break;
        }
        steamState = state;
        SetButton3LCD(button3Pushed);
    }
    public void ClearSteamLCDs(bool active)
    {
        ClearSteam(active);
        foreach (GameObject button1Object in lcdButton3)
            button1Object.SetActive(active);
    }
    public void ClearSteam(bool active)
    {
        foreach (GameObject steamObject in lcdSteam)
            steamObject.SetActive(active);
    }

    public void PressButton3()
    {
        if (button3Pushed) return;

        Debug.Log("Button3 pushed");
        SetButton3LCD(true);
        button3PushedTime = Time.time;
        button3Pushed = true;

        if (steamState == 0)
            return;
        else
        {
            GameManager.instance.LetOffSteam();
        }    
    }

    void SetButton3LCD(bool pushed)
    {
        if (pushed)
        {
            lcdButton3[0].SetActive(false);
            lcdButton3[1].SetActive(true);
            lcdButton3[2].SetActive(true);
        }
        else
        {
            lcdButton3[0].SetActive(true);
            lcdButton3[1].SetActive(true);
            lcdButton3[2].SetActive(false);
        }
    }

    private void Update()
    {
        if (!button3Pushed)
            return;
        else
        {
            if (Time.time - button3PushedTime > button3Delay)
            {
                SetButton3LCD(false);
                button3Pushed = false;
            }
        }
    }
}
