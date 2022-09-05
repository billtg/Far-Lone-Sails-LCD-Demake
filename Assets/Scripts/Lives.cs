using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public static Lives instance;
    public List<GameObject> lcdLives;

    private void Awake()
    {
        instance = this;
    }
    public void ClearLivesLCDs(bool active)
    {
        foreach (GameObject lcdObject in lcdLives)
            lcdObject.SetActive(active);
    }

    public void UpdateLives(int lives)
    {
        switch (lives)
        {
            case 0:
                lcdLives[0].SetActive(false);
                lcdLives[1].SetActive(false);
                lcdLives[2].SetActive(false);
                break;
            case 1:
                lcdLives[0].SetActive(true);
                lcdLives[1].SetActive(false);
                lcdLives[2].SetActive(false);
                break;
            case 2:
                lcdLives[0].SetActive(true);
                lcdLives[1].SetActive(true);
                lcdLives[2].SetActive(false);
                break;
            case 3:
                lcdLives[0].SetActive(true);
                lcdLives[1].SetActive(true);
                lcdLives[2].SetActive(true);
                break;
            default:
                Debug.LogError("Incorrect Lives value sent to UpdateLives: " + lives.ToString());
                break;

        }
    }
}
