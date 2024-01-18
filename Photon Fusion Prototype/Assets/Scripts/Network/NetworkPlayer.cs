using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    public List<TextMeshProUGUI> playerNickNameTM;

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }

    private bool isPublicJoinMessageSent = false;
    NetworkGameMesseges netMesseges;

    [SerializeField] public Player playerO;
    
    private void Awake()
    {
        netMesseges = GetComponent<NetworkGameMesseges>();
    }
    public override void Spawned()
    {
        base.Spawned();
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local player");
            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));
        }
        else
        {
            Debug.Log("Spawned remote player");
        }
        //PlayerPrefabConnector.Instance.ConnectPlayerScriptsNetwork(GetComponent<Player>(), Object.HasInputAuthority);

        transform.name = $"P_{Object.Id}";
        Runner.SetPlayerObject(Object.InputAuthority, Object);
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject playerLeftNetworkObject))
            {
                if (playerLeftNetworkObject == Object)
                {
                    Local.GetComponent<NetworkGameMesseges>().SendInGameRPCMessage(playerLeftNetworkObject.GetComponent<NetworkPlayer>().nickName.ToString(), "left");
                }
            }
        }

        if (player == Object.InputAuthority)
        {
           // PlayersManager.Instance.players.Remove(playerO);
            Runner.Despawn(Object);
        }
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log("Nickname changed to " + nickName);
        foreach( var playerNickText in playerNickNameTM)
        {
            playerNickText.text = nickName.ToString();
            //if(playerNickNameTM.IndexOf(playerNickText) == playerO.index)
            //{
            //    playerNickText.gameObject.layer = 24;
            //}
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log("RPC setNickName" + nickName);
        this.nickName = nickName;

        if (!isPublicJoinMessageSent)
        {
            //netMesseges.SendInGameRPCMessage(nickName, " joined");
            isPublicJoinMessageSent = true;
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendPoints(int index)
    {
        Debug.Log("RPC add points" + nickName);
        netMesseges.SendPoints(index);
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetWinner(string nickName, string points)
    {
        Debug.Log("RPC winner" + nickName);
        netMesseges.SendInGameRPCMessage(nickName, " wins!");
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetGameWinner(string nickName)
    {
        Debug.Log("RPC game winner" + nickName);
        netMesseges.SendInGameRPCMessage(nickName, " wins the entire game!");
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetReadyState(int index, bool isReady)
    {
        Debug.Log("RPC ready state " + nickName);
        netMesseges.SendReadyState(index, isReady);

    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetPing(int index, double ping)
    {
        Debug.Log("RPC ping state " + nickName);
        netMesseges.SendPing(index, ping);

    }

}
