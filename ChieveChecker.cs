using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChieveChecker : MonoBehaviour
{
    [SerializeField] string Chievename;
    [SerializeField] Button chievebutton;
    [SerializeField] Image chieveBG;
    void Start()
    {

        if (PlayerPrefs.GetInt(Chievename) == 0)
        {
            GetComponent<Image>().color = Color.HSVToRGB(0, 0, 0.5f);
            chieveBG.color = Color.HSVToRGB(0, 0, 0.5f);
            chievebutton.interactable = false;
        }
        else
        {
            GetComponent<Image>().color = Color.HSVToRGB(0, 0, 1);
            chieveBG.color = Color.HSVToRGB(0, 0, 1);
            chievebutton.interactable = true;
        }
    }
    public void ClearChieve()
    {
        PlayerPrefs.DeleteAll();
    }
}
