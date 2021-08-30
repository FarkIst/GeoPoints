using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickHelper : Toggle
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        POIDelegateHandler.OnDestroyPOI();
        POIDelegateHandler.OnSpawnPOI();
    }
}
