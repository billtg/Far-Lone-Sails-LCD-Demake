using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> lcdPlayers;
    public List<GameObject> lcdGroundBoxes;
    public List<GameObject> lcdHeldBoxes;
    public List<GameObject> lcdGrounds;
    public List<GameObject> lcdButton1;
    public List<GameObject> lcdButton2;
    public List<GameObject> lcdLifts;
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
    public PlayerState11 ps11 = new PlayerState11();
    public PlayerState12 ps12 = new PlayerState12();
    public PlayerState13 ps13 = new PlayerState13();
    public PlayerState14 ps14 = new PlayerState14();
    public PlayerState15 ps15 = new PlayerState15();
    public PlayerState16 ps16 = new PlayerState16();
    public PlayerState17 ps17 = new PlayerState17();
    public PlayerState18 ps18 = new PlayerState18();
    public PlayerState19 ps19 = new PlayerState19();
    public PlayerState20 ps20 = new PlayerState20();
    public PlayerState21 ps21 = new PlayerState21();
    public PlayerState22 ps22 = new PlayerState22();
    public PlayerState23 ps23 = new PlayerState23();
    public PlayerState24 ps24 = new PlayerState24();
    public PlayerState25 ps25 = new PlayerState25();
    public PlayerState26 ps26 = new PlayerState26();
    public PlayerState27 ps27 = new PlayerState27();
    public PlayerState28 ps28 = new PlayerState28();
    public PlayerState29 ps29 = new PlayerState29();
    public PlayerState30 ps30 = new PlayerState30();
    public PlayerState31 ps31 = new PlayerState31();
    public PlayerState32 ps32 = new PlayerState32();
    public PlayerState33 ps33 = new PlayerState33();
    public PlayerState34 ps34 = new PlayerState34();
    public PlayerState35 ps35 = new PlayerState35();
    public PlayerState36 ps36 = new PlayerState36();
    public PlayerState37 ps37 = new PlayerState37();
    public PlayerState38 ps38 = new PlayerState38();
    public PlayerState39 ps39 = new PlayerState39();
    public PlayerState40 ps40 = new PlayerState40();
    public PlayerState41 ps41 = new PlayerState41();
    public PlayerState42 ps42 = new PlayerState42();
    public PlayerState43 ps43 = new PlayerState43();
    public PlayerState44 ps44 = new PlayerState44();

    //Gameplay values
    bool gameOver;
    public float gameOverDelay = 3;
    public float playerHangTime;
    public bool playerHoldingBox;
    public bool playerHoldingNozzle;
    public bool playerHoldingWelder;
    public int fuel;
    public bool vehicleMoving;
    public int speed;
    public int odometerAmount;
    public bool gateBlocking;

    //Box Spawning
    public int spawnBoxIndex;
    public int spawnBoxChance;

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
    public int motorSpeed;

    //Steam
    public int steam;
    public bool steamActive;
    public int steamCounter;
    public int steamRollover;
    public int steamPower;
    public float steamPowerDelay;
    public float steamPowerAdded;

    //Brake
    public bool brakeActive;

    //sails
    public int sailSpeed;
    public int wind;
    public bool sailsUp;
    float windSetTime;
    public float windChangeTime;


    AudioSource audioSource;
    public AudioSource gameOverAudio;
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
        lcdGroundBoxes[29].SetActive(true);

        //Initialize the vehicle
        fuelCounter = 0;
        steamCounter = 0;
        odometerAmount = 0;

        //Set the wind set time
        windSetTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;

        //Check for control input [Left,Right,Jump,Grab]
        CheckForInput();
        //Run PlayerUpdate
        currentPlayerState.PlayerUpdate(this);
        //Update Buttons
        UpdateButton1();
        UpdateButton2();

        //Update the steam
        UpdateSteam();

        //Move the vehicle
        MoveVehicle();

        //Check for wind changes
        UpdateWind();
    }

    void ClearScreen()
    {
        ClearPlayers();
        foreach (GameObject button1Object in lcdButton1)
            button1Object.SetActive(false);
        ClearHeldBoxes();
        ClearGroundBoxes();
        FuelGauge.instance.ClearFuelGauge();
        Flag.instance.ClearFlag();
        Sails.instance.ClearSails();
        FireHose.instance.ClearFireHoses();
        FireHose.instance.ClearWaterLCDs();
        FireHose.instance.ClearWeldingLCDs();
        Health.instance.ClearHealthLCDs();
        Fire.instance.ClearFireLCDs();
        Gate.instance.ClearGate();
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
        Brake.instance.Button4Pushed(false);
        Flag.instance.SetFlagState(wind);
        Sails.instance.SetSails(0);
        FireHose.instance.InitializeFireHose();
        Health.instance.UpdateHealthBarLCD();
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
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    public void ChangePlayerState(PlayerBaseState state)
    {
        currentPlayerState = state;
        currentPlayerState.EnterState(this);
        //Play the player move audio
        AudioManager.instance.PlayerMove();
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
        if (fuel == 0)
            button1State = 1;
        else
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
        if (motorSpeed == 0)
        {
            steam = 0;
            steamPower = 0;
            //Vent steam animation
        }
        else
        {
            //Only add to increase
            if (steamPower < steam)
            {
                steamActive = true;
                steamPowerAdded = Time.time;
                if (steam == 2 || steam == 3)
                    steamPower = 1;
                if (steam == 4)
                    steamPower = 2;
                steam = 0;
            }
            else
                steam = 0;
        }
        steamCounter = 0;
        SteamGauge.instance.SetSteamState(steam);
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        //Combine motor and sail speed, check for brake, then update relevant lcds
        //Steampower is only added when the motor is running.
        if (motorSpeed > 0)
            speed = motorSpeed + steamPower + sailSpeed;
        else
            speed = sailSpeed;
        //Negate speed when the brake is on, or gate blocking
        if (brakeActive || gateBlocking) speed = 0;
        //Update the speedometer and the flap
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

            //Record the time
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
        //Unpush the button after a time delay
        if (!button2Pushed)
            return;
        else
        {
            //See if time has elapsed. Delay is scaled according to health;
            if (Time.time - button2PushedTime > button2Delay*Health.instance.fuelDelayFactor)
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
            AudioManager.instance.PickupBox();
        }
        else
        {
            Debug.Log("Dropping up a box");
            playerHoldingBox = false;
            lcdHeldBoxes[state].SetActive(false);
            lcdGroundBoxes[state].SetActive(true);
            AudioManager.instance.DropBox();
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
        else
        {
            tickSpeedTime = tickBaseTime/speed;
            if (Time.time - lastTickTime > tickSpeedTime)
            {
                //Debug.Log("Ticking");
                TurnSmallWheel();
                lastTickTime = Time.time;
                AudioManager.instance.VehicleTick();
                //If the motor is contributing, consume fuel
                if (motorSpeed > 0)
                    ConsumeFuel();
                MoveGroundItems();
                SpawnBoxes();
                odometerAmount++;
                if (odometerAmount == 10000)
                    Debug.Log("WINN");
                else
                    Odometer.instance.UpdateOdometer(odometerAmount);

                //Spawn a gate. Currently happens at 20 at 100 m.
                if (odometerAmount == Gate.instance.ticksToGate || odometerAmount == 100)
                    Gate.instance.SpawnGate();
                if (Gate.instance.gateSpawned)
                    Gate.instance.MoveGate();
            }
        }
    }

    void MoveGroundItems()
    {
        //Check for Game Over
        if (currentPlayerState.GameOver())
            GameOver();
        //Move the Player
        if (currentPlayerState.IsOnGround())
            currentPlayerState.MoveLeft(this);
        //Move the boxes. It's gonna be a mess
        //First, clear 17, then move 15,16, 18, then undercar, then aheadcar
        if (lcdGroundBoxes[17].activeSelf)
            lcdGroundBoxes[17].SetActive(false);
        if (lcdGroundBoxes[16].activeSelf)
        {
            lcdGroundBoxes[17].SetActive(true);
            lcdGroundBoxes[16].SetActive(false);
        }
        if (lcdGroundBoxes[15].activeSelf)
        {
            lcdGroundBoxes[16].SetActive(true);
            lcdGroundBoxes[15].SetActive(false);
        }
        if (lcdGroundBoxes[18].activeSelf)
        {
            lcdGroundBoxes[15].SetActive(true);
            lcdGroundBoxes[18].SetActive(false);
        }

        //Undercar
        for (int i = 19; i <= 23; i++)
        {
            if (lcdGroundBoxes[i].activeSelf)
            {
                lcdGroundBoxes[i-1].SetActive(true);
                lcdGroundBoxes[i].SetActive(false);
            }
        }
        //Aheadcar
        if (lcdGroundBoxes[27].activeSelf)
        {
            lcdGroundBoxes[23].SetActive(true);
            lcdGroundBoxes[27].SetActive(false);
        }
        for (int i = 28; i <= 29; i++)
        {
            if (lcdGroundBoxes[i].activeSelf)
            {
                lcdGroundBoxes[i-1].SetActive(true);
                lcdGroundBoxes[i].SetActive(false);
            }
        }

    }
    void ConsumeFuel()
    {
        //Consume fuel. Update the fuel gauge when rollover point is reached.
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
                SteamExplosion();
            else
            {
                SteamGauge.instance.SetSteamState(steam);
                steamCounter = 0;
            }
        }
        else if (!steamActive)
            steamCounter++;

    }

    public void DrainFuel()
    {
        if (fuel > 0)
        {
            fuel--;
            FuelGauge.instance.SetFuelGauge(fuel);
            if (fuel == 0)
                SetButton1State(1);
        }
    }

    void SteamExplosion()
    {
        //Steam got too high.
        Debug.Log("Steam Explosion");

        //Vent all the steam. Damage the motor. 
        steam = 0;
        steamPower = 0;
        SteamGauge.instance.SetSteamState(steam);
        SetButton1State(1);

        //Add Damage.
        Health.instance.TakeDamage(HealthBar.fuel);

        //Set the fuel thing on fire
        Fire.instance.CatchFire(HealthBar.fuel);

        //Play the fire alarm
        AudioManager.instance.FireAlarm();
    }

    void UpdateSteam()
    {
        if (!steamActive) return;
        if (Time.time - steamPowerAdded > steamPowerDelay)
        {
            steamPower = 0;
            steamActive = false;
            UpdateSpeed();
        }
    }

    public void SetBrake(bool brakeOn)
    {
        if (brakeOn)
        {
            brakeActive = true;
            SetButton1State(1);
        }
        else
            brakeActive = false;
        UpdateSpeed();
    }

    void SpawnBoxes()
    {
        int spawnRoll = (int)Random.Range(0, 10);
        if (spawnRoll < spawnBoxChance)
        {
            Debug.Log("Spawning Box");
            lcdGroundBoxes[spawnBoxIndex].SetActive(true);
        }    
    }
    void GameOver()
    {
        gameOver = true;
        gameOverAudio.Play();

        //Restart after a delay
        StartCoroutine(DelayedRestart());
    }

    IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(0);
    }

    public void ChangeSail()
    {
        sailsUp = !sailsUp;
        if (sailsUp)
        {
            Sails.instance.SetSails(2);
            sailSpeed = wind;
            UpdateSpeed();
        }
        else
        {
            Sails.instance.SetSails(0);
            sailSpeed = 0;
            UpdateSpeed();
        }
        AudioManager.instance.SailsUp();
    }

    void UpdateWind()
    {
        if (Time.time - windSetTime > windChangeTime)
        {
            Debug.Log("Changing wind");
            wind = Random.Range(0, 3);
            Flag.instance.SetFlagState(wind);
            if (sailsUp)
                sailSpeed = wind;
            UpdateSpeed();
            windSetTime = Time.time;
        }
    }

    public void PickUpNozzle()
    {
        FireHose.instance.PickupNozzle();
        playerHoldingNozzle = true;
    }

    public void DropNozzle()
    {
        FireHose.instance.DropNozzle();
        playerHoldingNozzle = false;
    }

    public void PickUpWelder()
    {
        FireHose.instance.PickupWelder();
        playerHoldingWelder = true;
    }

    public void DropWelder()
    {
        FireHose.instance.DropWelder();
        playerHoldingWelder = false;
    }

    public void HitGate()
    {
        Debug.Log("Hit the gate");
        //Stop all movement
        SetButton1State(1);
        gateBlocking = true;
        UpdateSpeed();

        //Catch the button on fire
        Fire.instance.CatchFire(HealthBar.motor);
    }

    public void GateOpen()
    {
        gateBlocking = false;
        UpdateSpeed();
    }

    public void GateMovesWithPlayer()
    {
        if (currentPlayerState == ps40 || 
            currentPlayerState == ps41 || 
            currentPlayerState == ps42 || 
            currentPlayerState == ps43 ||
            currentPlayerState == ps44)
        {
            ChangePlayerState(ps39);
        }
    }

    public void HitSails()
    {
        //Drop the sail, set them on fire
        ChangeSail();
        Fire.instance.CatchFire(HealthBar.sails);
    }
}
