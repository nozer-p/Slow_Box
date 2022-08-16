using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private bool isDead;

    private void Start()
    {
        isDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isDead = true;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
