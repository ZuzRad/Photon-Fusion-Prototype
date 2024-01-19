using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.InputSystem;

public class NetworkSpawner : SimulationBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject playerPrefab;

    InputsManager playerMovementHandler;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void OnInput(NetworkRunner runner, NetworkInput input) 
    {
        if (playerMovementHandler == null && NetworkPlayer.Local != null)
        {
            InputsManager localInputsManager = NetworkPlayer.Local.GetComponent<InputsManager>();
            if (localInputsManager != null)
            {
                playerMovementHandler = localInputsManager;
            }
        }

        if (playerMovementHandler != null)
        {
            input.Set(playerMovementHandler.GetInputs());
        }
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
    {
        if (runner.IsServer)
        {
            Debug.Log(runner + "  " + player);
            Debug.Log("OnPlayerJoined we are server. Spawning player");
            runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
        }
    }

    public void OnConnectedToServer(NetworkRunner runner){ Debug.Log("OnConnectedToServer"); }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { Debug.Log("OnShutDown"); }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public async void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("HostMigration");
        //PlayersManager.Instance.players.Clear();
        await runner.Shutdown(shutdownReason: ShutdownReason.HostMigration);

        FindObjectOfType<NetworkRunnerHandler>().StartHostMigration(hostMigrationToken);
    }
}


