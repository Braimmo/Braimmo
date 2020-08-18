using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class forbiddenPanel : MonoBehaviour, IPointerDownHandler
{    
    [SerializeField] GameObject forbiddenPanelPrefab;
    public void OnPointerDown(PointerEventData eventData){
        eventData.pointerEnter.transform.parent.gameObject.SetActive(false);
    }

    public void clickDeleteButton(){
        //this.transform.parent.gameObject.SetActive(false);
        forbiddenPanelPrefab.SetActive(false);
    }
    
}
