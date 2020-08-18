using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropBackground : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    
    //[SerializeField] GameObject explainPanel;
     public void OnPointerDown(PointerEventData eventData){
         Debug.Log("touch background. delete panel");
         this.gameObject.SetActive(false);
     }
}
