using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tapclose : MonoBehaviour
{
    [SerializeField] private MissionManager missionManager;
    private InputControls m_PlayerInput;
    public void Awake()
    {
        m_PlayerInput = new InputControls();

    }


    void OnEnable()
    {
        m_PlayerInput.Enable();
        m_PlayerInput.UI.Click.performed += missionManager.EffectShowOver;

    }



    void OnDisable()
    {
        m_PlayerInput.UI.Click.performed -= missionManager.EffectShowOver;
        m_PlayerInput.Disable();
    }
}
