using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DamageTest : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!isServer) return;
        GameObject.Find("Capsule(Clone)").GetComponent<HealthController>().takeDamage(1);
    }
}
