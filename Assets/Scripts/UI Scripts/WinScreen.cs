﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour
{
    const string aliveString = "Alive";
    const string deadString = "Deceased";
    const string playerString = "Player";

    const string namePrefix = "Name";
    const string scorePrefix = "Score";
    const string statusPrefix = "Status";

    struct PlayerScoreDisplay
    {
        public TextMeshProUGUI nameDisplay;
        public TextMeshProUGUI scoreDisplay;
        public TextMeshProUGUI statusDisplay;
    }

    PlayerScoreDisplay[] scoreDisplays;

    void PopulateDisplays()
    {
        scoreDisplays = new PlayerScoreDisplay[4];

        foreach (Transform t in transform)
        {
            if (t.name == "Title" || t.name == "Button")
            {
                continue;
            }

            string idString = t.name.Substring(t.name.Length - 1);
            int id = Convert.ToInt32(idString) - 1;

            if (t.name.StartsWith(namePrefix))
            {
                t.gameObject.SetActive(false);
                scoreDisplays[id].nameDisplay = t.GetComponent<TextMeshProUGUI>();
            }
            else if (t.name.StartsWith(scorePrefix))
            {
                t.gameObject.SetActive(false);
                scoreDisplays[id].scoreDisplay = t.GetComponent<TextMeshProUGUI>();
            }
            else if (t.name.StartsWith(statusPrefix))
            {
                t.gameObject.SetActive(false);
                scoreDisplays[id].statusDisplay = t.GetComponent<TextMeshProUGUI>();
            }
        }
    }
    void Start()
    {
        PopulateDisplays();

        int i = 0;
        foreach (GameState.PlayerState playerState in GameState.playerStates)
        {
            PlayerScoreDisplay scoreDisplay = scoreDisplays[i];

            scoreDisplay.nameDisplay.gameObject.SetActive(true);
            scoreDisplay.scoreDisplay.gameObject.SetActive(true);
            scoreDisplay.statusDisplay.gameObject.SetActive(true);

            scoreDisplay.nameDisplay.text = playerString + " " + (playerState.playerId + 1);
            scoreDisplay.scoreDisplay.text = "" + playerState.score;

            i++;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}