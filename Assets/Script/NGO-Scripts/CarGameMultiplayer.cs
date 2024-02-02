using Unity.Netcode;

public class CarGameMultiplayer : NetworkBehaviour
{
    public static CarGameMultiplayer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
