using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Each : MonoBehaviour
{
    public GameObject target;
    void Awake()
    {
        if(this.transform.name == "MainCamera")
        {
            target = this.transform.gameObject;
        }
        else
        {
            target = this.transform.parent.gameObject;
        }
    }
    void Start()
    {
        this.transform.parent = null;
    }
    void LateUpdate()
    {
        if(this.transform.name != "MainCamera")
        {
            if(target != null || !target.Equals(null))
            {
                //this.transform.parent = null;
                this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 12, target.transform.position.z - 12);
            }
            else if(this.transform.GetComponent<Camera>() == GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera)
            {
                print("changing to main camera");
                GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;
                Destroy(this.transform.gameObject);
            }
        }
    }
}
