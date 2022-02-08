using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickableUI : MonoBehaviour, IPointerClickHandler
{
    private float clickTime;            // time of last click
    private int clickCount = 0;         // current click count


    public void OnPointerClick(PointerEventData eventData)
    {
        // get interval between this click and the previous one (check for double click)
        float interval = eventData.clickTime - clickTime;

        // if this is double click, change click count
        if (interval < 0.5 && interval > 0 && clickCount != 2)
            clickCount = 2;
        else
            clickCount = 1;

        // reset click time
        clickTime = eventData.clickTime;

        // single click
        if (clickCount == 1)
        {
            GameManager.instance.onScreenClicked();
        }

        // double click
        if (clickCount == 2)
        {
            // enter code here
        }
    }
}
