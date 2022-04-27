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
        lcdHoses[4].SetActive(true);
    }

    public void DropNozzle()
    {
        Debug.Log("Dropped Nozzle");
        //Clear hose lcds, clear heldnozzle, turn on parkednozzle;
        InitializeFireHose();
    }

    public void activateLCDs (int state, bool lcdsOn)
    {
        ClearFireHoses();
        hoseRoll.SetActive(true);
        switch (state)
        {
            case 1:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[0].SetActive(true);
                lcdHoses[1].SetActive(true);
                lcdHoses[2].SetActive(true);
                break;
            case 2:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                lcdHoses[3].SetActive(true);
                break;
            case 3:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[11].SetActive(true);
                break;
            case 5:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                lcdHoses[5].SetActive(true);
                lcdHoses[6].SetActive(true);
                break;
            case 7:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[7].SetActive(true);
                lcdHoses[8].SetActive(true);
                break;
            case 8:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[9].SetActive(true);
                lcdHoses[10].SetActive(true);
                break;
            case 30:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[11].SetActive(true);
                lcdHoses[12].SetActive(true);
                break;
            case 36:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                lcdHoses[5].SetActive(true);
                break;
            case 37:
                lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                break;
            default:
                Debug.LogError("Invalid state sent to FireHose: " + state.ToString());
                break;
        }
    }

}
