using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
public class MissionManager : MonoBehaviour
{
    [Serializable]
    public struct Objtransform
    {
        public Vector3 ObjPosition;
        public Quaternion ObjRotation;
        public Vector3 Objscale;
    }
    [Serializable]
    public struct Effect
    {
        public Sprite ProcessedTitleIMG;
        [Space]
        public Sprite EffectIMG;
        public String EffectTitle;
        [TextArea] public string EffectIntro;
    }
    [Serializable]
    public struct Mission
    {
        public int ID;
        public GameObject Equipmentobj;
        public Objtransform objtransform;
        public string Equipname;
        [TextArea] public string Equipintro;
        public Effect effect;

    }

    [SerializeField, Range(0f, 20f)] float outlinewidth;

    [Space]
    [SerializeField] private List<Mission> mission;
    //任務UI管理
    private GameObject MissionRunCanvas;
    private GameObject Processing, Processed;
    //任務中介面
    private GameObject Equipment, EquipUI, Equipname, Equipintro, ProcessBar;
    //任務完成介面
    private GameObject EffectObj, Continue;
    private GameObject TitleIMG, EffectIMG, EffectTitle, EffectIntro, Tip;
    //任務觸發器
    private List<GameObject> MissionTrigger = new List<GameObject>();
    //遊戲完成介面
    private GameObject OverCanvasObj, SuccessObj, FaildObj;
    private int currentID;
    private bool isMissionStart = false;
    void Awake()
    {
        MissionTrigger.AddRange(GameObject.FindGameObjectsWithTag("MissionTrigger"));
    }
    void Start()
    {
        Debug.Log("Level" + PlayerPrefs.GetInt("currentlevel"));
        MissionRunCanvas = GameObject.Find("MissionCanvas");
        //取得任務中元件
        Processing = GameObject.Find("Processing");
        EquipUI = GameObject.Find("Equipment");
        Equipname = GameObject.Find("Equipmentname");
        Equipintro = GameObject.Find("Equipmentintro");
        ProcessBar = GameObject.Find("Fill");
        //取得任務完成元件
        Processed = GameObject.Find("Processed");
        EffectObj = GameObject.Find("Effect");
        TitleIMG = GameObject.Find("TitleIMG");
        EffectIMG = GameObject.Find("EffectIMG");
        EffectTitle = GameObject.Find("EffectTitle");
        EffectIntro = GameObject.Find("EffectIntro");
        Continue = GameObject.Find("Continue");
        Tip = GameObject.Find("NextTip");


        //取得遊戲結束元件
        OverCanvasObj = GameObject.Find("GameOverCanvas");
        SuccessObj = GameObject.Find("Success");
        FaildObj = GameObject.Find("Faild");


        //初始化
        MissionRunCanvas.SetActive(false);
        Processing.SetActive(false);
        Processed.SetActive(false);
        EffectObj.SetActive(false);
        Continue.SetActive(false);

        OverCanvasObj.SetActive(false);
        SuccessObj.SetActive(false);
        FaildObj.SetActive(false);
    }
    void OnEnable()
    {
        PlayboardEvent.MissionStart += MissionStart;
        PlayboardEvent.MissionEnd += MissionProcessEnd;
        PlayboardEvent.GameEnd += GameEnd;
    }


    void OnDisable()
    {
        PlayboardEvent.MissionStart -= MissionStart;
        PlayboardEvent.MissionEnd -= MissionProcessEnd;
        PlayboardEvent.GameEnd -= GameEnd;
    }

    void Update()
    {
        if (isMissionStart)
        {
            if (ProcessBar.GetComponent<Image>().fillAmount == 1)
            {
                PlayboardEvent.CallMissionEnd(currentID);
            }
        }
    }

