using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(float delta);

    private Vector2 tapPos;
    private Vector2 tapPosNew;

    private float side = 0f;
    private float tapDelta = 0f;
    [SerializeField] private float maxDeadZone = 1f;
    [SerializeField] private float minTapDeltaPos = 1f;
    private float percentages = 0f;

    private bool isSwiping;
    private bool isMobile;

    private void Start()
    {
        side = 0f;
        tapDelta = 0f;
        if (maxDeadZone < 0)
        {
            maxDeadZone *= -1f;
        }
        else if (maxDeadZone == 0)
        {
            maxDeadZone = 1f;
        }
        percentages = 1f / maxDeadZone;
        isMobile = Application.isMobilePlatform;
        //isMobile = true;
    }

    private void FixedUpdate()
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
                ResetSwipe();
            }
            CheckSwipe();
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
                    ResetSwipe();
                }
            }
            CheckSwipe();
        }
    }

    public bool GetIsSwiping()
    {
        return isSwiping;
    }

    private void CheckSwipe()
    {
        if (isSwiping)
        {
            if (!isMobile)
            {
                tapPosNew = (Vector2)Input.mousePosition;
                CheckTapPosNew();
            }
            else
            {
                tapPosNew = Input.GetTouch(0).position;
                CheckTapPosNew();
            }
        }

        if (SwipeEvent != null)
        {
            float deltaTmp;
            if (tapDelta > maxDeadZone)
            {
                deltaTmp = maxDeadZone;
            }
            else
            {
                deltaTmp = tapDelta;
            }

            float delta = deltaTmp * percentages * side;
            SwipeEvent(delta);
        }
    }

    private void CheckTapPosNew()
    {
        float dif = Mathf.Abs(tapPosNew.x - tapPos.x);
        if (dif < tapDelta - minTapDeltaPos)
        {
            tapPos = tapPosNew;
            tapDelta = Mathf.Abs(tapPosNew.x - tapPos.x);
        }
        else if (dif > tapDelta)
        {
            tapDelta = dif;
        }

        side = 0;
        if (tapPos.x > tapPosNew.x + minTapDeltaPos)
        {
            side = -1f;
        }
        else if (tapPos.x < tapPosNew.x - minTapDeltaPos)
        {
            side = 1f;

        }
        
        //Debug.Log(dif);
        //Debug.Log(tapDelta - minTapDeltaPos);
        //Debug.Log(side);
    }

    private void ResetSwipe()
    {
        tapDelta = 0f;
        isSwiping = false;
        tapPos = Vector2.zero;
        tapPosNew = Vector2.zero;
    }
}