using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        createPublicButton.onClick.AddListener(() =>
        {
            CarGameLobby.Instance.CreateLobby(inputField.text, false);
        });
        createPrivateButton.onClick.AddListener(() =>
        {
            CarGameLobby.Instance.CreateLobby(inputField.text, true);
        });


    }
}
