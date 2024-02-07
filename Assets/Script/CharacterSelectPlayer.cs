using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameObject;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button kickButton;

    private void Awake()
    {
        kickButton.onClick.AddListener(() =>
        {
            PlayerData playerData = CarGameMultiplayer.Instance.GetPLayerDataFromPlayerIndex(playerIndex);
            CarGameMultiplayer.Instance.KickPlayer(playerData.clientId);
        });
    }

    private void Start()
    {
        CarGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += CarGameMultiplayer_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.Instace.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }

    private void CarGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        if (CarGameMultiplayer.Instance.IsPlayerIndexConnected(playerIndex))
        {
            Show();

            PlayerData playerData = CarGameMultiplayer.Instance.GetPLayerDataFromPlayerIndex(playerIndex);
            readyGameObject.SetActive(CharacterSelectReady.Instace.IsPlayerReady(playerData.clientId));

            playerVisual.SetBodyColor(CarGameMultiplayer.Instance.GetPlayerColor(playerData.colorId));
            playerVisual.SetPlowColor(CarGameMultiplayer.Instance.GetPlayerColor(playerData.colorId));
        }
        else
        {
            Hide();
        }
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
        CarGameMultiplayer.Instance.OnPlayerDataNetworkListChanged -= CarGameMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
