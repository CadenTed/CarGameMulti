using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameObject;
    [SerializeField] private PlayerVisual playerVisual;

    private void Start()
    {
        CarGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += CarGameMultiplayer_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.Instace.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

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

            playerVisual.SetBodyColor(CarGameMultiplayer.Instance.GetPlayerColor(playerIndex));
            playerVisual.SetPlowColor(CarGameMultiplayer.Instance.GetPlayerColor(playerIndex));
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
}
