using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkGameMesseges : NetworkBehaviour
{
    GameMessegesHandler gameMessegesHandler;

    public void SendInGameRPCMessage(string userNickName, string message)
    {
        RPC_InGameMessage($"<b>{userNickName}</b> {message}");
    }


    public void SendPing(int index, double ping)
    {
        RPC_Ping(index, ping);
    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_InGameMessage(string message, RpcInfo info = default)
    {
        if(gameMessegesHandler == null)
        {
            gameMessegesHandler = NetworkPlayer.Local.playerO.GetComponentInChildren<GameMessegesHandler>();
        }

        if(gameMessegesHandler != null)
        {
            gameMessegesHandler.OnGameMessegeReceived(message);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_Ping(int index, double ping, RpcInfo info = default)
    {
        if (gameMessegesHandler == null)
        {
            gameMessegesHandler = NetworkPlayer.Local.playerO.GetComponentInChildren<GameMessegesHandler>();
        }

        if (gameMessegesHandler != null)
        {
            gameMessegesHandler.OnPingReceived(index, ping);
        }
    }
}
