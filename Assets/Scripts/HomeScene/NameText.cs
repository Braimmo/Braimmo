using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class NameText : MonoBehaviour
{
    public Text text;
    UserInformation data;
    String name;
    public int AccountID;
    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);
        name = data.name;
        text.text = name + "님";
    }
}
