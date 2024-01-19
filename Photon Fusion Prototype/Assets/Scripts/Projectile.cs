using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force = 50f;
    private Player parent;
    private TickTimer expiredTime = TickTimer.None;

    public void InitializeFunc(Player player)
    {
        expiredTime = TickTimer.CreateFromSeconds(Runner, 10);
        parent = player;
    }

    public override void FixedUpdateNetwork()
    {
        if (expiredTime.Expired(Runner))
        {
            expiredTime = TickTimer.None;
            Runner.Despawn(GetComponent<NetworkObject>());
            gameObject.SetActive(false);
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            player.rb.AddForce(transform.forward * force, ForceMode.Impulse);

            NetworkPlayer netPlayer = player.netPlayer;
            if(NetworkPlayer.Local == netPlayer)
            {
                netPlayer.RPC_ShootMsg(netPlayer.nickName.ToString());
                Runner.Despawn(GetComponent<NetworkObject>());
                gameObject.SetActive(false);
            }
        }
    }
}
