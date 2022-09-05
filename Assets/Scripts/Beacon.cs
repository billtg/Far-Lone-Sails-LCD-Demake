using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public static Beacon instance;
    public bool beaconLit = false;
    public bool doorOpen = true;
    public bool buttonPushed = false;

    public GameObject lcdTower;
    public GameObject lcdBeacon;
    public GameObject lcdBeaconHinge;
    public GameObject lcdBeaconFuel;
    public GameObject lcdBeaconDoorOpen;

    public List<GameObject> lcdBeaconFire;
    public List<GameObject> lcdBeaconSmoke;
    public List<GameObject> lcdBeaconButton;

    float fireAnimationTimeSet;
    public float fireAnimationDelay;

    public float smokeDelay;

    float buttonTimeSet;
    public float buttonDelay;

    private void Awake()
    {
        instance = this;
    }

    public void ClearBeaconLCDs(bool active)
    {
        lcdTower.SetActive(active);
        lcdBeacon.SetActive(active);
        lcdBeaconHinge.SetActive(active);
        lcdBeaconFuel.SetActive(active);
        lcdBeaconDoorOpen.SetActive(active);

        foreach (GameObject beaconObject in lcdBeaconFire)
            beaconObject.SetActive(active);
        foreach (GameObject beaconObject in lcdBeaconSmoke)
            beaconObject.SetActive(active);
        foreach (GameObject beaconObject in lcdBeaconButton)
            beaconObject.SetActive(active);
    }

    public void ActivateBeaconLCDs()
    {
        //spawn the beacon, unlit
        lcdTower.SetActive(true);
        lcdBeacon.SetActive(true);
        lcdBeaconHinge.SetActive(true);
        lcdBeaconDoorOpen.SetActive(true);

        lcdBeaconButton[0].SetActive(true);
        lcdBeaconButton[1].SetActive(true);
    }

    public void PushBeaconButton()
    {
        if (buttonPushed)
            return;

        lcdBeaconButton[0].SetActive(false);
        lcdBeaconButton[2].SetActive(true);

        //Check for box on open door
        if (GameManager.instance.lcdGroundBoxes[49].activeSelf)
            LightBeacon();
        else
        {
            buttonTimeSet = Time.time;
            buttonPushed = true;
            lcdBeaconDoorOpen.SetActive(false);
            doorOpen = false;
        }

    }

    public void LightBeacon()
    {
        //This is the end of the game
        beaconLit = true;
        //Start with the LCDs
        lcdBeaconDoorOpen.SetActive(false);
        lcdBeaconFuel.SetActive(true);

        //Set the animation times
        fireAnimationTimeSet = Time.time;

        //start the smoke
        StartCoroutine(AnimateSmoke());

        //Let Gamemanager know. Good job.
        GameManager.instance.EndOfGame();
    }

    private void Update()
    {
        if (buttonPushed)
            if (Time.time - buttonTimeSet > buttonDelay)
            {
                buttonPushed = false;
                lcdBeaconButton[0].SetActive(true);
                lcdBeaconButton[2].SetActive(false);
                lcdBeaconDoorOpen.SetActive(true);
                doorOpen = true;
            }

        //Only update if the beacon isn't lit
        if (!beaconLit) return;

        AnimateFire();
    }

    void AnimateFire()
    {
        if (Time.time - fireAnimationTimeSet > fireAnimationDelay)
        {
            for (int i = 0; i < lcdBeaconFire.Count; i++)
                lcdBeaconFire[i].SetActive(Random.Range(0, 2) == 0);
            fireAnimationTimeSet = Time.time;
        }
    }
        IEnumerator AnimateSmoke()
    {
        for (int i = 0; i < lcdBeaconSmoke.Count; i++)
        {
            yield return new WaitForSeconds(smokeDelay);
            lcdBeaconSmoke[i].SetActive(true);
        }
    }
}
