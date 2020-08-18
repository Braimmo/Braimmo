using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution_charPick : MonoBehaviour
{
    [SerializeField] GameObject GoBackButton;
    [SerializeField] GameObject HomeButton;
    [SerializeField] GameObject SettingButton;
    [SerializeField] GameObject gameStartButton;
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject background;

    public float cameraScale;

    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9); //가로/세로
        float scaleWidth = 1.0f / scaleHeight;

        if (scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0f;
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

            GoBackButton.transform.localPosition = new Vector3(GoBackButton.transform.localPosition.x, GoBackButton.transform.localPosition.y * rect.width, GoBackButton.transform.localPosition.z);
            HomeButton.transform.localPosition = new Vector3(HomeButton.transform.localPosition.x, HomeButton.transform.localPosition.y * rect.width, HomeButton.transform.localPosition.z);
            SettingButton.transform.localPosition = new Vector3(SettingButton.transform.localPosition.x, SettingButton.transform.localPosition.y * rect.width, SettingButton.transform.localPosition.z);
           // gameStartButton.transform.localPosition = new Vector3(gameStartButton.transform.localPosition.x, (float)(gameStartButton.transform.localPosition.y * rect.width), gameStartButton.transform.localPosition.z);
            
            characterPanel.transform.localPosition = new Vector3(characterPanel.transform.localPosition.x, characterPanel.transform.localPosition.y * rect.width, characterPanel.transform.localPosition.z);
            //background.transform.localPosition = new Vector3(background.transform.localPosition.x, background.transform.localPosition.y * rect.width, background.transform.localPosition.z);
        }
        cameraScale = camera.rect.width;
        //characterPanel.transform.localScale = new Vector3(characterPanel.transform.localScale.x * rect.width, characterPanel.transform.localScale.y * rect.width, characterPanel.transform.localScale.z);
        //background.transform.localScale = new Vector3(background.transform.localScale.x, background.transform.localScale.y * rect.width, background.transform.localScale.z);
        // GoBackButton.transform.localScale = new Vector3(GoBackButton.transform.localScale.x, GoBackButton.transform.localScale.y * rect.width, GoBackButton.transform.localScale.z);
        // HomeButton.transform.localScale = new Vector3(HomeButton.transform.localScale.x , HomeButton.transform.localScale.y * rect.width, HomeButton.transform.localScale.z);
        // SettingButton.transform.localScale = new Vector3(SettingButton.transform.localScale.x, SettingButton.transform.localScale.y * rect.width, SettingButton.transform.localScale.z);
        gameStartButton.transform.localScale = new Vector3(gameStartButton.transform.localScale.x, gameStartButton.transform.localScale.y * rect.width, gameStartButton.transform.localScale.z);
    }
}

