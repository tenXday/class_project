using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Environment : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float Efectspeed = 10f;

    float OriSpeed;
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    void Update()
    {

    }
    void OnEnable()
    {
        PlayboardEvent.MissionEnd += ShowArea;
        PlayboardEvent.GameStart += StartG;
    }
    bool isStart = false;
    private void StartG()
    {
        isStart = true;

    }

    private void ShowArea(int missionID)
    {
        if (missionID == 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    void OnDisable()
    {
        PlayboardEvent.MissionEnd -= ShowArea;
        PlayboardEvent.GameStart -= StartG;
    }
    private void OnTriggerStay(Collider other)
    {
        if (isStart)
        {

            if (other.tag == "Enemy")
            {

                OriSpeed = other.GetComponent<ai2>().move_rate;
                other.GetComponent<NavMeshAgent>().speed = OriSpeed + Efectspeed;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (isStart)
        {
            if (other.tag == "Enemy")
            {
                OriSpeed = other.GetComponent<ai2>().move_rate;
                other.GetComponent<NavMeshAgent>().speed = OriSpeed;

            }
        }
    }
}
