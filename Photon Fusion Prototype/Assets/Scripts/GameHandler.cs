using UnityEngine;
using Unity.VisualScripting;

[Singleton(Persistent = true, Automatic = true, HideFlags = HideFlags.None)]
public class GameHandler : MonoBehaviour, ISingleton
{
    [SerializeField] private NetworkRunnerHandler networkRunnerHandler;

    public void CreateRoom(string roomName)
    {
        networkRunnerHandler.StartServer(roomName);
    }

    private void Awake()
    {
        Singleton<GameHandler>.Awake(this);
    }

    private void OnDestroy()
    {
        Singleton<GameHandler>.OnDestroy(this);
    }
}
