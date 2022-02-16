using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiItemManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject[] items;
    private int itemIndex;

    void Awake()
    {
        itemIndex = 0;

        //subscribe to event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        //deactivate previous game state item
        if (itemIndex > 0)
        {
            items[itemIndex - 1].SetActive(false);
        }

        //set next item as current game state
        if (itemIndex < items.Length)
        {
            items[itemIndex].SetActive(state == gameState);

            if (state == gameState)
            {
                itemIndex++;
            }
        }
        //else
        //{
        //    Application.Quit();
        //}
    }
}
