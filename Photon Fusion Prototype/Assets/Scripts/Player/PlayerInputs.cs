using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputs : INetworkInput
{
    public float forward;
    public float backward;
    public float left;
    public float right;
    public Vector3 rotate;

    public static PlayerInputs None => new PlayerInputs();
}
