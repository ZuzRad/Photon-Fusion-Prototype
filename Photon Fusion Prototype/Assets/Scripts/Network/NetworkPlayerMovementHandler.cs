using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayerMovementHandler : NetworkBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerController controller;


    private void ShowTable(bool isPressed)
    {
        player.UIController.gameObject.SetActive(isPressed);
    }

    public override void FixedUpdateNetwork()
    {
        if (NetworkPlayer.Local == player.netPlayer)
        {
            player.netPlayer.RPC_SetPing(player.index, Runner.GetPlayerRtt(Runner.LocalPlayer));
        }

        if (GetInput(out PlayerInputs networkInputData))
        {
            controller.inputs = networkInputData;
            controller.UpdatePosition(Runner.DeltaTime);
            ShowTable(networkInputData.tab);
        }
    }
}

