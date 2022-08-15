using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Rigidbody[] allRigidbodies;

    private void Start()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = true;
        }
    }

    public void OffAnimator()
    {
        animator.enabled = false;
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = false;
            //Time.timeScale = 0.3f;
        }
    }
}
