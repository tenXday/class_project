using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;
    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("mainvolume", Mathf.Log10(volume) * 20);
    }
}
