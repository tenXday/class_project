using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class RandomLoadingShow : MonoBehaviour
{
    [Serializable]
    public struct Objtransform
    {
        public Vector3 ObjPosition;
        public Vector3 ObjRotation;
        public Vector3 Objscale;
    }
    [Serializable]
    public struct Equipment
    {
        public GameObject Equipmentobj;
        public Objtransform objtransform;
        public string Equipname;
        [TextArea(10, 15)] public string Equipintro;
    }
    private GameObject EquipUI, Equipname, Equipintro;
    private GameObject EquipObj;
    private LoadingBar m_loadingBar;
    public bool isLoad;
    public int testIndex;
    [SerializeField] private List<Equipment> equipment;
    int Randomindex;
    void Start()
    {
        EquipUI = GameObject.Find("Equipment");
        Equipname = GameObject.Find("Equipment_name");
        Equipintro = GameObject.Find("Equipment_intro");
        m_loadingBar = GetComponent<LoadingBar>();
        //隨機選取
        if (testIndex == 99)
        {
            Randomindex = UnityEngine.Random.Range(0, equipment.Count);
        }
        else
        {
            Randomindex = testIndex;
        }

        //生成裝備 
        EquipObj = GameObject.Instantiate(equipment[Randomindex].Equipmentobj);
        EquipObj.transform.SetParent(EquipUI.transform);
        EquipObj.transform.localPosition = equipment[Randomindex].objtransform.ObjPosition;
        EquipObj.transform.localScale = equipment[Randomindex].objtransform.Objscale;
        EquipObj.transform.localRotation = Quaternion.Euler(equipment[Randomindex].objtransform.ObjRotation);
        SetLayerRecursively(EquipObj, 17);
        //生成外框線
        /*EquipObj.AddComponent<Outline>();
        EquipObj.GetComponent<Outline>().OutlineColor = Color.white;
        EquipObj.GetComponent<Outline>().OutlineWidth = 5;
        EquipObj.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;*/
        //旋轉
        EquipObj.AddComponent<Routate>();
        //設定文字
        Equipname.GetComponent<TMP_Text>().SetText(equipment[Randomindex].Equipname);
        Equipintro.GetComponent<TMP_Text>().SetText(equipment[Randomindex].Equipintro);

        if (isLoad)
        {
            m_loadingBar.StartConnect();
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
}
