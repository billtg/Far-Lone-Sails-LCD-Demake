using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public static Elevator instance;
    public int elevatorState;
    public List<GameObject> lcdElevator;

    private void Awake()
    {
        instance = this;
    }

    public void SetElevatorState(int state)
    {
        ClearElevator();
        if (state < 0 || state > 3)
            Debug.LogError("Invalid State sent to SetElevatorState: " + state.ToString());
        //Check for ground boxes and take them along
        if (GameManager.instance.lcdGroundBoxes[elevatorState].activeSelf)
        {
            GameManager.instance.lcdGroundBoxes[elevatorState].SetActive(false);
            GameManager.instance.lcdGroundBoxes[state].SetActive(true);
        }
        lcdElevator[state].SetActive(true);
        elevatorState = state;
    }

    public void ClearElevator()
    {
        foreach (GameObject elevatorObject in lcdElevator)
            elevatorObject.SetActive(false);
    }
}
