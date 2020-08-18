using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCamera : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject theCamera;
    private RectTransform cameraRectTransform;
    private Canvas canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        cameraRectTransform = theCamera.GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        print(theCamera.transform.position);
        float theCameraX = theCamera.transform.position.x;
        float theCameraY = theCamera.transform.position.y;
        float toMoveX = eventData.delta.x / canvas.scaleFactor;
        float toMoveY = eventData.delta.y / canvas.scaleFactor;
        if(theCameraX > 800 && toMoveX < 0)
            toMoveX = 0;
        else if(theCameraX < -650 && toMoveX > 0)
            toMoveX = 0;
        
        if(theCameraY > 620 && toMoveY < 0)
            toMoveY = 0;
        else if(theCameraY < -200 && toMoveY > 0)
            toMoveY = 0;

        cameraRectTransform.anchoredPosition -= new Vector2(toMoveX,toMoveY);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
