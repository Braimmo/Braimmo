using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarOrientation : MonoBehaviour
{
    public Camera main_camera;
    void Update()
    {
        try
        {
            main_camera = GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera;
            transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.back, main_camera.transform.rotation * Vector3.up);
        }
        catch (System.Exception)
        {
            
        }
    }
}
