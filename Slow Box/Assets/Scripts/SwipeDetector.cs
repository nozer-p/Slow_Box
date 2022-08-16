using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(float delta);

    private Vector2 tapPos;
    private Vector2 tapPosNew;

    [SerializeField] private float minDeadZone = 1f;
    private float delta = 0;

    private bool isSwiping;
    private bool isMobile;

    private void Start()
    {
        isSwiping = false;
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CheckSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    CheckSwipe();
                }
            }
        }
    }

    private void CheckSwipe()
    {
        tapPosNew = Vector2.zero;

        if (isSwiping)
        {
            if (!isMobile && Input.GetMouseButtonUp(0))
            {
                tapPosNew = (Vector2)Input.mousePosition;
            }
            else if (Input.touchCount > 0)
            {
                tapPosNew = Input.GetTouch(0).position;
            }
        }

        if (SwipeEvent != null)
        {
            delta = tapPosNew.x - tapPos.x;
            if (Mathf.Abs(delta) >= minDeadZone)
            {
                SwipeEvent(delta);
            }
            else
            {
                SwipeEvent(0f);
            }
        }

        ResetSwipe();
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        delta = 0f;
        tapPos = Vector2.zero;
        tapPosNew = Vector2.zero;
    }
}