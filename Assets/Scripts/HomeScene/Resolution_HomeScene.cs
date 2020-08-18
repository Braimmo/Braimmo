using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution_HomeScene : MonoBehaviour
{
    public GameObject theCanvas;
    public GameObject[] theChildrens;
    public GameObject theCamera;

    void Awake()
    {
        int childrenCount = theCanvas.transform.childCount;
        print(childrenCount);
        theChildrens = new GameObject[childrenCount];
        for(int i = 0; i < childrenCount; i++)
        {
            theChildrens[i] = theCanvas.transform.GetChild(i).gameObject;
        }

        float targetAspect = 16.0f / 9.0f;
        float windowAspect = (float) Screen.width / (float) Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        float scaleWidth = 1.0f / scaleHeight;

        Camera camera = theCamera.GetComponent<Camera>();
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
        {//16:9 --> 20:10 이렇게 갈 때, 좀더 길어질때
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0f;
            camera.rect = rect;

            for(int i = 0; i < childrenCount; i++)
            {
                theChildrens[i].transform.localPosition = new Vector3(theChildrens[i].transform.localPosition.x, theChildrens[i].transform.localPosition.y * camera.rect.width, theChildrens[i].transform.localPosition.z);
                theChildrens[i].transform.localScale = new Vector3(theChildrens[i].transform.localScale.x, theChildrens[i].transform.localScale.y * camera.rect.width, theChildrens[i].transform.localScale.z);
            }
        }
        //thecanva.transform.localScale = Background.transform.localScale * camera.rect.width;

    }
}
