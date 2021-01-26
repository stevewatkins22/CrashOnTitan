using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;

    private Vector2 inputVector;

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgImg.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2 + 1, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick Image
            joystickImg.rectTransform.anchoredPosition = new Vector2(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickImg.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }
}
