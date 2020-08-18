using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;

public class HomeSceneMenuControl : MonoBehaviour
{
    public static UserInformation data;

    public Button characterRoom, storyMode, individualGame, teamGame, bossGame, shop, mail,
                  quest, giftBox, community, configuration, home, charge1;

    //public static int tutorial_Stage;

    void Awake()
    {
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/UserInformation/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);

        characterRoom.interactable = false;
        storyMode.interactable = false;
        individualGame.interactable = false;
        teamGame.interactable = false;
        bossGame.interactable = false;
        shop.interactable = false;
        mail.interactable = false;
        quest.interactable = false;
        giftBox.interactable = false;
        community.interactable = false;
        configuration.interactable = false;
        home.interactable = false;
        charge1.interactable = false;


        if (data.tutorialStage > 7)
        {
            characterRoom.interactable = true;
            storyMode.interactable = true;
        }


    }

    private void Update()
    {
        if (data.tutorialStage == 3)
        {
            if (DialogueManager.sentences.Count == 0)
                characterRoom.interactable = true;
        }


        if (data.tutorialStage >= 11)
        {
            {
                characterRoom.interactable = true;
                storyMode.interactable = true;
            }
        }
    }

    public static void SaveTutorial()
    {
        string jsonData = JsonConvert.SerializeObject(data);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/UserInformation/userInformation.json", jsonData);
        AssetDatabase.Refresh();
    }


}
