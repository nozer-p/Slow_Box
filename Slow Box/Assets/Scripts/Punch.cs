using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private List<Collider> enemies = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !enemies.Contains(other))
        {
            enemies.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && enemies.Contains(other))
        {
            enemies.Remove(other);
        }
    }

    public List<Collider> GetEnemies()
    {
        return enemies;
    }
}