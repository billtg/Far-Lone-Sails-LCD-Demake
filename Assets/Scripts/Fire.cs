using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public static Fire instance;

    public List<GameObject> lcdFuelFire;
    public List<GameObject> lcdMotorFire;
    public List<GameObject> lcdSailsFire;

    public float fireAnimationDelay;
    float fuelFireAnimationTimeSet;
    float motorFireAnimationTimeSet;
    float sailsFireAnimationTimeSet;

    public float fireDamageDelay;
    float timeSinceDamageFuel;
    float timeSinceDamageMotor;
    float timeSinceDamageSails;

    public bool fuelOnFire;
    public bool motorOnFire;
    public bool sailsOnFire;

    private void Awake()
    {
        instance = this;
        fuelFireAnimationTimeSet = Time.time;
        motorFireAnimationTimeSet = Time.time;
        sailsFireAnimationTimeSet = Time.time;
    }

    public void ClearFireLCDs(bool active)
    {
        foreach (GameObject fireObject in lcdFuelFire)
            fireObject.SetActive(active);
        foreach (GameObject fireObject in lcdMotorFire)
            fireObject.SetActive(active);
        foreach (GameObject fireObject in lcdSailsFire)
            fireObject.SetActive(active);
    }

    public void CatchFire(HealthBar onFire)
    {
        //Start a fire on the input onFire
        switch (onFire)
        {
            case HealthBar.fuel:
                if (fuelOnFire)
                    break;
                fuelOnFire = true;
                timeSinceDamageFuel = Time.time;
                break;
            case HealthBar.motor:
                if (motorOnFire)
                    break;
                motorOnFire = true;
                timeSinceDamageMotor = Time.time;
                break;
            case HealthBar.sails:
                if (sailsOnFire)
                    break;
                sailsOnFire = true;
                timeSinceDamageSails = Time.time;
                break;
        }
        //Play the fire alarm
        AudioManager.instance.FireAlarm();
    }

    public void ResetFires()
    {
        fuelOnFire = false;
        motorOnFire = false;
        sailsOnFire = false;
    }

    public void DouseFire(HealthBar dousedFire)
    {
        switch (dousedFire)
        {
            case HealthBar.fuel:
                fuelOnFire = false;
                break;
            case HealthBar.motor:
                motorOnFire = false;
                break;
            case HealthBar.sails:
                sailsOnFire = false;
                break;
        }
        ClearFireLCDs(false);
        AudioManager.instance.FiresOut();
    }


    // Update is called once per frame
    void Update()
    {
        //Animate the fire and check for damage
        if (fuelOnFire)
        {
            AnimateFire(HealthBar.fuel);
            CheckForFireDamage(HealthBar.fuel);
        }
        if (sailsOnFire)
        {
            AnimateFire(HealthBar.sails);
            CheckForFireDamage(HealthBar.sails);
        }
        if (motorOnFire)
        {
            AnimateFire(HealthBar.motor);
            CheckForFireDamage(HealthBar.motor);
        }
    }

    void CheckForFireDamage(HealthBar damageCheck)
    {
        switch (damageCheck)
        {
            case HealthBar.fuel:
                if (Time.time - timeSinceDamageFuel > fireDamageDelay)
                {
                    Health.instance.TakeDamage(HealthBar.fuel);
                    timeSinceDamageFuel = Time.time;
                }
                break;
            case HealthBar.motor:
                if (Time.time - timeSinceDamageMotor > fireDamageDelay)
                {
                    Health.instance.TakeDamage(HealthBar.motor);
                    timeSinceDamageMotor = Time.time;
                }
                break;
            case HealthBar.sails:
                if (Time.time - timeSinceDamageSails > fireDamageDelay)
                {
                    Health.instance.TakeDamage(HealthBar.sails);
                    timeSinceDamageSails = Time.time;
                }
                break;
        }
    }

    void AnimateFire(HealthBar animateFire)
    {
        //activate the Fire LCDs randomly after random 0 to 2 second interval
        switch(animateFire)
        {
            case HealthBar.fuel:
                if (Time.time - fuelFireAnimationTimeSet > fireAnimationDelay)
                {
                    for (int i = 0; i < lcdFuelFire.Count; i++)
                        lcdFuelFire[i].SetActive(Random.Range(0, 2) == 0);
                    fuelFireAnimationTimeSet = Time.time;
                }
                break;
            case HealthBar.motor:
                if (Time.time - motorFireAnimationTimeSet > fireAnimationDelay)
                {
                    for (int i = 0; i < lcdFuelFire.Count; i++)
                        lcdMotorFire[i].SetActive(Random.Range(0, 2) == 0);
                    motorFireAnimationTimeSet = Time.time;
                }
                break;
            case HealthBar.sails:
                if (Time.time - sailsFireAnimationTimeSet > fireAnimationDelay)
                {
                    for (int i = 0; i < lcdFuelFire.Count; i++)
                        lcdSailsFire[i].SetActive(Random.Range(0, 2) == 0);
                    sailsFireAnimationTimeSet = Time.time;
                }
                break;
        }
    }
}
