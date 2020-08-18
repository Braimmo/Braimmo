using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class outerBox : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData){
        if(eventData.pointerEnter.tag == "awardExplainPanel"){
            Debug.Log("pointer down awardExplainPanel");
            Destroy(transform.parent.gameObject);
        }
    }
}
