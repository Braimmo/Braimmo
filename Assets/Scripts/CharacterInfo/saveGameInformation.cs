using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class saveGameInformation : MonoBehaviour
{
    public GameInformation gameInformation;
    public static saveGameInformation instance;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        gameInformation = new GameInformation(0, 0, 1, 2, 3);
        string jsonData = JsonConvert.SerializeObject(gameInformation);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/UserInformation/gameInformation.json", jsonData);

    }


    // Update is called once per frame

}
