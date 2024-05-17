using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FixButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject FixBar;
    private Image Bar;
    [SerializeField] private bool isPressed;
    [SerializeField] AudioSource FixAudio;
    void Start()
    {
        isPressed = false;
        FixBar = GameObject.Find("Fill");
        Bar = FixBar.GetComponent<Image>();
    }
    void Update()
    {
        if (isPressed)
        {
            Bar.fillAmount += 0.1f * Time.unscaledDeltaTime;
            if (!FixAudio.isPlaying)
            {
                FixAudio.Play();
            }
        }
        else
        {
            FixAudio.Stop();
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
    void OnDisable()
    {
        isPressed = false;
    }


}