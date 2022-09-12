using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHose : MonoBehaviour
{

    public static FireHose instance;

    public List<GameObject> lcdHeldNozzles;
    public List<GameObject> lcdHeldWelders;
    public List<GameObject> lcdHoses;

    public List<GameObject> lcdFuelWater;
    public List<GameObject> lcdSailsWater;
    public List<GameObject> lcdMotorWater;

    public List<GameObject> lcdWeldingArcs;

    public GameObject hoseRoll;
    public GameObject parkedNozzle;
    public GameObject parkedWelder;

    public bool hosing;
    float timeStartedHosing;
    public float timeToDouse;
    public HealthBar hoseTarget;

    public bool welding;
    float timeStartedWelding;
    float lastFuelDrain;
    public float timeToWeld;
    public float timeToDrainFuel;
    public HealthBar weldTarget;

    public float nozzleFlashTime;
    float lastNozzleFlash;
    public float welderFlashTime;
    float lastWelderFlash;

    public float waterAnimationTime;
    float lastWaterAnimationTime;
    public int waterAnimationState = 0;

    float lastWeldingAnimationTime;
    public float weldingAnimationTime = 0.1f;

    private void Awake()
    {
        instance = this;
        lastWaterAnimationTime = Time.time;
        lastWeldingAnimationTime = Time.time;
    }

    public void InitializeFireHose()
    {
        ClearFireHoses();
        parkedNozzle.SetActive(true);
        parkedWelder.SetActive(true);
        hoseRoll.SetActive(true);
    }

    public void ClearFireHoses()
    {
        parkedNozzle.SetActive(false);
        parkedWelder.SetActive(false);
        hoseRoll.SetActive(false);
        foreach (GameObject nozzleObject in lcdHeldNozzles)
            nozzleObject.SetActive(false);
        foreach (GameObject welderObject in lcdHeldWelders)
            welderObject.SetActive(false);
        foreach (GameObject hoseObject in lcdHoses)
            hoseObject.SetActive(false);
    }

    public void PickupNozzle()
    {
        //Debug.Log("Picked up nozzle");
        //Turn on the lcds.
        parkedNozzle.SetActive(false);
        lcdHeldNozzles[37].SetActive(true);
        lcdHoses[4].SetActive(true);
    }

    public void PickupWelder()
    {
        //Debug.Log("Picking up Welder");
        //turn on the LCDs
        parkedWelder.SetActive(false);
        lcdHeldWelders[38].SetActive(true);
        //Set the fire hose lcds
        lcdHoses[13].SetActive(true);
    }

    public void DropNozzle()
    {
        //Debug.Log("Dropped Nozzle");
        //Clear hose lcds, clear heldnozzle, turn on parkednozzle;
        InitializeFireHose();
        StopHosing();
    }

    public void DropWelder()
    {
        //Debug.Log("Dropped welder");
        //Clear hose lcds, clear heldnozzle, turn on parkednozzle;
        InitializeFireHose();
        StopWelding();
    }

    public void ActivateHoseLCDs (int state, bool lcdsOn)
    {
        ClearFireHoses();
        hoseRoll.SetActive(true);
        ActivateHeldObject(state, lcdsOn);
        switch (state)
        {
            case 1:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[0].SetActive(true);
                lcdHoses[1].SetActive(true);
                lcdHoses[2].SetActive(true);
                break;
            case 2:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                lcdHoses[3].SetActive(true);
                break;
            case 3:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[11].SetActive(true);
                break;
            case 5:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                lcdHoses[5].SetActive(true);
                lcdHoses[6].SetActive(true);
                break;
            case 7:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[7].SetActive(true);
                lcdHoses[8].SetActive(true);
                break;
            case 8:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[9].SetActive(true);
                lcdHoses[10].SetActive(true);
                break;
            case 30:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[11].SetActive(true);
                lcdHoses[12].SetActive(true);
                break;
            case 36:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                lcdHoses[5].SetActive(true);
                break;
            case 37:
                //lcdHeldNozzles[state].SetActive(lcdsOn);
                lcdHoses[4].SetActive(true);
                break;
            case 38:
                lcdHoses[13].SetActive(true);
                break;
            default:
                Debug.LogError("Invalid state sent to FireHose: " + state.ToString());
                break;
        }
    }

    void ActivateHeldObject(int state, bool lcdsOn)
    {
        if (GameManager.instance.playerHoldingNozzle)
        {
            lcdHeldNozzles[state].SetActive(lcdsOn);
            parkedWelder.SetActive(true);
        }
        if (GameManager.instance.playerHoldingWelder)
        {
            lcdHeldWelders[state].SetActive(lcdsOn);
            parkedNozzle.SetActive(true);
        }
    }

    public void StartHosing(HealthBar hoseTarget)
    {
        hosing = true;
        timeStartedHosing = Time.time;
        this.hoseTarget = hoseTarget;
        AudioManager.instance.FireHose();
    }

    public void StopHosing()
    {
        hosing = false;
        ClearWaterLCDs();
        waterAnimationState = 0;
        AudioManager.instance.StopHosing();
    }

    public void StartWelding(HealthBar weldTarget)
    {
        welding = true;
        timeStartedWelding = Time.time;
        lastFuelDrain = Time.time;
        this.weldTarget = weldTarget;
        AudioManager.instance.Welding();
    }

    public void StopWelding()
    {
        welding = false;
        ClearWeldingLCDs();
        AudioManager.instance.StopWelding();
    }

    private void Update()
    {
        //Flash the welder/nozzle if they're needed and not held.
        if ((Fire.instance.fuelOnFire || Fire.instance.motorOnFire || Fire.instance.sailsOnFire) && !GameManager.instance.playerHoldingNozzle)
            FlashNozzle();
        if (!Health.instance.AtFullHealth() && !GameManager.instance.playerHoldingWelder)
            FlashWelder();

        //Only update if actively hosing
        if (!hosing && !welding)
            return;
        //For the active hose target, check if its been dousing long enough, then put out the fire. Animate water meanwhile.
        if (hosing)
        {
            switch (hoseTarget)
            {
                case HealthBar.fuel:
                    if (Time.time - timeStartedHosing > timeToDouse)
                    {
                        //Debug.Log("Fuel fire is out");
                        Fire.instance.DouseFire(HealthBar.fuel);
                        StopHosing();
                    }
                    else
                        AnimateWater(lcdFuelWater);
                    break;
                case HealthBar.motor:
                    if (Time.time - timeStartedHosing > timeToDouse)
                    {
                        //Debug.Log("Motor fire is out");
                        Fire.instance.DouseFire(HealthBar.motor);
                        StopHosing();
                    }
                    else
                        AnimateWater(lcdMotorWater);
                    break;
                case HealthBar.sails:
                    if (Time.time - timeStartedHosing > timeToDouse)
                    {
                        //Debug.Log("Sails fire is out");
                        Fire.instance.DouseFire(HealthBar.sails);
                        StopHosing();
                    }
                    else
                        AnimateWater(lcdSailsWater);
                    break;
            }
        }

        //Weld
        if (welding)
        {
            //DrainFuel();
            switch (weldTarget)
            {
                case HealthBar.fuel:
                    if (Time.time - timeStartedWelding > timeToWeld)
                    {
                        //Debug.Log("Fuel health increased");
                        Health.instance.RemoveDamage(HealthBar.fuel);
                        if (Health.instance.fuelHealth == 3)
                            StopWelding();
                        else
                            timeStartedWelding = Time.time;
                    }
                    else
                        AnimateWelding(lcdWeldingArcs[0]);
                    break;
                case HealthBar.motor:
                    if (Time.time - timeStartedWelding > timeToWeld)
                    {
                        //Debug.Log("Motor health increased");
                        Health.instance.RemoveDamage(HealthBar.motor);
                        if (Health.instance.motorHealth == 3)
                            StopWelding();
                        else
                            timeStartedWelding = Time.time;
                    }
                    else
                        AnimateWelding(lcdWeldingArcs[2]);
                    break;
                case HealthBar.sails:
                    if (Time.time - timeStartedWelding > timeToWeld)
                    {
                        //Debug.Log("Sail health increased");
                        Health.instance.RemoveDamage(HealthBar.sails);
                        if (Health.instance.sailHealth == 3)
                            StopWelding();
                        else
                            timeStartedWelding = Time.time;
                    }
                    else
                        AnimateWelding(lcdWeldingArcs[1]);
                    break;
            }
        }
    }

    void DrainFuel()
    {
        if (Time.time - lastFuelDrain > timeToDrainFuel)
        {
            GameManager.instance.DrainFuel();
            lastFuelDrain = Time.time;
        }
    }

    void FlashNozzle()
    {
        if (Time.time - lastNozzleFlash > nozzleFlashTime)
        {
            parkedNozzle.SetActive(!parkedNozzle.activeSelf);
            lastNozzleFlash = Time.time;
        }
    }

    void FlashWelder()
    {
        if (Time.time - lastWelderFlash > welderFlashTime)
        {
            parkedWelder.SetActive(!parkedWelder.activeSelf);
            lastWelderFlash = Time.time;
        }
    }

    void AnimateWater(List<GameObject> lcdAnimateWater)
    {
        //After the animation delay, set the current animation state lcd on, then increment and reset time
        if (Time.time - lastWaterAnimationTime > waterAnimationTime)
        {
            ClearWaterLCDs();
            lcdAnimateWater[waterAnimationState].SetActive(true);
            waterAnimationState++;
            if (waterAnimationState > 2)
                waterAnimationState = 0;
            lastWaterAnimationTime = Time.time;
        }
    }

    void AnimateWelding(GameObject weldingAnimationTarget)
    {
        //Flicker the welding arc for this state
        if (Time.time - lastWeldingAnimationTime > weldingAnimationTime)
        {
            weldingAnimationTarget.SetActive(!weldingAnimationTarget.activeSelf);
            lastWeldingAnimationTime = Time.time;
            weldingAnimationTime = Random.Range(0.01f, 0.1f);
        }
    }

    public void ClearWaterLCDs()
    {
        foreach (GameObject waterObject in lcdFuelWater)
            waterObject.SetActive(false);
        foreach (GameObject waterObject in lcdSailsWater)
            waterObject.SetActive(false);
        foreach (GameObject waterObject in lcdMotorWater)
            waterObject.SetActive(false);
    }

    public void ClearWeldingLCDs()
    {
        foreach (GameObject weldingObject in lcdWeldingArcs)
            weldingObject.SetActive(false);
    }

    public void TurnOnAllLCDs()
    {
        //Nozzles
        foreach (GameObject nozzleObject in lcdHeldNozzles)
            nozzleObject.SetActive(true);
        foreach (GameObject nozzleObject in lcdHeldWelders)
            nozzleObject.SetActive(true);
        parkedNozzle.SetActive(true);
        parkedWelder.SetActive(true);

        //Hoses
        foreach (GameObject hoseObject in lcdHoses)
            hoseObject.SetActive(true);
        hoseRoll.SetActive(true);

        //Welding
        foreach (GameObject weldingObject in lcdWeldingArcs)
            weldingObject.SetActive(true);
        //Water
        foreach (GameObject waterObject in lcdFuelWater)
            waterObject.SetActive(true);
        foreach (GameObject waterObject in lcdSailsWater)
            waterObject.SetActive(true);
        foreach (GameObject waterObject in lcdMotorWater)
            waterObject.SetActive(true);
    }
}
