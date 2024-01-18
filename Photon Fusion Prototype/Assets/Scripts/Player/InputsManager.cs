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
    private InputAction leftInput;
    private InputAction rightInput;
    private InputAction rotateInput;

    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        actionMap = inputAsset.FindActionMap("Movement");

        forwardInput = actionMap.FindAction("Forward");
        backwardInput = actionMap.FindAction("Backward");
        leftInput = actionMap.FindAction("Left");
        rightInput = actionMap.FindAction("Right");
        rotateInput = actionMap.FindAction("Rotation");
    }

    public override void FixedUpdateNetwork()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        input.forward = forwardInput.ReadValue<float>();
        input.backward = backwardInput.ReadValue<float>();
        input.left = leftInput.ReadValue<float>();
        input.right = rightInput.ReadValue<float>();
        input.rotate = rotateInput.ReadValue<Vector2>();
    }

    public PlayerInputs GetInputs()
    {
        return input;
    }
}
