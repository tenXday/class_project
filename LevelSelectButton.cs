using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    [Serializable]
    public struct Level
    {
        public string level_name;

        public Sprite level_img;
    }
    [SerializeField] Level level;
    [SerializeField] GameObject hardSelectObj;
    public void ShowHardSelect()
    {
        hardSelectObj.SetActive(true);
        GameObject hardSelectCanvas = hardSelectObj.transform.Find("HardSelectCanvas").gameObject;
        GameObject level_name = hardSelectCanvas.transform.Find("Level_name").gameObject;

        GameObject level_img = hardSelectCanvas.transform.Find("Level_img").gameObject;

        level_name.GetComponent<TMP_Text>().SetText(level.level_name);
        level_img.GetComponent<Image>().sprite = level.level_img;
    }
}
