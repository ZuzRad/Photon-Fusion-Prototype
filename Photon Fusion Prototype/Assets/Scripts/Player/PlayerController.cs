using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkRigidbody
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    public PlayerInputs inputs;
    public void UpdatePosition(float deltaTime)
    {

        float mouseRotate = (inputs.rotate.x / Screen.width) * 2 - 1;

        float rotationAngle = mouseRotate * rotationSpeed * deltaTime;
        Debug.Log(inputs.rotate);
        transform.Rotate(Vector3.up, rotationAngle);

        Vector3 localMoveDirection = new Vector3(inputs.right - inputs.left, 0f, inputs.forward - inputs.backward).normalized;
        Vector3 globalMoveDirection = transform.TransformDirection(localMoveDirection);

        Vector3 targetPosition = transform.position + deltaTime * moveSpeed * globalMoveDirection;

        transform.position = targetPosition;

    }
}
