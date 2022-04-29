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
    float fireAnimationTimeSet;
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
        fireAnimationTimeSet = Time.time;
    }

    public void ClearFireLCDs()
    {
        foreach (GameObject fireObject in lcdFuelFire)
            fireObject.SetActive(false);
        foreach (GameObject fireObject in lcdMotorFire)
            fireObject.SetActive(false);
        foreach (GameObject fireObject in lcdSailsFire)
            fireObject.SetActive(false);
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
        ClearFireLCDs();
    }


    // Update is called once per frame
    void Update()
    {
        //Animate the fire and check for damage
        if (fuelOnFire)
        {
            AnimateFire(lcdFuelFire);
            CheckForFireDamage(HealthBar.fuel);
        }
        if (sailsOnFire)
        {
            AnimateFire(lcdSailsFire);
            CheckForFireDamage(HealthBar.sails);
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

    void AnimateFire(List<GameObject> fireLCDs)
    {
        //After a time delay, randomly set each of the lcds in the fire, then reset the timer
        if (Time.time - fireAnimationTimeSet > fireAnimationDelay)
        {
            for (int i = 0; i < fireLCDs.Count; i++)
                fireLCDs[i].SetActive(Random.Range(0, 2) == 0);
            fireAnimationTimeSet = Time.time;
        }
    }
}
