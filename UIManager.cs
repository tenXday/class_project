using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Color UnClearColor;
    private GameObject UIHp, UIHpBar, Player, Eventname;
    private List<GameObject> UIMission;
    private float Health, HealthMax;
    void OnEnable()
    {
        PlayboardEvent.MissionEnd += MissionEnd;
        PlayboardEvent.HealthMinus += GetAttack;
    }
    void OnDisable()
    {
        PlayboardEvent.MissionEnd -= MissionEnd;
        PlayboardEvent.HealthMinus -= GetAttack;
    }
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        HealthMax = Player.GetComponent<Player>().Hp;

        UIHp = GameObject.Find("HealthPrecent");
        UIHpBar = GameObject.Find("HealthBar");
        Health = HealthMax;
        UIHp.GetComponent<TMP_Text>().SetText(Health / HealthMax * 100 + "%");
        UIHpBar.GetComponent<Image>().fillAmount = Health / HealthMax;

        UIMission = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            UIMission.Add(GameObject.Find("MissionIcon" + i));
            UIMission[i].GetComponent<Image>().color = UnClearColor;
        }

        Eventname = GameObject.Find("Eventname");

        if (PlayerPrefs.GetInt("currentlevel") == 0)
        {
            Eventname.GetComponent<TMP_Text>().SetText("X公路危機 1/3");
        }
        if (PlayerPrefs.GetInt("currentlevel") == 1)
        {
            Eventname.GetComponent<TMP_Text>().SetText("X公路危機 2/3");
        }
        if (PlayerPrefs.GetInt("currentlevel") == 2)
        {
            Eventname.GetComponent<TMP_Text>().SetText("X公路危機 3/3");
        }

    }

    private void GetAttack(float health)
    {

        Health -= health;
        UIHp.GetComponent<TMP_Text>().SetText(Health / HealthMax * 100 + "%");
        UIHpBar.GetComponent<Image>().fillAmount = Health / HealthMax;
    }

    private void MissionEnd(int missionID)
    {
        UIMission[missionID].GetComponent<Image>().color = Color.white;
    }

    public void Pause()
    {
        PlayboardEvent.CallGamePause();
    }
    public void Continue()
    {
        PlayboardEvent.CallGameContinue();
    }
}
