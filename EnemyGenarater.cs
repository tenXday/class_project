using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenarater : MonoBehaviour
{
    [SerializeField] float GenarateSecond = 10;
    [SerializeField] GameObject Enemy;
    [SerializeField, Range(5, 30)] int Enemy_up;
    private List<Vector3> BrithList = new List<Vector3>();
    private int randomIndex;
    private List<GameObject> EnemyList = new List<GameObject>();

    private Transform Manage_transform;
    private bool isGenarate = false;
    private bool breakGenarete = false;
    private bool isSlow = false;
    void OnEnable()
    {
        PlayboardEvent.MissionEnd += Mission;
    }


    void OnDisable()
    {
        PlayboardEvent.MissionEnd -= Mission;

    }
    private void Mission(int missionID)
    {
        if (missionID == 1)
        {
            isSlow = true;
        }
        if (missionID == 2)
        {
            breakGenarete = true;
        }
    }
    void Update()
    {
        if (isSlow)
        {
            foreach (GameObject Enemy in EnemyList)
            {
                Enemy.GetComponent<ai2>().move_rate = 5;
            }
        }
        //將攻擊過的敵人移出列表
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i] == null)
            {
                EnemyList.Remove(EnemyList[i]);
            }
        }
        //如果任務完成,則無法生成
        if (!breakGenarete)
        {
            if (isGenarate)
            {

                //若未達生成上限,生成敵人
                if (EnemyList.Count < Enemy_up)
                {
                    StartCoroutine(StartGenarate(GenarateSecond));
                    isGenarate = false;
                }
            }
        }

    }
    public void SetList(List<Vector3> NoOccupyList, Transform M_transform)
    {
        BrithList = NoOccupyList;
        Manage_transform = M_transform;
        isGenarate = true;
    }
    IEnumerator StartGenarate(float seconds)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        GenarateEnemy();
        isGenarate = true;
    }
    void GenarateEnemy()
    {
        randomIndex = Random.Range(0, BrithList.Count);
        Vector3 N_Enemytransform = BrithList[randomIndex];
        GameObject Ene = GameObject.Instantiate(Enemy, N_Enemytransform, Enemy.transform.rotation);
        Ene.transform.position = new Vector3(Ene.transform.position.x, Ene.transform.position.y + 1, transform.position.z);
        Ene.transform.SetParent(Manage_transform);
        EnemyList.Add(Ene);

    }
}
