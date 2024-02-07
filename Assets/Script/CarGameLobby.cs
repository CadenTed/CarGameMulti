using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class CarGameLobby : MonoBehaviour
{
    public static CarGameLobby Instance { get; private set; }

    private Lobby joinedLobby;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeUnityAuthentication();


    }

    private async void InitializeUnityAuthentication()
    {

        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initialization = new InitializationOptions();
            initialization.SetProfile(Random.Range(0, 1000).ToString());
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        }

    }

    public async void CreateLobby(string lobbyName, bool isPrivate)
    {

        try
        {
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, CarGameMultiplayer.MAX_PLAYERS, new CreateLobbyOptions
            {
                IsPrivate = isPrivate,
            });

            CarGameMultiplayer.Instance.StartHost();
            LoadGame.LoadCharacterSelect();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
    }

    public async void QuickJoin()
    {
        try
        {
            joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();

            CarGameMultiplayer.Instance.StartClient();

        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }


}
