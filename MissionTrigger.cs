using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    public int MissionID;

    void OnEnable()
    {
        PlayboardEvent.MissionEnd += MissionEnd;
    }
    void OnDisable()
    {
        PlayboardEvent.MissionEnd -= MissionEnd;
    }

    private void MissionEnd(int ID)
    {
        if (ID == MissionID)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayboardEvent.CallMissionStart(MissionID);
            PlayboardEvent.CallGamePause();
        }
    }

}