    private void MissionStart(int missionID)
    {
        currentID = missionID;
        isMissionStart = true;
        if (MissionRunCanvas != null)
        {
            //啟動任務畫面
            MissionRunCanvas.SetActive(true);
            Processing.SetActive(true);
            Processed.SetActive(false);
            //初始化
            ProcessBar.GetComponent<Image>().fillAmount = 0;
            //生成展示裝備
            Equipment = GameObject.Instantiate(mission[missionID].Equipmentobj);
            Equipment.transform.SetParent(EquipUI.transform);
            Equipment.transform.localPosition = mission[missionID].objtransform.ObjPosition;
            Equipment.transform.localScale = mission[missionID].objtransform.Objscale;
            Equipment.transform.localRotation = mission[missionID].objtransform.ObjRotation;
            SetLayerRecursively(Equipment, 17);

            //生成外框線
            Equipment.AddComponent<Outline>();
            Equipment.GetComponent<Outline>().OutlineColor = Color.white;
            Equipment.GetComponent<Outline>().OutlineWidth = outlinewidth;
            Equipment.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            //旋轉
            Equipment.AddComponent<Routate>();
            //設定文字
            Equipname.GetComponent<TMP_Text>().SetText(mission[missionID].Equipname);
            Equipintro.GetComponent<TMP_Text>().SetText(mission[missionID].Equipintro);
        }
    }
    void SetLayerRecursively(GameObject obj, int layer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, layer);
        }
    }
    private void MissionProcessEnd(int missionID)
    {
        //關閉任務進行畫面
        isMissionStart = false;
        Destroy(Equipment);
        Processing.SetActive(false);
        //顯示下一個觸發器
        if (missionID + 1 < MissionTrigger.Count)
        {
            MissionTrigger[missionID + 1].SetActive(true);
        }
        //開啟效果畫面
        Processed.SetActive(true);
        EffectObj.SetActive(true);
        //設定畫面內容
        var Effect = mission[missionID].effect;
        TitleIMG.GetComponent<Image>().sprite = Effect.ProcessedTitleIMG;
        EffectIMG.GetComponent<Image>().sprite = Effect.EffectIMG;
        EffectTitle.GetComponent<TMP_Text>().SetText(Effect.EffectTitle);
        EffectIntro.GetComponent<TMP_Text>().SetText(Effect.EffectIntro);
        Continue.SetActive(false);
        if (missionID == 3)
        {
            Tip.GetComponent<TMP_Text>().SetText("你完成了最後一項工作!傑出極了!");
        }
    }

    public void EffectShowOver(InputAction.CallbackContext ctx)
    {
        EffectObj.SetActive(false);
        Continue.SetActive(true);

    }
    public void ContinueGame()
    {
        PlayboardEvent.CallGameContinue();
        if (currentID == 3)
        {
            PlayboardEvent.CallGameEnd(true);
        }
    }
    private void GameEnd(bool isSuccessed)
    {
        OverCanvasObj.SetActive(true);
        PlayboardEvent.CallGamePause();
        if (isSuccessed)
        {
            //通關數增加
            if (PlayerPrefs.GetInt("currentlevel") == 0)
            {
                PlayerPrefs.SetInt("Level1PassTime", PlayerPrefs.GetInt("Level1PassTime") + 1);
            }
            if (PlayerPrefs.GetInt("currentlevel") == 1)
            {
                PlayerPrefs.SetInt("Level2PassTime", PlayerPrefs.GetInt("Level2PassTime") + 1);
            }
            if (PlayerPrefs.GetInt("currentlevel") == 2)
            {
                PlayerPrefs.SetInt("Level3PassTime", PlayerPrefs.GetInt("Level3PassTime") + 1);
            }
            //設置成就
            if (PlayerPrefs.GetInt("currentlevel") == 2 && PlayerPrefs.GetInt("Level3PassTime") == 1)
            {
                PlayerPrefs.SetInt("Chieve0", 1);
            }
            if (PlayerPrefs.GetInt("currentlevel") == 2 && PlayerPrefs.GetInt("Level3PassTime") == 2)
            {
                PlayerPrefs.SetInt("Chieve1", 1);
            }
            if (PlayerPrefs.GetInt("currentlevel") == 2 && PlayerPrefs.GetInt("Level3PassTime") == 3)
            {
                PlayerPrefs.SetInt("Chieve2", 1);
            }

            SuccessObj.SetActive(true);
        }
        else
        {
            FaildObj.SetActive(true);
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void SetCurrentLevel(int currentlevel)
    {
        PlayerPrefs.SetInt("currentlevel", currentlevel);
    }
}
