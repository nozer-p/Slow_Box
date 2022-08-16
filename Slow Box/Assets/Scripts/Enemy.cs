using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Rigidbody[] allRigidbodies;
    private GameObject player;

    private float timeBtwPunch;
    [SerializeField] private float startTimeBtwPunch;
    [SerializeField] private float freezeTimeBtwPunch;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        animator = GetComponent<Animator>();
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = true;
        }
    }

    public void OffAnimator()
    {
        animator.enabled = false;
        Time.timeScale = 1f;
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = false;
        }
        gameObject.GetComponent<Enemy>().enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.1f)
        {
            if (timeBtwPunch <= 0f)
            {
                Time.timeScale = 1f;
                animator.SetTrigger("punch");
                timeBtwPunch = startTimeBtwPunch;
            }
            else
            {
                timeBtwPunch -= Time.deltaTime;
            }

            if (timeBtwPunch <= freezeTimeBtwPunch)
            {
                Time.timeScale = 0.1f;
            }
        }
    }
}
