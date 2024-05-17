using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextShow : MonoBehaviour
{
    [Range(0.2f, 5f), SerializeField] float charPerSecond = 0.2f;
    [SerializeField] private bool isActive = false;
    [SerializeField] private TMP_Text[] mText;
    [SerializeField] AudioClip typeSound;
    [SerializeField] private GameObject TapObj;
    [SerializeField] Animator textAnimater;
    [SerializeField] Tapto tap;


    private AudioSource source;
    private string[] tempwords;
    private float timer;
    private int currentP = 0;
    private int currentPos = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        timer = 0;
        tempwords = new string[mText.Length];
        charPerSecond = Mathf.Max(0.2f, charPerSecond);
        for (int i = 0; i < mText.Length; i++)
        {
            if (mText[i] != null)
            {
                tempwords[i] = mText[i].text;
                mText[i].SetText("");
            }
        }
        if (TapObj != null)
        {
            TapObj.SetActive(false);
        }

    }


    void Update()
    {
        if (currentP < mText.Length)
        {
            Printer(mText[currentP], tempwords[currentP]);
        }
        else
        {
            isActive = false;
            currentP = 0;
            TapObj.SetActive(true);
            textAnimater.SetBool("New Bool", true);
            tap.isTouchActive = true;
        }

    }
    void StartShow()
    {
        isActive = true;
    }

    void Printer(TMP_Text currentPrint, string words)
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charPerSecond)
            {
                timer = 0;
                currentPos++;
                currentPrint.SetText(words.Substring(0, currentPos));
                source.PlayOneShot(typeSound, 1f);
                if (currentPos >= words.Length)
                {
                    timer = 0;
                    currentPos = 0;
                    currentPrint.SetText(words);
                    currentP++;
                    source.Stop();
                }
            }
        }
    }
}
