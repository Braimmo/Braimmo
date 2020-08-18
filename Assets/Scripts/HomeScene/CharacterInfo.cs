using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class CharacterInfo : MonoBehaviour
{
    public Text levelText;
    public Text nameText;
    public Text ExpText;
    public Text moneyText;
    public Text gemText;
    public Image ExpImage;

    UserInformation data;
    int level;
    String name;
    float currentExp;
    float maxExp;
    float money, gem;

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
        name = data.name;
        money = data.money;
        gem = data.gem;

        levelText.text = "LV." + level;
        nameText.text = name + "님";
        currentExp = data.experience;
        maxExp = 1000;
        ExpImage.fillAmount = currentExp / maxExp;
        ExpText.text = currentExp + "/" + maxExp;
        String a = money.ToString();
        moneyText.text = a;
        String b = gem.ToString();
        gemText.text = b;

    }

}
