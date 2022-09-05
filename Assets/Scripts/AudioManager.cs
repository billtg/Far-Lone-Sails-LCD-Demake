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
    public AudioClip fireHose;
    public int fireHosePriority;
    public AudioClip steamWhistle;
    public int steamWhistlePriority;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
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
    public void SteamWhistle()
    {
        PlayWithPriority(steamWhistlePriority, steamWhistle, false);
    }
    
}
