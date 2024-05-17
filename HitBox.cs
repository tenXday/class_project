using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Hit(damage * (PlayerPrefs.GetInt("currentlevel") + 1));
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
