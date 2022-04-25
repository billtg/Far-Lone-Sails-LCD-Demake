using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHose : MonoBehaviour
{

    public static FireHose instance;
    public List<GameObject> lcdHeldNozzles;
    public List<GameObject> lcdHoses;
    public GameObject hoseRoll;
    public GameObject parkedNozzle;

    private void Awake()
    {
        instance = this;
    }

    public void InitializeFireHose()
    {
        ClearFireHoses();
        parkedNozzle.SetActive(true);
        hoseRoll.SetActive(true);
    }

    public void ClearFireHoses()
    {
        parkedNozzle.SetActive(false);
        hoseRoll.SetActive(false);
        foreach (GameObject nozzleObject in lcdHeldNozzles)
            nozzleObject.SetActive(false);
        foreach (GameObject hoseObject in lcdHoses)
            hoseObject.SetActive(false);
    }

    public void PickupNozzle()
    {
        Debug.Log("Picked up nozzle");
        //Turn on the lcds.
        parkedNozzle.SetActive(false);
        lcdHeldNozzles[37].SetActive(true);
    }

    public void DropNozzle()
    {
        Debug.Log("Dropped Nozzle");
        //Clear hose lcds, clear heldnozzle, turn on parkednozzle;
        InitializeFireHose();
    }

    public void activateLCDs (int state, bool lcdsOn)
    {
        switch (state)
        {
            case 1:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 2:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 3:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 5:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 7:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 8:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 30:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 36:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            case 37:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                break;
            default:
                Debug.LogError("Invalid state sent to FireHose: " + state.ToString());
                break;
        }
    }

}
