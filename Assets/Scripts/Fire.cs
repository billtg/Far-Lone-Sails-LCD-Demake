using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public static Fire instance;

    public List<GameObject> lcdFuelFire;
    public List<GameObject> lcdMotorFire;
    public List<GameObject> lcdSailsFire;

    public bool fuelOnFire;
    public bool motorOnFire;
    public bool sailsOnFire;

    private void Awake()
    {
        instance = this;
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
                break;
            case HealthBar.motor:
                if (motorOnFire)
                    break;
                motorOnFire = true;
                break;
            case HealthBar.sails:
                if (sailsOnFire)
                    break;
                sailsOnFire = true;
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        //Put out fires
        //Animate the fire
        //Damage anything on fire

    }
}
