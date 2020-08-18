using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;
using Newtonsoft.Json;

namespace CharacterPick
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] GameObject enemySlot;
        public int enemyNumber;
        public EnemyInfo[] enemies;
        //LoadCharInfo charInfo;
        public List<int> enemyIds;
        List<EnemyInformation> jsonEnemyInfos;

        void Awake()
        {
            TextAsset textData = Resources.Load("Json_GameInfo/EnemyInfo") as TextAsset;
            string charJsonData = textData.text;
           jsonEnemyInfos = JsonConvert.DeserializeObject<List<EnemyInformation>>(charJsonData);
        }

        public void loadInfo(int enemyNum, List<int> _enemyIds)
        {
            print("enemy load info");
            enemyNumber = enemyNum;
            enemyIds = new List<int>(_enemyIds);

            setEnemySlot();
        }

        public void setEnemySlot()
        {
            enemies = new EnemyInfo[enemyNumber];
            for (int i = 0; i < enemyNumber; i++)
            {
                var newEnemySlot = Instantiate(enemySlot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                newEnemySlot.transform.SetParent(transform);
                newEnemySlot.transform.localScale = new Vector3(1, 1, 1);

                enemies[i] = newEnemySlot.GetComponent<EnemyInfo>();
                enemies[i].enemyID = enemyIds[i];
                enemies[i].enemyInfo = jsonEnemyInfos[enemyIds[i]];           
                enemies[i].enemyImage.sprite = Resources.Load<Sprite>("CharacterDB/0"); //json에서 받아와야 함
                enemies[i].enemyImage.color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
