using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

//모든 캐릭터와 enemy와 stage 정보를 담고 있는 json 생성
public class makeAccountIInfoJson : MonoBehaviour
{
    public List<StageInformation> stages;
    public List<EnemyInformation> enemies;
    public List<CharacterInformation_item> characters;

    //스테이지 json 생성
    //enemy json 생성
    void Start()
    {
        // makeStageJson();
         makeEnemyJson();
        //makeCharacterJson();
        
    }

    public void makeCharacterJson()
    {
        characters = new List<CharacterInformation_item>();

        for (int i = 0; i < 1; i++)
        {
            CharacterInformation_item character = new CharacterInformation_item();
            character.ID_Character_Global = i;
            character.ID_Character_Account = i;
            character.name = "Alice";
            character.level = 1;
            character.experience = 1000;
            
            character.health_origin = 100;
            character.damage_origin = 30;
            character.weaponRange_origin = 10;
            character.speed_origin = 10;
            character.defense_origin= 100;
            character.criticalRate_origin = 50; 
            character.health_item = 0;
            character.damage_item = 0;
            character.weaponRange_item = 0;
            character.speed_item = 0;
            character.defense_item = 0;
            character.criticalRate_item = 0; 
            character.health_total = character.health_origin + character.health_item;
            character.damage_total = character.damage_origin + character.damage_item;
            character.weaponRange_total = character.weaponRange_origin + character.weaponRange_item;
            character.speed_total = character.speed_origin + character.speed_item;
            character.defense_total = character.speed_origin + character.speed_item;
            character.criticalRate_total = character.criticalRate_origin + character.criticalRate_item;
            
            character.maxHealth =  character.health_total;
            character.prefabID = 3;
            characters.Add(character);
        }
        string jdata = JsonConvert.SerializeObject(characters);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_GameInfo/CharacterInfo.json", jdata);
    }

    public void makeEnemyJson()
    {
        enemies = new List<EnemyInformation>();

        EnemyInformation enemy = new EnemyInformation();
        enemy.characterID = 0;
        enemy.name = "카드병정1";
        enemy.level = 1;
        enemy.health = 100;
        enemy.damage = 10;
        enemy.weaponRange = 10;
        enemy.speed = 5;
        enemy.defense = 100;
        enemy.criticalRate = 50;
        enemy.criticalDamage = 10;
        enemy.prefabID = "3";       //Resource에 load할때는 "prefab_"+enemy.prefabID
        enemies.Add(enemy);

        EnemyInformation enemy2 = new EnemyInformation();
        enemy2.characterID = 1;
        enemy2.name = "카드병정2";
        enemy2.level = 1;
        enemy2.health = 100;
        enemy2.damage = 20;
        enemy2.weaponRange = 10;
        enemy2.speed = 7;
        enemy2.defense = 100;
        enemy2.criticalRate = 50;
        enemy2.criticalDamage = 50;
        enemy2.prefabID = "3";       //Resource에 load할때는 "prefab_"+enemy.prefabID
        enemies.Add(enemy2);

        string jdata = JsonConvert.SerializeObject(enemies);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_GameInfo/EnemyInfo.json", jdata);

        AssetDatabase.Refresh();
    }

    public void makeStageJson()
    {
        //스테이지 정보(age + stage) + 맵 level
        //reward 정보: emeblem, rewards
        //enemy 정보: enemy id, enemy num, enemy level

        stages = new List<StageInformation>();
        StageInformation stage = new StageInformation();
        makeAge(stage);
        stage.stageID = 0;
        stage.stageInfo = "카드병정 1마리를 물리치세요";
        stage.stageLevel = 1;
        stage.enemyNumber = 1;
        List<int> enemyIds = new List<int>(new int[] { 0 });
        stage.enemyIds = new List<int>(enemyIds);
        stage.charNumber = 1;
        stage.money = 1000;
        stage.gem = 100;
        stages.Add(stage);

        StageInformation stage1 = new StageInformation();
        makeAge(stage1);
        stage1.stageID = 1;
        stage1.stageInfo = "카드병정 1마리를 물리치세요";
        stage1.stageLevel = 2;
        stage1.enemyNumber = 1;
        enemyIds = new List<int>(new int[] { 0});
        stage1.enemyIds = new List<int>(enemyIds);
        stage1.charNumber = 1;
        stage1.money = 1000;
        stage1.gem = 100;
        stages.Add(stage1);

        StageInformation stage2 = new StageInformation();
        makeAge(stage2);
        stage2.stageID = 2;
        stage2.stageInfo = "카드병정 2마리를 물리치세요";
        stage2.stageLevel = 3;
        stage2.enemyNumber = 2;
        enemyIds = new List<int>(new int[] { 0, 1 });
        stage2.enemyIds = new List<int>(enemyIds);
        stage2.charNumber = 1;
        stage2.money = 1000;
        stage2.gem = 100;
        stages.Add(stage2);


        string jdata = JsonConvert.SerializeObject(stages);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_GameInfo/Age0/StageInfo.json", jdata);

        AssetDatabase.Refresh();
    }

    void makeAge(StageInformation _stage)
    {
        _stage.ageID = 0;
        _stage.ageName = "이상한 나라의 앨리스";
        _stage.ageInfo = "챕터 1은 이상한 나라의 앨리스입니다.\n붉은 여왕의 분노에 맞서 블러드 루비를 탈환하세요 .\n ";
        _stage.emblemID = 0;
        _stage.emblemName = "블러드 루비";
        _stage.emblemInfo = "게임 플레이시 카드 병정 1마리 소환 가능";

        List<JsonStageAwardFormat> awards = new List<JsonStageAwardFormat>();
        JsonStageAwardFormat _award = new JsonStageAwardFormat();
        _award.AwardImage = 0;
        _award.AwardName = "조건1";
        _award.AwardInfo = "뫄뫄하는 조건입니다";
        awards.Add(_award);
        
        _award = new JsonStageAwardFormat();
        _award.AwardImage = 1;
        _award.AwardName = "액션1";
        _award.AwardInfo = "뫄뫄하는 액션입니다";
        awards.Add(_award);

        _award = new JsonStageAwardFormat();
        _award.AwardImage = 2;
        _award.AwardName = "아이템1";
        _award.AwardInfo = "뫄뫄하는 아이템입니다";
        awards.Add(_award);

        _stage.awards = new List<JsonStageAwardFormat>(awards);
    }

}
