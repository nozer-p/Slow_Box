using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Rigidbody[] allRigidbodies;
    private GameObject player;

    private bool side;
    private float timeBtwPunch;
    [SerializeField] private float startTimeBtwPunch;
    [SerializeField] private float freezeTimeBtwLeftPunch;
    [SerializeField] private float freezeTimeBtwRightPunch;

    [SerializeField] private float speedMove;

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
        gameObject.GetComponent<EnemyMovement>().enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.2f)
        {
            animator.SetBool("walk", false);
            if (timeBtwPunch <= 0f)
            {
                Time.timeScale = 1f;

                if (Random.Range(0f, 100f) > 50f)
                {
                    animator.SetTrigger("leftPunch");
                    side = false;
                }
                else
                {
                    animator.SetTrigger("rightPunch");
                    side = true;
                }
                timeBtwPunch = startTimeBtwPunch;
            }
            else
            {
                timeBtwPunch -= Time.deltaTime;
            }

            if (side)
            {
                if (timeBtwPunch <= freezeTimeBtwRightPunch)
                {
                    Time.timeScale = 0.1f;
                }
            }
            else
            {
                if (timeBtwPunch <= freezeTimeBtwLeftPunch)
                {
                    Time.timeScale = 0.1f;
                }
            }
        }
        else
        {
            animator.SetBool("walk", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedMove * Time.deltaTime);
        }
    }
}
