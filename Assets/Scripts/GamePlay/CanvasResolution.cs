using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasResolution : MonoBehaviour
{
    public GameObject StatBox;
    public float ratio_width;
    public float ratio_height;
    public float theRatio;

    void Awake()
    {
        float targetAspect = 16.0f / 9.0f;
        float windowAspect = (float) Screen.width / (float) Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        float scaleWidth = 1.0f / scaleHeight;
        if(scaleHeight < 1.0f)
        {
            ratio_width = 1.0f;
            ratio_height = scaleHeight;
            theRatio = (1.0f - ratio_height) / 2.0f;
        }
        else
        {
            ratio_width = scaleWidth;
            ratio_height = 1.0f;
            StatBox.transform.localPosition = new Vector3(StatBox.transform.localPosition.x, StatBox.transform.localPosition.y * scaleWidth, StatBox.transform.localPosition.z);
        }
    }


}
