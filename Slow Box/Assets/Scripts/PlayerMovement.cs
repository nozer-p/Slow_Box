using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float timeBtwPunch;
    [SerializeField] private float startTimeBtwPunch;

    [SerializeField] private Transform punchPos;
    [SerializeField] private float punchRange;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float damage;
    private Animator animator;

    private List<Collider> enemies = new List<Collider>();

    void Start()
    {
        timeBtwPunch = 0f;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Punch();
    }

    private void Punch()
    {
        if (timeBtwPunch <= 0f)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetTrigger("punch");
                enemies.AddRange(Physics.OverlapSphere(punchPos.position, punchRange, enemy));
                foreach (Collider en in enemies)
                {
                    Debug.Log(damage);
                }
            }

            timeBtwPunch = startTimeBtwPunch;
        }
        else
        {
            timeBtwPunch -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(punchPos.position, punchRange);
    }
}