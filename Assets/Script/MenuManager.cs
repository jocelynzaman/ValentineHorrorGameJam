using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject menu;

    void Awake()
    {
        //subscribe to event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        menu.SetActive(state == gameState);        
    }

    public void PlayGame()
    {
        GameManager.Instance.UpdateGameState(GameState.CutScene);
    }

    public void ShowControls()
    {
        GameManager.Instance.UpdateGameState(GameState.ControlScreen);
    }

    public void ShowCredits()
    {
        GameManager.Instance.UpdateGameState(GameState.Credits);
    }

    //public void SetMenuOption(GameState state)
    //{
    //    GameManager.Instance.UpdateGameState(state);
    //}

    public void Back()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void Exit()
    {
        print("quit game");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
