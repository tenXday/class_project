using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HardButton : MonoBehaviour
{
    [SerializeField] List<Button> Hardbutton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level1PassTime") >= 0)
        {
            Hardbutton[0].interactable = true;
        }
        if (PlayerPrefs.GetInt("Level1PassTime") >= 1)
        {
            Hardbutton[1].interactable = true;
        }
        if (PlayerPrefs.GetInt("Level2PassTime") >= 1)
        {
            Hardbutton[2].interactable = true;
        }
    }
    public void SetCurrentLevel(int currentlevel)
    {
        PlayerPrefs.SetInt("currentlevel", currentlevel);
    }
}
