using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    public AudioClip playerMove;
    public AudioClip vehicleTick;
    public AudioClip pickupBox;
    public AudioClip dropBox;
    public AudioClip sailsUp;
    public AudioClip fireAlarm;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayerMove()
    {
        audioSource.clip = playerMove;
        audioSource.Play();
    }

    public void VehicleTick()
    {
        audioSource.clip = vehicleTick;
        audioSource.Play();
    }

    public void PickupBox()
    {
        audioSource.clip = pickupBox;
        audioSource.Play();
    }

    public void DropBox()
    {
        audioSource.clip = dropBox;
        audioSource.Play();
    }
    public void SailsUp()
    {
        audioSource.clip = sailsUp;
        audioSource.Play();
    }
    public void FireAlarm()
    {
        audioSource.clip = fireAlarm;
        audioSource.Play();
    }
}
