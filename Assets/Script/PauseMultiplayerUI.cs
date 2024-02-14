using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{

    private void Start()
    {
        PauseManager.Instance.OnMultiplayerGamePaused += PauseManager_OnMultiplayerGamePaused;
        PauseManager.Instance.OnMultiplayerGameUnpaused += PauseManager_OnMultiplayerGameUnpaused;

        Hide();
    }

    private void PauseManager_OnMultiplayerGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void PauseManager_OnMultiplayerGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
