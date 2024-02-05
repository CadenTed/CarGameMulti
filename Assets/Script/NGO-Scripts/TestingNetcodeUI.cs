using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{

    [SerializeField] private Button clientBtn;
    [SerializeField] private Button hostBtn;
    // Start is called before the first frame update
    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            Debug.Log("HOST");
            CarGameMultiplayer.Instance.StartHost();
            Hide();
        });
        clientBtn.onClick.AddListener(() =>
        {
            Debug.Log("HOST");
            CarGameMultiplayer.Instance.StartClient();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
