using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    SELECTING,
    PLANTING,
    WAITINGFORRAIN,
    GROWING,
    SCORING
}

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance { get; private set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
        } 
    }

    private void Start()
    {
        ProcessState();
    }
    public GameState gameState = GameState.SELECTING;

    public RootType RootToPlant = RootType.Yellow;

    public Overlay CardOverlay;

    public void ProcessState()
    {
        switch (gameState)
        {
        case GameState.SELECTING:
            StartCoroutine(StartSelecting());
            Debug.Log("Processing SELECT");
            break;
        case GameState.PLANTING:
            StartPlanting();
            Debug.Log("Processing PLANTING");
            break;
        case GameState.GROWING:
            StartCoroutine(StartGrowing());
            Debug.Log("Processing GROWING");
            break;  
        case GameState.SCORING:
            StartScoring();
            Debug.Log("Processing SCORING");
            break;  
        case GameState.WAITINGFORRAIN:
            // TODO
            gameState = GameState.GROWING;
            ProcessState();
            break;
        }
    }

    private IEnumerator StartGrowing()
    {
        yield return new WaitForSeconds(1);
        TileManager.Instance.SpreadRoots();
        yield return new WaitForSeconds(1);
        gameState = GameState.SCORING;
        ProcessState();
    }

    private void StartScoring()
    {
        gameState = GameState.SELECTING;
        ProcessState();
    }

    private void StartPlanting()
    {

    }

    private IEnumerator StartSelecting()
    {
        yield return new WaitForSeconds(1);
        CardOverlay.ClickDraw();
    }

    public void OnCardSelected(RootType rootType)
    {
        RootToPlant = rootType;
        gameState = GameState.PLANTING;
        Debug.Log($"Ready to plant a {RootToPlant}");

    }
}
