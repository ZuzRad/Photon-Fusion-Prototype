using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    public TextMeshProUGUI playerNickText;

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
            EnableCamera();
        }
        else
        {
            Debug.Log("Spawned remote player");
            playerO.localUI.SetActive(false);
        }

        transform.name = $"P_{Object.Id}";
        Runner.SetPlayerObject(Object.InputAuthority, Object);
    }

    private void EnableCamera()
    {
        playerO.mainCamera.enabled = true;
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
        playerNickText.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log("RPC setNickName" + nickName);
        this.nickName = nickName;

        if (!isPublicJoinMessageSent)
        {
            netMesseges.SendInGameRPCMessage(nickName, " joined");
            isPublicJoinMessageSent = true;
        }
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetWinner(string nickName, string points)
    {
        Debug.Log("RPC winner" + nickName);
        netMesseges.SendInGameRPCMessage(nickName, " wins!");
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetPing(int index, double ping)
    {
        Debug.Log("RPC ping state " + nickName);
        netMesseges.SendPing(index, ping);

    }

}
