using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;

[Singleton(Persistent = true, Automatic = true, HideFlags = HideFlags.None)]
public class GameHandler : MonoBehaviour, ISingleton
{
    [SerializeField] private NetworkRunnerHandler networkRunnerHandler;
    public string lobbyName;
    public List<Player> players;

    public void CreateRoom(string roomName)
    {
        lobbyName = roomName;
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
