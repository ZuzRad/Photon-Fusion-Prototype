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
        //player.playerUIController.tableUI.gameObject.SetActive(isPressed);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out PlayerInputs networkInputData))
        {
            controller.inputs = networkInputData;
            controller.UpdatePosition(Runner.DeltaTime);
            //ShowTable(networkInputData.Tab);
        }
    }
}

