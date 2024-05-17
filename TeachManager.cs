using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeachManager : MonoBehaviour
{

    [Serializable]
    public struct TeachShow
    {
        public string SecondTitle;
        public String teachcontent;
        public Sprite teachimage;

    }
    [SerializeField] GameObject Endcanvas;
    [SerializeField] List<TeachShow> teachShow;
    private GameObject teachimage_UI, teachcontent_UI, secondtitle;
    private GameObject backbutton, nextbutton;
    [SerializeField] private int currentshow;
    public bool Toverflag;

    void Start()
    {
        teachimage_UI = GameObject.Find("TeachIMG");
        teachcontent_UI = GameObject.Find("TeachContent");
        backbutton = GameObject.Find("BackButton");
        nextbutton = GameObject.Find("NextButton");
        secondtitle = GameObject.Find("Title2");

        backbutton.SetActive(false);
        currentshow = 0;

        Show();
    }

    void Show()
    {
        if (currentshow < teachShow.Count)
        {
            Toverflag = false;
        }
        if (currentshow <= 0)
        {
            currentshow = 0;
            backbutton.SetActive(false);
        }
        else if (currentshow > 0)
        {
            backbutton.SetActive(true);
            nextbutton.SetActive(true);
            if (currentshow == teachShow.Count)
            {
                Toverflag = true;

            }
        }
        if (!Toverflag)
        {
            secondtitle.GetComponent<TMP_Text>().SetText(teachShow[currentshow].SecondTitle);
            teachcontent_UI.GetComponent<TMP_Text>().SetText(teachShow[currentshow].teachcontent);
            teachimage_UI.GetComponent<Image>().sprite = teachShow[currentshow].teachimage;
        }
        else
        {
            Endcanvas.SetActive(true);
        }

    }
    public void Back()
    {
        currentshow--;
        Show();
    }
    public void Next()
    {
        currentshow++;
        Show();
    }
}
