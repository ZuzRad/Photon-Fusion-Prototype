using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject localUI;
    public Rigidbody rb;
    public PlayerUIController UIController;
    public int index;
    public NetworkPlayer netPlayer;
}
