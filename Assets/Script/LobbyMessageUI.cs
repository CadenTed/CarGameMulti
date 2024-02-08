using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageTxt;
    [SerializeField] private Button closeBtn;

    private void Awake()
    {
        closeBtn.onClick.AddListener(Hide);
    }

    private void Start()
    {
        CarGameMultiplayer.Instance.OnFailedToJoinGame += CarGameMultiplayer_OnFailedToJoinGame;

        CarGameLobby.Instance.OnCreatLobbyStarted += CarGameLobby_OnCreateLobbyStarted;
        CarGameLobby.Instance.OnCreateLobbyFailed += CarGameLobby_OnCreateLobbyFailed;
        CarGameLobby.Instance.OnJoinStarted += CarGameLobby_OnJoinStarted;
        CarGameLobby.Instance.OnJoinFailed += CarGameLobby_OnJoinFailed;
        CarGameLobby.Instance.OnQuickJoinFailed += CarGameLobby_OnQuickJoinFailed;
        
        
        Hide();
    }

    private void CarGameLobby_OnQuickJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("No Lobby to Quick Join!");

    }

    private void CarGameLobby_OnJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to join Lobby!");
    }

    private void CarGameLobby_OnJoinStarted(object sender, EventArgs e)
    {
        ShowMessage("Joining Lobby...");
    }

    private void CarGameLobby_OnCreateLobbyFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to creat lobby!");
    }

    private void CarGameLobby_OnCreateLobbyStarted(object sender, EventArgs e)
    {
        ShowMessage("Creating Lobby...");
    }

    private void CarGameMultiplayer_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Failed to connect");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }

    private void ShowMessage(string message)
    {
        Show();

        messageTxt.text = message;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        CarGameMultiplayer.Instance.OnFailedToJoinGame -= CarGameMultiplayer_OnFailedToJoinGame;
        CarGameLobby.Instance.OnCreatLobbyStarted -= CarGameLobby_OnCreateLobbyStarted;
        CarGameLobby.Instance.OnCreateLobbyFailed -= CarGameLobby_OnCreateLobbyFailed;
        CarGameLobby.Instance.OnJoinStarted -= CarGameLobby_OnJoinStarted;
        CarGameLobby.Instance.OnJoinFailed -= CarGameLobby_OnJoinFailed;
        CarGameLobby.Instance.OnQuickJoinFailed -= CarGameLobby_OnQuickJoinFailed;
    }
}

