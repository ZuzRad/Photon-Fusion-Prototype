using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : NetworkBehaviour, IPlayerInputProvider
{
    private PlayerInputs input;
    private InputActionAsset inputAsset;
    private InputActionMap actionMap;

    private InputAction forwardInput;
    private InputAction backwardInput;
    private InputAction rotateInput;
    private InputAction fireInput;
    private InputAction tabInput;

    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        actionMap = inputAsset.FindActionMap("Movement");

        forwardInput = actionMap.FindAction("Forward");
        backwardInput = actionMap.FindAction("Backward");
        rotateInput = actionMap.FindAction("Rotation");
        fireInput = actionMap.FindAction("Fire");
        tabInput = actionMap.FindAction("Tab");
    }

    public override void FixedUpdateNetwork()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        input.forward = forwardInput.ReadValue<float>();
        input.backward = backwardInput.ReadValue<float>();
        input.rotate = rotateInput.ReadValue<Vector2>();
        input.fire = fireInput.IsPressed();
        input.tab = tabInput.IsPressed();
    }

    public PlayerInputs GetInputs()
    {
        return input;
    }
}
