using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMainMove : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject theCamera;
    private Canvas canvas;
    public GameObject ZoomInImage;
    public GameObject ZoomOutImage;
    public int zoomState = 0; //0이 제일 높고, 3이 제일 낮은 카메라 위치
    public float originalCameraPositionY;
    public float originalCameraPositionX;
    public float originalCameraPositionZ;
    void Awake()
    {
        canvas = this.transform.GetComponent<Canvas>();
        originalCameraPositionX = theCamera.transform.position.x;
        originalCameraPositionY = theCamera.transform.position.y;
        originalCameraPositionZ = theCamera.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera == theCamera.GetComponent<Camera>())
        {
            ZoomInImage.SetActive(true);
            ZoomOutImage.SetActive(true);
        }
        else
        {
            ZoomInImage.SetActive(false);
            ZoomOutImage.SetActive(false);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void clickZoomIn()
    {
        if(zoomState == 3)
        {
            
        }
        else
        {
            zoomState++;
            theCamera.transform.position = new Vector3(originalCameraPositionX, theCamera.transform.position.y - (originalCameraPositionY / 4) , originalCameraPositionZ);
        }
        print(zoomState);
    }
    public void clickZoomOut()
    {
        if(zoomState == 0)
        {
            
        }
        else
        {
            zoomState--;
            theCamera.transform.position = new Vector3(originalCameraPositionX, theCamera.transform.position.y + (originalCameraPositionY / 4) , originalCameraPositionZ);
        }
        print(zoomState);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera == theCamera.GetComponent<Camera>())
        {
            float theCameraX = theCamera.transform.position.x-originalCameraPositionX; //제일 처음은 다 0
            float theCameraY = theCamera.transform.position.y-originalCameraPositionY;
            float theCameraZ = theCamera.transform.position.z-originalCameraPositionZ;

            float toMoveX = eventData.delta.x / canvas.scaleFactor / 50;
            float toMoveZ = eventData.delta.y / canvas.scaleFactor / 50;

            if(theCameraX >= (3.0f*zoomState) && toMoveX < 0)
            {
                toMoveX = 0;
            }
            else if(theCameraX < (-3.0f*zoomState) && toMoveX > 0)
            {
                toMoveX = 0;
            }

            if(theCameraZ >= (8*zoomState) && toMoveZ  < 0)
            {
                toMoveZ = 0;
            }
            else if(theCameraZ < (-1*zoomState) && toMoveZ > 0)
            {
                toMoveZ = 0;
            }
            
            theCamera.transform.position -= new Vector3(toMoveX, 0, toMoveZ);
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {

    }

}
