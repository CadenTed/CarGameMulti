using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionResponseMessageUI : MonoBehaviour
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

        Hide();
    }

    private void CarGameMultiplayer_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        Show();

        messageTxt.text = NetworkManager.Singleton.DisconnectReason;

        if (messageTxt.text == "" )
        {
            messageTxt.text = "Failed to connect";
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
        CarGameMultiplayer.Instance.OnFailedToJoinGame -= CarGameMultiplayer_OnFailedToJoinGame;
    }
}

