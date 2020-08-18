using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


public class ExpBar : MonoBehaviour
{
    public Image ExpImage;
    UserInformation data;
    float currentExp;
    float maxExp;
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
        currentExp = data.experience;
        maxExp = 1000; //그냥잡아놓은겈ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
        ExpImage.fillAmount = currentExp / maxExp;
    }


}
