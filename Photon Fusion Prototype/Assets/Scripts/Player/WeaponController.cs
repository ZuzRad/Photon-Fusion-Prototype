using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : NetworkBehaviour
{
    [SerializeField] private NetworkObject bullet;
    [SerializeField] private Player player;
    private float spawnDistance = 0.5f;
    private bool isShooting;
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out PlayerInputs networkInputData) && networkInputData.fire)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isShooting)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0,0.7f,0) + transform.forward * spawnDistance;
            Runner.Spawn(bullet, spawnPosition, transform.rotation, Object.InputAuthority, (runner, obj) =>
            {
                obj.GetComponent<Projectile>().InitializeFunc(player);
                obj.transform.position += obj.transform.forward;
            });
            StartCoroutine(ShootCoolDown());
        }
    }

    private IEnumerator ShootCoolDown()
    {
        isShooting = true;

        yield return new WaitForSeconds(0.2f);

        isShooting = false;
    }
}
