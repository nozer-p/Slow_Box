using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;

    private float timeBtwSpawn;
    [SerializeField] private float startTimeBtwSpawn;

    void Start()
    {
        timeBtwSpawn = 0f;
    }
    
    void Update()
    {
        if (timeBtwSpawn <= 0f)
        {
            Instantiate(prefabEnemy, transform.position, transform.rotation);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
