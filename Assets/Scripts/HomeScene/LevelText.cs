using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class LevelText : MonoBehaviour
{
    public Text text;
    UserInformation data;
    int level;
    public int AccountID;
    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
    }
    void Start()
    {
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);
        level = data.level;
        string a = level.ToString();
        text.text = "LV." + a;
    }
}
