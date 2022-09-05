using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;

    public int sailHealth;
    public int motorHealth;
    public int fuelHealth;

    public List<GameObject> lcdSailHealth;
    public List<GameObject> lcdMotorHealth;
    public List<GameObject> lcdFuelHealth;
    public List<GameObject> lcdSymbols;

    public float fuelDelayFactor =1;
    public float sailDelayFactor = 1;
    public float motorDelayFactor = 1;

    private void Awake()
    {
        instance = this;
    }

    public void ClearHealthLCDs(bool active)
    {
        foreach (GameObject healthObject in lcdSailHealth)
            healthObject.SetActive(active);
        foreach (GameObject healthObject in lcdMotorHealth)
            healthObject.SetActive(active);
        foreach (GameObject healthObject in lcdFuelHealth)
            healthObject.SetActive(active);
        foreach (GameObject healthObject in lcdSymbols)
            healthObject.SetActive(active);
    }

    public void UpdateHealth(HealthBar healthBar, int value)
    {
        if (value > 3 || value < 0)
            Debug.LogError("Invalid health value sent to UpdateHealth: " + value.ToString());
        switch (healthBar)
        {
            case HealthBar.sails:
                sailHealth = value;
                //Drop the sail if you're out of sail health
                if (sailHealth == 0 && GameManager.instance.sailsUp)
                    GameManager.instance.ChangeSail();
                if (sailHealth > 0)
                    sailDelayFactor = 3f / sailHealth;
                break;
            case HealthBar.motor:
                motorHealth = value;
                if (motorHealth > 0)
                    motorDelayFactor = 3f / motorHealth;
                break;
            case HealthBar.fuel:
                fuelHealth = value;
                if (fuelHealth > 0)
                    fuelDelayFactor = 3f / fuelHealth;
                break;
        }
        UpdateHealthBarLCD();
    }

    public void TakeDamage(HealthBar healthBar)
    {
        switch (healthBar)
        {
            case HealthBar.sails:
                if (sailHealth - 1 >= 0)
                    UpdateHealth(HealthBar.sails, sailHealth - 1);
                break;
            case HealthBar.motor:
                if (motorHealth - 1 >= 0)
                    UpdateHealth(HealthBar.motor, motorHealth - 1);
                break;
            case HealthBar.fuel:
                if (fuelHealth - 1 >= 0)
                    UpdateHealth(HealthBar.fuel, fuelHealth - 1);
                break;
        }
    }

    public void RemoveDamage(HealthBar healthBar)
    {
        switch (healthBar)
        {
            case HealthBar.sails:
                if (sailHealth < 3)
                    UpdateHealth(HealthBar.sails, sailHealth + 1);
                break;
            case HealthBar.motor:
                if (motorHealth < 3)
                    UpdateHealth(HealthBar.motor, motorHealth + 1);
                break;
            case HealthBar.fuel:
                if (fuelHealth < 3)
                    UpdateHealth(HealthBar.fuel, fuelHealth + 1);
                break;
        }
    }

    public void UpdateHealthBarLCD()
    {
        //Clear everything, then initialize the symbols and the relevant health bars
        ClearHealthLCDs(false);
        foreach (GameObject healthObject in lcdSymbols)
            healthObject.SetActive(true);

        //Set the sail health bar
        if (sailHealth != 0)
            SetSpecificHealthBar(sailHealth, lcdSailHealth);

        //Set the motor health bar
        if (motorHealth != 0)
            SetSpecificHealthBar(motorHealth, lcdMotorHealth);

        //Set the sail health bar
        if (fuelHealth != 0)
            SetSpecificHealthBar(fuelHealth, lcdFuelHealth);
    }

    void SetSpecificHealthBar(int health, List<GameObject> lcdList)
    {
        for (int i = 0; i < health; i++)
        {
            lcdList[i].SetActive(true);
        }
    }

    public bool AtFullHealth()
    {
        if (fuelHealth == 3 && motorHealth == 3 && sailHealth == 3)
            return true;
        else
            return false;
    }
}

public enum HealthBar { sails, motor, fuel}
