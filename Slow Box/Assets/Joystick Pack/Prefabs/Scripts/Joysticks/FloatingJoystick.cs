using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : Joystick
{
    public bool isActive;
    private Color color;
    private Vector3 startPos;

    protected override void Start()
    {
        base.Start();
        startPos = background.transform.position;
        color = background.gameObject.GetComponent<Image>().color;
        Standart();
    }

    private void Standart()
    {
        background.transform.position = startPos;
        background.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.2f);
        handle.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.2f);
    }
    private void Active()
    {
        background.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
        handle.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
        background.gameObject.SetActive(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isActive = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        Active();
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isActive = false;
        Standart();
        base.OnPointerUp(eventData);
    }
}