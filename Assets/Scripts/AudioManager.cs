using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    [Header("Audio Priority")]
    public int currentClipPriority;

    public AudioClip playerMove;
    public int playerMovePriority;
    public AudioClip vehicleTick;
    public int vehicleTickPriority;
    public AudioClip pickupBox;
    public int pickupBoxPriority;
    public AudioClip dropBox;
    public int dropBoxPriority;
    public AudioClip sailsUp;
    public int sailsUpPriority;
    public AudioClip fireAlarm;
    public int fireAlarmPriority;
    public AudioClip brakeApplied;
    public int brakeAppliedPriority;

    public AudioClip fuelLoad;
    public int fuelLoadPriority;
    public AudioClip gateFuelLoad;
    public int gateFuelLoadPriority;
    public AudioClip gateOpen;
    public int gateOpenPriority;
    public AudioClip fireHose;
    public int fireHosePriority;
    public AudioClip firesOut;
    public int firesOutPriority;
    public AudioClip welding;
    public int weldingPriority;
    public AudioClip steamWhistle;
    public int steamWhistlePriority;

    public AudioClip buttonPushing1;
    public AudioClip buttonPushing2;
    public int buttonPushingPriority;

    public AudioClip death;
    public int deathPriority;
    public AudioClip gameOver;
    public int gameOverPriority;

    public AudioClip finale;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Mute()
    {
        audioSource.mute = !audioSource.mute;
    }

    void PlayWithPriority(int priority, AudioClip audioClip, bool loop)
    {
        //Play if nothing else is playing, otherwise play the highest priority
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            currentClipPriority = priority;
            audioSource.loop = loop;
        }
        else
        {
            if (priority <= currentClipPriority)
            {
                //New clip is higher/equal priority
                audioSource.clip = audioClip;
                audioSource.Play();
                currentClipPriority = priority;
                audioSource.loop = loop;
            }
            else
            {
                //Debug.Log("Lower priority clip not played");
                return;
            }
        }

    }
    public void PlayerMove()
    {
        PlayWithPriority(playerMovePriority, playerMove, false);
    }

    public void VehicleTick()
    {
        PlayWithPriority(vehicleTickPriority, vehicleTick, false);
    }

    public void PickupBox()
    {
        PlayWithPriority(pickupBoxPriority, pickupBox, false);
    }

    public void DropBox()
    {
        PlayWithPriority(dropBoxPriority, dropBox, false);
    }
    public void SailsUp()
    {
        PlayWithPriority(sailsUpPriority, sailsUp, false);
    }
    public void FireAlarm()
    {
        PlayWithPriority(fireAlarmPriority, fireAlarm, false);
    }
    public void BrakeApplied()
    {
        PlayWithPriority(brakeAppliedPriority, brakeApplied, false);
    } 
    public void FuelLoad()
    {
        PlayWithPriority(fuelLoadPriority, fuelLoad, false);
    }
    public void FireHose()
    {
        PlayWithPriority(fireHosePriority, fireHose, true);
    }
    public void StopHosing()
    {
        Debug.Log("Stop Audio Hosing");
        audioSource.loop = false;
        //Play Fire out sound instead
        if (audioSource.clip == fireHose)
            audioSource.Stop();
    }

    public void FiresOut()
    {
        PlayWithPriority(firesOutPriority, firesOut, false);
    }
    public void SteamWhistle()
    {
        PlayWithPriority(steamWhistlePriority, steamWhistle, false);
    }

    public void ButtonPushing1() { PlayWithPriority(buttonPushingPriority, buttonPushing1, true); }

    public void ButtonPushing2() { PlayWithPriority(buttonPushingPriority, buttonPushing2, true); }

    public void StopButtonPushing()
    {
        Debug.Log("Stopping Button Audio");
        audioSource.loop = false;
        if (audioSource.clip == buttonPushing1 || audioSource.clip == buttonPushing2)
            audioSource.Stop();
    }

    public void Death() { PlayWithPriority(deathPriority, death, false); }
    public void GameOver() { PlayWithPriority(gameOverPriority, gameOver, false); }
    public void GateFuelLoad() { PlayWithPriority(gateFuelLoadPriority, gateFuelLoad, false); }
    public void GateOpen() { PlayWithPriority(gateOpenPriority, gateOpen, true); }
    public void StopGateOpen()
    {
        audioSource.loop = false;
        if (audioSource.clip == gateOpen)
            audioSource.Stop();
    }
    public void Welding() { PlayWithPriority(weldingPriority, welding, true); }
    public void StopWelding()
    {
        audioSource.loop = false;
        if (audioSource.clip == welding)
            audioSource.Stop();
    }
    
    public void Finale() { PlayWithPriority(0, finale, false); }
}
