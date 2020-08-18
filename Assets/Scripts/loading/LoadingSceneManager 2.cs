using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class LoadingSceneManager2 : MonoBehaviour
{
    UserInformation data;
    public int AccountID = 0; // 계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    void Start()
    {
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);
        Debug.Log("Userinfo = " + data.tutorialStage);
    }
    public void goHomeScene()
    {
        if (data.tutorialStage == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlayScene");

        }
        else
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }

}
