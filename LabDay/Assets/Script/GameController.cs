﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We'll use GameStates to switch beetween Scenes (Overworld, Battle, etc)

public enum GameState { FreeRoam, Battle} //For now we have just two states
public class GameController : MonoBehaviour
{
    GameState state;//Reference to our GameState
    [SerializeField] PlayerController playerController;//Reference to the PlayerController script
    [SerializeField] BattleSystem battleSystem;//Reference to the BattleSystem Script 
    [SerializeField] Camera worldCamera; //Reference to our Camera

    //On the first frame we check if we enable our Overworld script, or the battle one
    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    //Change our battle state, camera active, and gameobject of the Battle System
    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle(); //Call our StartBattle, so every fight are not the same
    }
    //Change our battle state, camera active, and gameobject of the Battle System
    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam) //While we are in the overworld, we use our PlayerController script
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle) //Else if we are in a battle, we'll disable our PlayerController Script
        {
            battleSystem.HandleUpdate();
        }
    }
}
