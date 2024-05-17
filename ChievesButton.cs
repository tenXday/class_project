using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChievesButton : MonoBehaviour
{
    [Serializable]
    public struct Chieves
    {
        public string chieves_name;
        [TextArea] public string chieves_intro;
        public Sprite chieves_icon;
    }

    [SerializeField] Chieves chieves;
    [SerializeField] GameObject chievesShowObj;

    public void ShowChieces()
    {
        chievesShowObj.SetActive(true);
        GameObject chievesCanvas = chievesShowObj.transform.Find("ChievesCanvas").gameObject;
        GameObject chieves_name = chievesCanvas.transform.Find("Chieves_name").gameObject;
        GameObject chieves_intro = chievesCanvas.transform.Find("Chieves_intro").gameObject;
        GameObject chieves_icon = chievesCanvas.transform.Find("Chieves_icon").gameObject;

        chieves_name.GetComponent<TMP_Text>().SetText(chieves.chieves_name);
        chieves_intro.GetComponent<TMP_Text>().SetText(chieves.chieves_intro);
        chieves_icon.GetComponent<Image>().sprite = chieves.chieves_icon;
    }
}
