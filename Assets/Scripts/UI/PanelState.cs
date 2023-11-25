using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelState : MonoBehaviour
{
    [SerializeField] private GameState _state;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        GameManager.instance.OnChangeState += StateListener;

    }
    private void OnDisable()
    {
        GameManager.instance.OnChangeState -= StateListener;
    }

    private void StateListener(GameState state)
    {
        _panel.SetActive(state == _state);
    }
}
