using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TileWorld;
using TileWorld.Events;
public class TestTileCreater : MonoBehaviour
{
    [SerializeField] private GameObject EnemyGenarater;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject chievetrigger;

    [Space]
    [SerializeField] private TileWorldCreator m_TileWorld;
    [SerializeField] private TileWorldObjectScatter m_TileWorldObjectSca;
    private int randomIndex;
    private GameObject EnemyManage;
    private List<Vector3> block_NoOccupyList = new List<Vector3>();
    private List<Vector3> block_OccupyList = new List<Vector3>();
    private List<Vector3> ground_NoOccupyList = new List<Vector3>();
    private List<Vector3> ground_OccupyList = new List<Vector3>();
    private List<GameObject> MissionTrigger = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        m_TileWorld.GenerateAndBuild(true);
        m_TileWorldObjectSca.ScatterProceduralObjects();
        EnemyManage = GameObject.Find("EnemyManage");
        MissionTrigger.AddRange(GameObject.FindGameObjectsWithTag("MissionTrigger"));
    }
    void OnEnable()
    {
        TileWorldEvents.OnBuildComplete += MapOnBuildComplete;
        TileWorldEvents.OnMergeComplete += MapOnMergeComplete;
        TileWorldEvents.MergeProgress += Mergep;
    }
    void OnDisenable()
    {
        TileWorldEvents.OnBuildComplete -= MapOnBuildComplete;
        TileWorldEvents.OnMergeComplete -= MapOnMergeComplete;
        TileWorldEvents.MergeProgress -= Mergep;
    }
    void Mergep(float progress)
    {
        Debug.Log(progress + "%");
    }

    void MapOnBuildComplete()
    {

        for (int i = 0; i < m_TileWorld.configuration.worldMap[0].tileObjects.GetLength(0); i++)
        {
            for (int j = 0; j < m_TileWorld.configuration.worldMap[0].tileObjects.GetLength(1); j++)
            {
                if (m_TileWorld.configuration.worldMap[0].tileObjects[i, j] != null &&
                   m_TileWorld.configuration.worldMap[0].tileTypes[i, j] == TileWorldConfiguration.TileInformation.TileTypes.block &&
                   !m_TileWorldObjectSca.OccupyMap[i, j])
                {
                    block_NoOccupyList.Add(m_TileWorld.configuration.worldMap[0].tileObjects[i, j].gameObject.transform.position);//獲取道路List
                }
                if (m_TileWorld.configuration.worldMap[0].tileObjects[i, j] != null &&
                   m_TileWorld.configuration.worldMap[0].tileTypes[i, j] == TileWorldConfiguration.TileInformation.TileTypes.ground &&
                   !m_TileWorldObjectSca.OccupyMap[i, j])
                {
                    ground_NoOccupyList.Add(m_TileWorld.configuration.worldMap[0].tileObjects[i, j].gameObject.transform.position);//獲取地面List
                }
            }
        }
        //設置玩家座標
        randomIndex = Random.Range(0, block_NoOccupyList.Count);
        Vector3 p_transform = block_NoOccupyList[randomIndex];
        GameObject p;
        if (player != null)
        {
            p = player;
        }
        else
        {
            p = GameObject.FindWithTag("Player");
        }
        p.transform.position = new Vector3(p_transform.x, p_transform.y + 10f, p_transform.z);
        p.transform.localRotation = Quaternion.identity;
        block_NoOccupyList.Remove(p_transform);//移出未占用
        block_OccupyList.Add(p_transform);//添加至已占用

        //設置成就觸發器座標
        randomIndex = Random.Range(0, ground_NoOccupyList.Count);
        Vector3 c_transform = ground_NoOccupyList[randomIndex];
        GameObject c;
        if (chievetrigger != null)
        {
            c = chievetrigger;
        }
        else
        {
            c = GameObject.FindWithTag("Chieve");
        }
        c.transform.position = new Vector3(c_transform.x, c_transform.y + 2.64f, c_transform.z);
        ground_NoOccupyList.Remove(c_transform);//移出未占用
        ground_OccupyList.Add(c_transform);//添加至已占用

        //設置任務觸發器座標

        for (int i = 0; i < MissionTrigger.Count; i++)
        {
            if (MissionTrigger[i] == null)
            {
                MissionTrigger.Clear();
                MissionTrigger.AddRange(GameObject.FindGameObjectsWithTag("MissionTrigger"));
            }
            randomIndex = Random.Range(0, ground_NoOccupyList.Count);
            Vector3 mission_transform = ground_NoOccupyList[randomIndex];

            MissionTrigger[i].transform.position = new Vector3(mission_transform.x, mission_transform.y + 10f, mission_transform.z);
            ground_NoOccupyList.Remove(mission_transform);//移出未占用
            ground_OccupyList.Add(mission_transform);//添加至已占用
        }
        for (int i = 1; i < MissionTrigger.Count; i++)
        {
            MissionTrigger[i].SetActive(false);
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
    void MapOnMergeComplete()
    {
        //生成導航地圖
        List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
        GameObject tileworld = GameObject.Find("layer_0");
        GameObject tileworldobj = GameObject.Find("TWC_World_Objects");
        SetLayerRecursively(tileworld, 15);
        SetLayerRecursively(tileworldobj, 15);

        tileworld.AddComponent<NavMeshSurface>();
        tileworld.GetComponent<NavMeshSurface>().collectObjects = CollectObjects.All;
        tileworld.GetComponent<NavMeshSurface>().layerMask = 1 << LayerMask.NameToLayer("Map");
        tileworld.GetComponent<NavMeshSurface>().BuildNavMesh();
        //生成敵人繁殖器
        randomIndex = Random.Range(0, block_NoOccupyList.Count);
        Vector3 Enemy_transform = block_NoOccupyList[randomIndex];
        float randomX = Random.Range(-10, 10);
        float randomZ = Random.Range(-10, 10);
        GameObject EG;
        if (EnemyGenarater != null)
        {
            EG = EnemyGenarater;

        }
        else
        {
            EG = GameObject.Find("EnemyGenarater");
        }
        EG.transform.position = new Vector3(Enemy_transform.x + randomX, Enemy_transform.y + 2.5f, Enemy_transform.z + randomZ);
        block_NoOccupyList.Remove(Enemy_transform);//移出未占用
        block_OccupyList.Add(Enemy_transform);//添加至已占用
        if (EnemyManage == null)
        {
            EnemyManage = GameObject.Find("EnemyManage");
        }
        EG.GetComponent<EnemyGenarater>().SetList(ground_NoOccupyList, EnemyManage.transform);

        //開始遊戲
        PlayboardEvent.CallGameStart();
    }

}
