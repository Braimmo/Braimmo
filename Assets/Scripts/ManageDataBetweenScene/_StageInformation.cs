using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _StageInformation: MonoBehaviour{
   void Awake(){
      DontDestroyOnLoad(this.gameObject);
   }
   
   public int ageID;          public int stageID;
   public string stageInfo;   public int stageLevel;
   public string ageName;     public string ageInfo;
   public string emblemInfo;  public string emblemName;
   public int emblemID;       public List<JsonStageAwardFormat> awards;
   public int enemyNumber;    public List<int> enemyIds;
   public float enemyStatMultiplier;     public int charNumber;
   public float experience;
   public float money;        public int gem;

   public List<int> selectedCharacterIDs;
}
