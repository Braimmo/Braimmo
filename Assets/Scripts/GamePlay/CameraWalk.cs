using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CameraWalk : MonoBehaviour
{
    public Camera[] cameras;
    public Camera[] active_cameras;
    public Camera enabledCamera;
    public int enabledCounter;
    public bool passedTrueFlag;
    public GameObject shockwavePrefab;
    public GameObject theShockwave;
    public GameObject zoomIn, zoomOut;
    void Awake()
    {   
        theShockwave = Instantiate(shockwavePrefab,new Vector3(0,0,0),Quaternion.identity);

        passedTrueFlag = false;
        enabledCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

        cameras = Camera.allCameras;
        int a = 0;
        for(int i = 0; i < cameras.Length; i++)
        {
            if(cameras[i].transform.tag == "Camera_Active")
            {
                a++;
            }
            else if(cameras[i].transform.tag == "Camera_notActive")
            {
                cameras[i].enabled = false;
            }
        }
        active_cameras = new Camera[a];
        a=0;
        for(int i = 0; i < cameras.Length; i ++)
        {
            if(cameras[i].transform.tag == "Camera_Active")
            {
                active_cameras[a] = cameras[i];
                a++;
            }
        }
        for(int i = 0; i < active_cameras.Length; i++)
        {
            if(active_cameras[i].transform.name == "MainCamera")
            {
                enabledCounter = i;
                active_cameras[i].enabled = true;
            }
            else
            {   
                active_cameras[i].enabled = false;
            }
        }
        //만약 캐릭터가 2개면 전체 뷰 더해서 3개로 나와야함
        //print(active_cameras.Length);
    }
    void Update()
    {
        switchCamera();
        if(this.transform.GetComponent<GameEndCheck>().endCheck == true && passedTrueFlag == false)
        {
            passedTrueFlag = true;
            for(int i = 0; i < active_cameras.Length; i++)
            {
                GameObject.Find("MainCamera").transform.GetComponent<Camera>().enabled = true;
                enabledCamera = GameObject.Find("MainCamera").transform.GetComponent<Camera>();
            }
        }
        else
        {
            if(enabledCamera.name != "MainCamera")
            {        
                theShockwave.SetActive(true);
                theShockwave.transform.position = enabledCamera.transform.GetComponent<Camera_Each>().target.transform.position;
            }
            else
            {
                theShockwave.SetActive(false);
            }
        }
    }

    public void switchCamera()
    {
        if( Input.GetKeyDown("c") && this.transform.GetComponent<GameEndCheck>().endCheck == false)
        {
            print("camera switch button pressed. Also, camera count is " + active_cameras.Length);
            //cameraChangeCounter();
            cameraChangeNew();

        }
    }

    public void cameraChangeNew()
    {
        enabledCounter++;
        if(enabledCounter + 1 > active_cameras.Length)
        {
            enabledCounter = 0;
        }

        for(int i = 0; i < active_cameras.Length; i ++)
        {
            if(i == enabledCounter)
            {
                active_cameras[i].enabled = true;
                enabledCamera = active_cameras[i];
            }
            else
            {
                active_cameras[i].enabled = false;
            }
        }
    }

    private void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    private void cameraPositionChange(int camPosition)
    {
        if(active_cameras.Length < camPosition + 1)
        {
            camPosition = 0;
        }
        PlayerPrefs.SetInt("CameraPosition", camPosition);
        active_cameras[camPosition].enabled = true;
        for(int i = 0; i < active_cameras.Length; i++)
        {
            if(i != camPosition)
            {
                active_cameras[i].enabled = false;
            }
        }
        enabledCamera = active_cameras[camPosition];
    }
}
