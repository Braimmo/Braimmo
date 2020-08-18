using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution_age : MonoBehaviour
{
    [SerializeField] GameObject GoBackButton;
    [SerializeField] GameObject HomeButton;
    [SerializeField] GameObject SettingButton;
    [SerializeField] GameObject Ages;
    [SerializeField] GameObject StageExplainPanel;

    public float cameraScale;

    void Awake(){
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16/9); //가로/세로
        float scaleWidth = 1.0f / scaleHeight;
   
        if(scaleHeight < 1.0f){
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0f;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else{
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0f;

            GoBackButton.transform.localPosition = new Vector3(GoBackButton.transform.localPosition.x,      GoBackButton.transform.localPosition.y * rect.width,     GoBackButton.transform.localPosition.z);
            HomeButton.transform.localPosition = new Vector3(HomeButton.transform.localPosition.x,          HomeButton.transform.localPosition.y * rect.width,       HomeButton.transform.localPosition.z);
            SettingButton.transform.localPosition = new Vector3(SettingButton.transform.localPosition.x,    SettingButton.transform.localPosition.y * rect.width,    SettingButton.transform.localPosition.z);
            //Background.transform.localPosition = new Vector3(Background.transform.localPosition.x, Background.transform.localPosition.y * camera.rect.width,Background.transform.localPosition.z);
            //Ages.transform.localPosition = new Vector3(Ages.transform.localPosition.x,      Ages.transform.localPosition.y * rect.width,     Ages.transform.localPosition.z);
            StageExplainPanel.transform.localScale = new Vector3(StageExplainPanel.transform.localScale.x, StageExplainPanel.transform.localScale.y * rect.width, StageExplainPanel.transform.localScale.z);
        }
        cameraScale = camera.rect.width;
    }


}
