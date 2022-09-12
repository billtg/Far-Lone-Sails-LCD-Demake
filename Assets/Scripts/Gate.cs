using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static Gate instance;

    [Header("LCd Objects")]
    public GameObject lcdFrame;
    public GameObject lcdLadder0;
    public GameObject lcdLadder1;
    public List<GameObject> lcdDoors;
    public List<GameObject> lcdDoorRoll;

    public GameObject lcdFuelTank;
    public GameObject lcdFuelDoorOpen;
    public GameObject lcdFuelDoorClosed;
    public GameObject lcdFuelHinge;
    public List<GameObject> lcdFuelTankGauge;

    public List<GameObject> lcdFuelButton;
    public List<GameObject> lcdDoorButton;

    public GameObject lcdDummyGate;
    public GameObject lcdArrow;
    public GameObject lcdLeavingGate;

    [Header("Variables")]
    public int ticksToGate;
    public int gateTicksDelay;
    public int gateState = 0;
    public bool gateSpawned;
    public bool gateOpen;
    public bool ladderDown;
    public bool fuelDoorOpen;
    public bool fuelButtonPressed;
    public bool doorButtonPressed;
    public bool fuelLoaded;
    bool delayDoorOpening;
    public int gateMoveTicks;
    public int gateMoveThreshold;

    [Header("Animation")]
    public float arrowAnimationTime;
    float timeSinceLastArrow;
    public float doorClosedDelay;
    float timeSinceDoorClosed;
    public float fuelGaugeLoadingDelay;
    public float doorOpenDelay;
    public float doorRollDelay;

    private void Awake()
    {
        instance = this;
        gateMoveTicks = 0;
    }

    public void ClearGate(bool active)
    {
        //Clear frame, ladder doors
        lcdFrame.SetActive(active);
        lcdLadder0.SetActive(active);
        lcdLadder1.SetActive(active);
        foreach (GameObject lcdObject in lcdDoors)
            lcdObject.SetActive(active);
        foreach (GameObject lcdObject in lcdDoorRoll)
            lcdObject.SetActive(active);

        //Cear fuel tank
        lcdFuelTank.SetActive(active);
        lcdFuelDoorOpen.SetActive(active);
        lcdFuelDoorClosed.SetActive(active);
        lcdFuelHinge.SetActive(active);
        foreach (GameObject lcdObject in lcdFuelTankGauge)
            lcdObject.SetActive(active);

        //clear buttons
        foreach (GameObject lcdObject in lcdFuelButton)
            lcdObject.SetActive(active);
        foreach (GameObject lcdObject in lcdDoorButton)
            lcdObject.SetActive(active);

        //Clear dummy gate and arrow
        lcdDummyGate.SetActive(active);
        lcdArrow.SetActive(active);
        lcdLeavingGate.SetActive(active);
    }

    public void SpawnGate()
    {
        gateSpawned = true;
        gateOpen = false;
        ladderDown = false;
        fuelDoorOpen = true;
        fuelButtonPressed = false;
        doorButtonPressed = true;
        fuelLoaded = false;
        delayDoorOpening = false;

        gateState = 1;
        lcdArrow.SetActive(true);
        timeSinceLastArrow = Time.time;

        //Set up for the next gate. Don't spawn it if it's too close to the end amount
        ticksToGate = GameManager.instance.odometerAmount + Random.Range(gateTicksDelay - 10, gateTicksDelay + 20);
        if (GameManager.instance.ticksUntilWin - ticksToGate < 20)
            ticksToGate = 10000;
        
    }

    public void ResetGate()
    {
        gateState = 0;
        ChangeGateState();
        gateMoveTicks = 0;
    }
    public void MoveGate()
    {
        //Accumulate ticks, move gates between states
        //Gate states: 0 = no gate; 1 = Gate coming; 2 = Gate in front; 3 = gate straddling; 4 = gate leaving
        gateMoveTicks++;
        if (gateMoveTicks >= gateMoveThreshold)
        {
            //Check if the gate is blocking
            if (gateState == 3 && !gateOpen)
            {
                //Hit the car.
                GameManager.instance.HitGate();
                gateMoveTicks = 0;
            }
            //Otherwise, advance
            else
            {
                //Debug.Log("Gate moving");
                gateState++;
                if (gateState > 4)
                    gateState = 0;
                ChangeGateState();
                gateMoveTicks = 0;
            }
        }
    }

    public void ChangeGateState()
    {
        switch (gateState)
        {
            case 0:
                //No gate spawned. Do nothing
                ClearGate(false);
                gateSpawned = false;
                break;
            case 1:
                //Gate coming. Activate the arrow
                AnimateArrow();
                break;
            case 2:
                //Gate close by. Activate the dummy gate
                ClearGate(false);
                lcdDummyGate.SetActive(true);
                break;
            case 3:
                //Gate blocking
                ClearGate(false);
                lcdDummyGate.SetActive(false);
                ActivateBlockingGate();
                break;
            case 4:
                //Gate leaving
                //Update LCDs
                ClearGate(false);
                lcdLeavingGate.SetActive(true);
                //Hit the sails if they're up
                if (GameManager.instance.sailsUp)
                    GameManager.instance.HitSails();
                //Check for player on the frame
                GameManager.instance.GateMovesWithPlayer();
                break;
            default:
                Debug.LogError("Invalid state for gateState: " + gateState.ToString());
                break;
        }
    }

    void ActivateBlockingGate()
    {
        //Activate LCDs
        lcdFrame.SetActive(true);
        lcdFuelTank.SetActive(true);
        lcdFuelDoorOpen.SetActive(true);
        lcdLadder0.SetActive(true);

        //Activate buttons
        lcdFuelButton[0].SetActive(true);
        lcdFuelButton[1].SetActive(true);

        lcdDoorButton[1].SetActive(true);
        lcdDoorButton[2].SetActive(true);

        //Activate Door and door roll
        foreach (GameObject doorObject in lcdDoors)
            doorObject.SetActive(true);
        lcdDoorRoll[0].SetActive(true);

        //Put down the ladder
        lcdLadder1.SetActive(true);
        ladderDown = true;

    }

    public void PressFuelButton()
    {
        //Change the LCDs
        lcdFuelButton[0].SetActive(false);
        lcdFuelButton[2].SetActive(true);
        //Close the fuel door
        lcdFuelDoorOpen.SetActive(false);
        lcdFuelDoorClosed.SetActive(true);
        //Load fuel if it was there
        fuelDoorOpen = false;
        if (GameManager.instance.lcdGroundBoxes[42].activeSelf)
            LoadFuel();
        else
        {
            //Delay door opening and button unpushing
            delayDoorOpening = true;
            timeSinceDoorClosed = Time.time;
        }
    }
    public void LoadFuel()
    {
        fuelLoaded = true;
        fuelButtonPressed = true;
        GameManager.instance.lcdGroundBoxes[42].SetActive(false);
        //Set the fuel door lcds
        lcdFuelDoorOpen.SetActive(false);
        lcdFuelDoorClosed.SetActive(true);
        //set the fuel gauge LCDs
        StartCoroutine(AnimateFuelGauge());
        //Set the door button
        lcdDoorButton[0].SetActive(true);
        lcdDoorButton[1].SetActive(true);
        lcdDoorButton[2].SetActive(false);
        //Play the audio
        AudioManager.instance.GateFuelLoad();
    }

    public void PressDoorButton()
    {
        //Set the LCDs
        lcdDoorButton[0].SetActive(false);
        lcdDoorButton[1].SetActive(true);
        lcdDoorButton[2].SetActive(true);
        fuelLoaded = false;
        //Start animating the door opening
        StartCoroutine(AnimateDoorOpening());
        //Start animating the door roll
        StartCoroutine(AnimateDoorRoll());
        //Break the ladder
        lcdLadder1.SetActive(false);
        ladderDown = false;
        //Play the gate opening audio
        AudioManager.instance.GateOpen();
    }

    private void Update()
    {
        if (!gateSpawned)
            return;

        switch(gateState)
        {
            case 0:
                //No gate spawned. Do nothing
                break;
            case 1:
                //Gate coming. Activate the arrow
                AnimateArrow();
                break;
            case 2:
                //Gate close by. Activate the dummy gate
                break;
            case 3:
                //Gate blocking
                if (delayDoorOpening)
                {
                    if (Time.time - timeSinceDoorClosed > doorClosedDelay)
                    {
                        //Change the LCDs
                        lcdFuelButton[0].SetActive(true);
                        lcdFuelButton[2].SetActive(false);
                        //open the fuel door
                        lcdFuelDoorOpen.SetActive(true);
                        lcdFuelDoorClosed.SetActive(false);

                        fuelDoorOpen = true;
                        delayDoorOpening = false;
                    }
                }
                break;
            case 4:
                //Gate leaving
                break;
            default:
                Debug.LogError("Invalid state for gateState: " + gateState.ToString());
                break;
        }
    }

    void AnimateArrow()
    {
        if (Time.time - timeSinceLastArrow > arrowAnimationTime)
        {
            lcdArrow.SetActive(!lcdArrow.activeSelf);
            timeSinceLastArrow = Time.time;

        }
    }

    IEnumerator AnimateFuelGauge()
    {
        lcdFuelTankGauge[0].SetActive(true);
        yield return new WaitForSeconds(fuelGaugeLoadingDelay);
        lcdFuelTankGauge[1].SetActive(true);
        yield return new WaitForSeconds(fuelGaugeLoadingDelay);
        lcdFuelTankGauge[2].SetActive(true);
        yield return new WaitForSeconds(fuelGaugeLoadingDelay);
        lcdFuelTankGauge[3].SetActive(true);

    }

    IEnumerator AnimateDoorOpening()
    {
        for (int i = lcdDoors.Count; i > 0; i--)
        {
            lcdDoors[i-1].SetActive(false);
            yield return new WaitForSeconds(doorOpenDelay);
        }
        AudioManager.instance.StopGateOpen();
        yield return new WaitForSeconds(2);
        gateOpen = true;
        GameManager.instance.GateOpen();
    }
    
    IEnumerator AnimateDoorRoll()
    {
        for (int i = 0; i < lcdDoorRoll.Count; i++)
        {
            lcdDoorRoll[i].SetActive(true);
            yield return new WaitForSeconds(doorRollDelay);
        }
    }
}
