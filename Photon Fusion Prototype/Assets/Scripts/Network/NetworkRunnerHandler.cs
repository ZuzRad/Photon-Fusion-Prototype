using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;

    private NetworkRunner networkRunner;


    public void StartServer(string sessionName)
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network runner";

        InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null, sessionName);

        Debug.Log("Server started");
    }

    public void StartHostMigration(HostMigrationToken hostMigrationToken)
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network runner - Migrated";

        var clientTask = InitializeNetworkRunnerHostMigration(networkRunner, hostMigrationToken);

        Debug.Log("Host migration started");
    }

    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if(sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        return sceneManager;
    }

    public virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized, string sessionName)
    {
        var sceneManager = GetSceneManager(runner);

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            Initialized = initialized,
            SceneManager = sceneManager
        });

    }


    public virtual Task InitializeNetworkRunnerHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        var sceneManager = GetSceneManager(runner);

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            SceneManager = sceneManager,
            HostMigrationToken = hostMigrationToken,
            HostMigrationResume = HostMigrationResume
        });

    }

    private void HostMigrationResume(NetworkRunner runner)
    {
        foreach (var resumeNetworkObject in runner.GetResumeSnapshotNetworkObjects())
        {
            Debug.Log(runner.GetResumeSnapshotNetworkObjects());
            //if (resumeNetworkObject.TryGetBehaviour<AdvanceCarController>(out var carController))
            //{
            //    runner.Spawn(resumeNetworkObject, position: carController.ReadPosition(), rotation: carController.ReadRotation(), onBeforeSpawned: (runner, newNO) =>
            //    {
            //        newNO.CopyStateFrom(resumeNetworkObject);
            //    });
            //}
        }
    }
}
