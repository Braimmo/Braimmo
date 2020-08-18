using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


//user에 대한 정보
public class UserInformation
{
    public string name; public float experience;
    public int gem; public float money;
    public int ageID; public int stageID;
    public int tutorialStage; public int level;

    public UserInformation()
    {
        this.name = "";
        this.experience = 0;
        this.gem = 0;
        this.money = 0;
        this.ageID = 0;
        this.stageID = 0;
        this.tutorialStage = 0;
        this.level = 0;
    }

    public UserInformation(string name, float experience, int gem, float money, int ageID, int stageID, int tutorialStage,int level){
        this.name = name;
        this.experience = experience;
        this.gem = gem;
        this.money = money;
        this.ageID = ageID;
        this.stageID = stageID;
        this.tutorialStage = tutorialStage;
        this.level = level;
    }

}

public class GameInformation
{
    public int ageID; public int stageID;
    public int characterID_1;
    public int characterID_2;
    public int characterID_3;

    public GameInformation()
    {
        this.ageID = 0;
        this.stageID = 0;
        this.characterID_1 = 0;
        this.characterID_2 = 0;
        this.characterID_3 = 0; 
    }
    public GameInformation(int ageID, int stageID, int characterID_1, int characterID_2, int characterID_3)
    {
        this.ageID = ageID;
        this.stageID = stageID;
        this.characterID_1 = characterID_1;
        this.characterID_2 = characterID_2;
        this.characterID_3 = characterID_3;
    }

}
