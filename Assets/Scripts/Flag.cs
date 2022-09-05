using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public static Flag instance;
    public List<GameObject> lcdFlags;
    public GameObject lcdFlagPole;

    public float timeSinceFlap;
    public float flapTimeBase;
    public float flapTime;
    public bool activeFlap;


    private void Awake()
    {
        instance = this;
    }

    public void SetFlagState(int state)
    {
        ClearFlag(false);
        lcdFlagPole.SetActive(true);
        lcdFlags[0].SetActive(true);
        switch (state)
        {
            case 0:
                lcdFlags[1].SetActive(true);
                break;
            case 1:
                lcdFlags[2].SetActive(true);
                lcdFlags[3].SetActive(true);
                break;
            case 2:
                lcdFlags[2].SetActive(true);
                lcdFlags[4].SetActive(true);
                lcdFlags[5].SetActive(true);
                timeSinceFlap = Time.time;
                break;
            default:
                Debug.LogError("Incorrect state sent to Flag");
                break;
        }
    }

    public void ClearFlag(bool active)
    {
        foreach (GameObject flagObject in lcdFlags)
            flagObject.SetActive(active);
        lcdFlagPole.SetActive(active);
    }

    private void Update()
    {
        if (GameManager.instance.gameOver)
            return;
        if (lcdFlags[4].activeSelf)
        {
            if (Time.time - timeSinceFlap > flapTime)
            {
                if (activeFlap)
                {
                    lcdFlags[5].SetActive(true);
                    lcdFlags[6].SetActive(false);
                }
                else
                {
                    lcdFlags[5].SetActive(false);
                    lcdFlags[6].SetActive(true);
                }
                timeSinceFlap = Time.time;
                flapTime = flapTimeBase + Random.Range(0, 1f);
                activeFlap = !activeFlap;
            }
        }
    }
}
