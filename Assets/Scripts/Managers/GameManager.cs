using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneManager;
    [SerializeField] private string _menusceneName = "Menu";
    private GameState _state;
    public static GameManager instance;
    public Action<GameState> OnChangeState;

    public GameState State { get => _state; private set => _state = value; }
    public SceneLoader SceneManager { get => _sceneManager; }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            _sceneManager.LoadScene(_menusceneName, LoadSceneMode.Additive, false);
        }
    }

    public void SetState(GameState state)
    {
        State = state;
        OnChangeState?.Invoke(State);
    }
}

public enum GameState
{
    MENU = 0,
    OPTIONS = 1,
    GAME = 2,
    WIN = 3,
    LOSE = 4,
}


