using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChieveTrigger : MonoBehaviour
{
    GameObject chievecanvas;
    void Start()
    {
        chievecanvas = GameObject.Find("ChieveCanvas");
        chievecanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            //設置成就
            PlayerPrefs.SetInt("Chieve3", 1);
            chievecanvas.SetActive(true);
            Destroy(this.gameObject);
            PlayboardEvent.CallGamePause();
        }
    }
}
