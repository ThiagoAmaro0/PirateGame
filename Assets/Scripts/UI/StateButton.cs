using System;
using UnityEngine;
using UnityEngine.UI;

public class StateButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameState _state;

    private void Awake()
    {
        _button.onClick.AddListener(SetState);
    }

    private void SetState()
    {
        GameManager.instance.SetState(_state);
    }
}