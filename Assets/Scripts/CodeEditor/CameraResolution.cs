using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{    
    public GameObject GoBackButton;
    public GameObject SaveButton;
    public GameObject InventoryButton;
    public GameObject CodeSlot;
    public float codeSlotX;
    public float cameraScale;
    
    void Awake()
    {
        float targetAspect = 16.0f / 9.0f;
        float windowAspect = (float) Screen.width / (float) Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        float scaleWidth = 1.0f / scaleHeight;
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        if(scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0f;
            //camera.rect = rect;

            //GoBackButton.transform.localPosition = new Vector3(GoBackButton.transform.localPosition.x * camera.rect.width, GoBackButton.transform.localPosition.y * camera.rect.width, GoBackButton.transform.localPosition.z);
            //SaveButton.transform.localPosition = new Vector3(SaveButton.transform.localPosition.x * camera.rect.width, SaveButton.transform.localPosition.y * camera.rect.width, SaveButton.transform.localPosition.z);
            //InventoryButton.transform.localPosition = new Vector3(InventoryButton.transform.localPosition.x * camera.rect.width, InventoryButton.transform.localPosition.y * camera.rect.width, InventoryButton.transform.localPosition.z);
            //CodeSlot.transform.localPosition = new Vector3(CodeSlot.transform.localPosition.x * camera.rect.width, CodeSlot.transform.localPosition.y * camera.rect.width, CodeSlot.transform.localPosition.z);
            
            GoBackButton.transform.localPosition = new Vector3(GoBackButton.transform.localPosition.x,          GoBackButton.transform.localPosition.y * rect.width,     GoBackButton.transform.localPosition.z);
            SaveButton.transform.localPosition = new Vector3(SaveButton.transform.localPosition.x,              SaveButton.transform.localPosition.y * rect.width,       SaveButton.transform.localPosition.z);
            InventoryButton.transform.localPosition = new Vector3(InventoryButton.transform.localPosition.x,    InventoryButton.transform.localPosition.y * rect.width,  InventoryButton.transform.localPosition.z);
            CodeSlot.transform.localPosition = new Vector3(CodeSlot.transform.localPosition.x,                  CodeSlot.transform.localPosition.y * rect.width,         CodeSlot.transform.localPosition.z);
        }
        cameraScale = camera.rect.width;
        codeSlotX = CodeSlot.transform.localPosition.x;
        CodeSlot.transform.localScale = CodeSlot.transform.localScale * cameraScale;
        print(codeSlotX);
        
    }
}
