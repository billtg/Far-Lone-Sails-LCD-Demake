using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGauge : MonoBehaviour
{
    public List<GameObject> lcdFuelGauge;
    public static FuelGauge instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetFuelGauge(int fuel)
    {
        lcdFuelGauge[0].SetActive(false);
        lcdFuelGauge[1].SetActive(false);
        lcdFuelGauge[2].SetActive(false);
        lcdFuelGauge[3].SetActive(false);
        lcdFuelGauge[4].SetActive(false);
        lcdFuelGauge[5].SetActive(false);
        switch (fuel)
        {
            case 0:
                break;
            case 1:
                lcdFuelGauge[0].SetActive(true);
                break;
            case 2:
                lcdFuelGauge[0].SetActive(true);
                lcdFuelGauge[1].SetActive(true);
                break;
            case 3:
                lcdFuelGauge[0].SetActive(true);
                lcdFuelGauge[1].SetActive(true);
                lcdFuelGauge[2].SetActive(true);
                break;
            case 4:
                lcdFuelGauge[0].SetActive(true);
                lcdFuelGauge[1].SetActive(true);
                lcdFuelGauge[2].SetActive(true);
                lcdFuelGauge[3].SetActive(true);
                break;
            case 5:
                lcdFuelGauge[0].SetActive(true);
                lcdFuelGauge[1].SetActive(true);
                lcdFuelGauge[2].SetActive(true);
                lcdFuelGauge[3].SetActive(true);
                lcdFuelGauge[4].SetActive(true);
                break;
            case 6:
                lcdFuelGauge[0].SetActive(true);
                lcdFuelGauge[1].SetActive(true);
                lcdFuelGauge[2].SetActive(true);
                lcdFuelGauge[3].SetActive(true);
                lcdFuelGauge[4].SetActive(true);
                lcdFuelGauge[5].SetActive(true);
                break;
            default:
                Debug.LogError("Incorrect fuel amount");
                break;
        }
    }

    public void ClearFuelGauge(bool active)
    {
        foreach (GameObject fuelGaugeObject in lcdFuelGauge)
            fuelGaugeObject.SetActive(active);
    }
}
