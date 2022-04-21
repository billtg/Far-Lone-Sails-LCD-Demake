using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> lcdPlayers;
    public List<GameObject> lcdGrounds;
    public List<GameObject> lcdButton1;
    public List<GameObject> lcdButton2;
    public List<GameObject> lcdLifts;
    public List<GameObject> lcdGroundBoxes;
    public List<GameObject> lcdHeldBoxes;
    public List<GameObject> lcdFuelGauge;
    public List<GameObject> lcdsmallWheel;
    //public List<GameObject> lcdInteractables;

    //Player States
    PlayerBaseState currentPlayerState;
    public PlayerState0 ps0 = new PlayerState0();
    public PlayerState1 ps1 = new PlayerState1();
    public PlayerState2 ps2 = new PlayerState2();
    public PlayerState3 ps3 = new PlayerState3();
    public PlayerState4 ps4 = new PlayerState4();
    public PlayerState5 ps5 = new PlayerState5();
    public PlayerState6 ps6 = new PlayerState6();
    public PlayerState7 ps7 = new PlayerState7();
    public PlayerState8 ps8 = new PlayerState8();
    public PlayerState9 ps9 = new PlayerState9();
    public PlayerState10 ps10 = new PlayerState10();

    //Gameplay values
    public float playerHangTime;
    public bool playerHoldingBox;
    public int fuel;
    public int steam;
    public bool vehicleMoving;
    public int speed;

    //Button1 value
    public int button1State;
    public bool pushingButton1 = false;
    public float button1StateTime;
    public float button1StateChangedTime;

    //Button2 stuff
    public bool button2Pushed;
    public float button2PushedTime;
    public float button2Delay;

    //Wheel stuff
    public bool smallWheelState;

    //Vehicle Movement
    public float lastTickTime;
    public float tickBaseTime;
    public float tickSpeedTime;
    public int fuelCounter;
    public int fuelRollover;
    public int steamCounter;
    public int steamRollover;
    public int motorSpeed;
    public int sailSpeed;
    public bool brakeActive;

    AudioSource audioSource;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //play a little ditty with all the objects active

        //Turn on only the initial lcd objects
        ClearScreen();
        InitializeLcdObjects();

        //Set the initial player state
        currentPlayerState = ps0;
        playerHoldingBox = false;

        //Get the audio source
        audioSource = GetComponent<AudioSource>();

        //Initialize tick time
        lastTickTime = Time.time;

        //Make a box at location 3 & 4 for now
        lcdGroundBoxes[0].SetActive(true);
        lcdGroundBoxes[4].SetActive(true);

        //Initialize the vehicle
        fuelCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for control input [Left,Right,Jump,Grab]
        CheckForInput();
        //Run PlayerUpdate
        currentPlayerState.PlayerUpdate(this);
        //Update Buttons
        UpdateButton1();
        UpdateButton2();

        //Move the vehicle
        MoveVehicle();
    }

    void ClearScreen()
    {
        ClearPlayers();
        //foreach (GameObject groundObject in grounds)
        //groundObject.SetActive(false);
        foreach (GameObject button1Object in lcdButton1)
            button1Object.SetActive(false);
        ClearHeldBoxes();
        ClearGroundBoxes();
        FuelGauge.instance.ClearFuelGauge();
    }
    void ClearPlayers()
    {
        foreach (GameObject playerObject in lcdPlayers)
            playerObject.SetActive(false);
    }
    void ClearHeldBoxes()
    {
        foreach (GameObject heldBoxObject in lcdHeldBoxes)
            heldBoxObject.SetActive(false);
    }
    void ClearGroundBoxes()
    {
        foreach (GameObject groundBoxObject in lcdGroundBoxes)
            groundBoxObject.SetActive(false);
    }
    void InitializeLcdObjects()
    {
        lcdPlayers[0].SetActive(true);
        SetButton1State(1);
        SetButton2(false);
        FuelGauge.instance.SetFuelGauge(fuel);
        Elevator.instance.SetElevatorState(0);
        SteamGauge.instance.SetSteamState(steam);
        TurnSmallWheel();
    }

    void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentPlayerState.MoveLeft(this);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentPlayerState.MoveRight(this);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            currentPlayerState.MoveUp(this);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            currentPlayerState.MoveDown(this);
        if (Input.GetKeyDown(KeyCode.Z))
            currentPlayerState.Grab(this);
        if (Input.GetKeyDown(KeyCode.Space))
            currentPlayerState.Jump(this);
    }

    public void ChangePlayerState(PlayerBaseState state)
    {
        currentPlayerState = state;
        currentPlayerState.EnterState(this);
    }

    public void UpdatePlayerSprite(int spriteIndex)
    {
        ClearPlayers();
        lcdPlayers[spriteIndex].SetActive(true);
        if(playerHoldingBox)
        {
            ClearHeldBoxes();
            lcdHeldBoxes[spriteIndex].SetActive(true);
        }    
    }
    
    public void SetButton1State(int state)
    {
        button1State = state;
        button1StateChangedTime = Time.time;
        switch (state)
        {
            case 1:
                Debug.Log("Button1 unpushed");
                //Button Fully unpushed
                //activate the LCDs
                lcdButton1[0].SetActive(true);
                lcdButton1[1].SetActive(true);
                lcdButton1[2].SetActive(true);
                lcdButton1[3].SetActive(false);
                lcdButton1[4].SetActive(false);
                //Set the speed to 0. Later add it to wind;
                motorSpeed = 0;
                break;
            case 2:
                //Button partially pushed
                lcdButton1[0].SetActive(false);
                lcdButton1[1].SetActive(true);
                lcdButton1[2].SetActive(true);
                lcdButton1[3].SetActive(true);
                lcdButton1[4].SetActive(false);
                //Set the time to stay in this state
                button1StateTime = 6;
                //Set the Speed. Later this will combine with wind/sail
                motorSpeed = 1;
                break;
            case 3:
                //Button fully pushed
                lcdButton1[0].SetActive(false);
                lcdButton1[1].SetActive(false);
                lcdButton1[2].SetActive(true);
                lcdButton1[3].SetActive(false);
                lcdButton1[4].SetActive(true);
                //Set the time to stay in this state
                button1StateTime = 10;
                //Set the Speed. Later this will combine with wind/sail
                motorSpeed = 2;
                break;
            default:
                Debug.LogError("Invalid state sent to Button1");
                break;
        }
        UpdateSpeed();
    }

    public void LetOffSteam()
    {
        motorSpeed += steam;
        steam = 0;
        SteamGauge.instance.SetSteamState(steam);
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        //Combine motor and sail speed, check for brake, then update relevant lcds
        speed = motorSpeed + sailSpeed;
        if (brakeActive) speed = 0;
        Speedometer.instance.UpdateSpeedometer(speed);
        FrontFlap.instance.UpdateFrontFlap(speed);
    }

    void UpdateButton1()
    {
        if (pushingButton1)
            return;

        switch (button1State)
        {
            case 1:
                //Button Fully unpushed
                //Do nothing... For now.
                break;
            case 2:
                //Button partially pushed
                //make the vehicle go
                //Check if you should change states
                if (Time.time - button1StateChangedTime > button1StateTime)
                    SetButton1State(1);
                break;
            case 3:
                //Button fully pushed
                //make the vehicle go
                //Check if you should change states
                if (Time.time - button1StateChangedTime > button1StateTime)
                    SetButton1State(2);
                break;
            default:
                Debug.LogError("Invalid state sent to Button1");
                break;
        }
    }

    public void SetButton2(bool pushed)
    {
        if (pushed)
        {
            Debug.Log("pushed button 1");
            button2Pushed = true;
            //Update LCDs
            lcdButton2[0].SetActive(false);
            lcdButton2[1].SetActive(true);
            lcdButton2[2].SetActive(true);
            lcdLifts[0].SetActive(false);
            lcdLifts[1].SetActive(true);
            button2PushedTime = Time.time;
            //Add Fuel
            if (lcdGroundBoxes[9].activeSelf)
            {
                Debug.Log("Consumed Fuel");
                lcdGroundBoxes[9].SetActive(false);
                AddFuel(1);
            }
        }
        else
        {
            //Button2 extended
            button2Pushed = false;
            //Update LCDs
            lcdButton2[0].SetActive(true);
            lcdButton2[1].SetActive(true);
            lcdButton2[2].SetActive(false);
            lcdLifts[0].SetActive(true);
            lcdLifts[1].SetActive(false);
        }

    }
    void UpdateButton2()
    {
        if (!button2Pushed)
            return;
        else
        {
            if (Time.time - button2PushedTime > button2Delay)
                SetButton2(false);
        }
    }
    
    void AddFuel(int amount)
    {
        if (fuel < 6)
            fuel += amount;
        FuelGauge.instance.SetFuelGauge(fuel);
    }
    public void PickUpBox(int state, bool pickup)
    {
        if (pickup)
        {
            Debug.Log("Picking up a box");
            playerHoldingBox = true;
            lcdHeldBoxes[state].SetActive(true);
            lcdGroundBoxes[state].SetActive(false);
        }
        else
        {
            Debug.Log("Dropping up a box");
            playerHoldingBox = false;
            lcdHeldBoxes[state].SetActive(false);
            lcdGroundBoxes[state].SetActive(true);
        }
    }

    //Wheels
    void TurnSmallWheel()
    {
        if (smallWheelState)
        {
            lcdsmallWheel[0].SetActive(true);
            lcdsmallWheel[1].SetActive(false);
            lcdsmallWheel[2].SetActive(true);
        }
        else
        {
            lcdsmallWheel[0].SetActive(true);
            lcdsmallWheel[1].SetActive(true);
            lcdsmallWheel[2].SetActive(false);
        }
        smallWheelState = !smallWheelState;
    }

    //Moving the vehicle
    void MoveVehicle()
    {
        if (speed == 0)
            return;
        else if (fuel > 0)
        {
            tickSpeedTime = tickBaseTime/speed;
            if (Time.time - lastTickTime > tickSpeedTime)
            {
                //Debug.Log("Ticking");
                TurnSmallWheel();
                ConsumeFuel();
                lastTickTime = Time.time;
                audioSource.Play();
            }
        }
    }
    void ConsumeFuel()
    {
        //Consume fuel
        if (fuelCounter > fuelRollover)
        {
            fuel--;
            FuelGauge.instance.SetFuelGauge(fuel);
            fuelCounter = 0;
            if (fuel == 0)
                SetButton1State(1);
        }
        else
            fuelCounter++;
        //Make Steam
        if (steamCounter > steamRollover)
        {
            steam++;
            if (steam > 4)
            {
                Debug.Log("Explode");
                SetButton1State(1);
            }
            else
            {
                SteamGauge.instance.SetSteamState(steam);
                steamCounter = 0;
            }
        }
        else
            steamCounter++;

    }
}
