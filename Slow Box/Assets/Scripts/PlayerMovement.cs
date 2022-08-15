using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Punch lPunch;
    [SerializeField] private Punch rPunch;
    private List<Collider> enemies = new List<Collider>();

    private float timeBtwPunch;
    private Animator animator;

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
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("punch");
                timeBtwPunch = animator.GetCurrentAnimatorStateInfo(0).length;
            }
        }
        else
        {
            enemies = rPunch.GetEnemies();
            foreach (Collider en in enemies)
            {
                en.gameObject.GetComponent<Enemy>().OffAnimator();
            }
            timeBtwPunch -= Time.deltaTime;
        }
    }
}