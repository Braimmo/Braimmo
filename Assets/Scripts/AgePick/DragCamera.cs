using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AgePick
{
    public class DragCamera : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public GameObject theCamera;

        private RectTransform cameraRectTransform;
        private Canvas canvas;
        void Awake()
        {
            canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
            cameraRectTransform = theCamera.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            float theCameraX = theCamera.transform.localPosition.x;
            float toMoveX = eventData.delta.x / canvas.scaleFactor;
            
            if(theCameraX - toMoveX < 0){
                toMoveX = 0;
            }
            else if(theCameraX < 10 && toMoveX > 0){
                toMoveX = 0;
            }

            if(theCameraX > 1120 && toMoveX < 0){
                toMoveX = 0;
            }
            else if(theCameraX - toMoveX > 1125){
                toMoveX = 0;
            }
            

            cameraRectTransform.anchoredPosition -= new Vector2(toMoveX, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }
    }
}