using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Tapto : MonoBehaviour
{
    public string tap_to;
    public bool isTouchActive;
    private InputControls m_PlayerInput;

    public void Awake()
    {
        m_PlayerInput = new InputControls();
    }
    void Update()
    {
        if (isTouchActive)
        {
            var c_touchPhase = m_PlayerInput.Touch.TouchPhase.ReadValue<UnityEngine.InputSystem.TouchPhase>();
            if (c_touchPhase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                LoadScene();

            }
        }
    }
    public void LoadScene()
    {
        StartCoroutine(LoadinBackground());
        //SceneManager.LoadScene(tap_to);
    }
    IEnumerator LoadinBackground()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(tap_to);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    void OnEnable()
    {
        m_PlayerInput.Enable();
    }
    void OnDisenable()
    {
        m_PlayerInput.Disable();
    }
}
