using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontFlap : MonoBehaviour
{
    public static FrontFlap instance;
    public List<GameObject> lcdFlaps;
    public GameObject lcdHinge;
    private void Awake()
    {
        instance = this;
    }

    public void UpdateFrontFlap(int speed)
    {
        ClearFrontFlap();
        lcdHinge.SetActive(true);
        if (speed == 0)
            lcdFlaps[1].SetActive(true);
        else
            lcdFlaps[0].SetActive(true);
    }

    public void ClearFrontFlap()
    {
        lcdFlaps[0].SetActive(false);
        lcdFlaps[1].SetActive(false);
        lcdHinge.SetActive(false);

    }
}
