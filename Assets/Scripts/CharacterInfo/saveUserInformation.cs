using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class saveUserInformation : MonoBehaviour
{
    public UserInformation userInformation;
    public static saveUserInformation instance;
    public int AccountID = 0; // 계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ

    void Awake()
    {
        Debug.Log("hi");
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Debug.Log("hi22");

        //userInformation = new UserInformation();


        //임시로 저장한 것 나중에 아래 setUSerInformation함수로 바꾸면 지우면 됨.
        userInformation = new UserInformation("yechan",500,2,1000,1,1,3,1);
        string jsonData = JsonConvert.SerializeObject(userInformation);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json", jsonData);


    }

    /* story모두에서 받아올 때 수정
    public void setUserInformation()
    {
        userInformation.name =
        userInformation.experience +=
        userInformation.gem +=
        userInformation.money +=
        userInformation.ageID =    
        userInformation.stageID = ;
        string jsonData = JsonConvert.SerializeObject(userInformation);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/UserInformation/userInformation.json", jsonData);


    }
    */

}
