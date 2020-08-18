using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class commonDelete : MonoBehaviour, IPointerDownHandler
{
      public void OnPointerDown(PointerEventData eventData){
        eventData.pointerEnter.transform.parent.gameObject.SetActive(false);
    }
}
