using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountID : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public int theID;
}
