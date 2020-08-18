using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempID : MonoBehaviour
{
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public string theID;
}
