using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Punch lPunch;
    [SerializeField] private Punch rPunch;

    private List<Collider> enemies = new List<Collider>();
    [SerializeField] private Rigidbody[] allRigidbodies;

    [SerializeField] private PlayerCollider playerCollider;
    [SerializeField] private float speedForCollider;

    private float timeBtwPunch;
    [SerializeField] private float startTimeBtwPunch;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetZ;
    private Vector3 newPos;

    private float delta;
    private bool side;

    void Start()
    {
        side = false;
        timeBtwPunch = 0f;
        animator = GetComponent<Animator>();
        SwipeDetector.SwipeEvent += OnSwipe;
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = true;
        }
    }

    private void OnSwipe(float delta)
    {
        this.delta = delta;
    }

    private void FixedUpdate()
    {
        Punch();

        if (playerCollider.IsDead())
        {
            Die();
        }
    }

    private void Punch()
    {
        if (timeBtwPunch <= 0f)
        {
            playerCollider.transform.position = Vector3.MoveTowards(playerCollider.transform.position, Vector3.zero, speedForCollider * Time.deltaTime);
            if (delta >= 1f)
            {
                animator.SetTrigger("rightPunch");
                newPos = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z + offsetZ);
                timeBtwPunch = startTimeBtwPunch;
                side = true;
                delta = 0f;
            }
            else if (delta <= -1f)
            {
                animator.SetTrigger("leftPunch");
                newPos = new Vector3(transform.position.x - offsetX, transform.position.y, transform.position.z + offsetZ);
                timeBtwPunch = startTimeBtwPunch;
                side = false;
                delta = 0f;
            }
        }
        else
        {
            playerCollider.transform.position = Vector3.MoveTowards(playerCollider.transform.position, newPos, speedForCollider * Time.deltaTime);
            timeBtwPunch -= Time.deltaTime;
        }

        if (side)
        {
            enemies = rPunch.GetEnemies();
        }
        else
        {
            enemies = lPunch.GetEnemies();
        }

        foreach (Collider en in enemies)
        {
            en.gameObject.GetComponent<Enemy>().OffAnimator();
        }
    }

    private float speedRotation;

    private void RotateToNearestEnemy(Transform target)
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
    }

    private void Die()
    {
        animator.enabled = false;
        Time.timeScale = 0.5f;
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = false;
        }
    }
}