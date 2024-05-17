using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float Hp = 100;
    [SerializeField] AudioClip Gethit;

    public void Hit(float damage)
    {
        PlayboardEvent.CallHealthMinus(damage);
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().PlayOneShot(Gethit);
        }
        Hp -= damage;
        if (Hp <= 0)
        {
            PlayboardEvent.CallGameEnd(false);
        }
    }
}
