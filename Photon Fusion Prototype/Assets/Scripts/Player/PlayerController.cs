using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkRigidbody
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float pitchSpeed = 180f;

    public PlayerInputs inputs;
    public Transform cameraTransform;

    private float minPitch = -45f;
    private float maxPitch = 90f;

    private float currentPitch = 0f; // Dodano zmienn¹ monitoruj¹c¹ aktualn¹ rotacjê kamery

    public void UpdatePosition(float deltaTime)
    {
        float mouseRotateX = (inputs.rotate.x / Screen.width) * 2 - 1;
        float mouseRotateY = (inputs.rotate.y / Screen.height) * 2 - 1;

        float rotationAngleX = mouseRotateX * rotationSpeed * deltaTime;
        float rotationAngleY = mouseRotateY * pitchSpeed * deltaTime;

        transform.Rotate(Vector3.up, rotationAngleX);

        currentPitch = Mathf.Clamp(currentPitch - rotationAngleY, minPitch, maxPitch);

        cameraTransform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);

        Vector3 localMoveDirection = new Vector3(0f, 0f, inputs.forward - inputs.backward).normalized;
        Vector3 globalMoveDirection = transform.TransformDirection(localMoveDirection);

        Vector3 targetPosition = transform.position + deltaTime * moveSpeed * globalMoveDirection;

        transform.position = targetPosition;
    }
}
