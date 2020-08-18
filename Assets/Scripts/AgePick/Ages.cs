using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AgePick;
using UnityEngine.UI;

namespace AgePick{
    public class Ages : MonoBehaviour
    {
        [SerializeField] GameObject UIManager;
        public Age[] ageArray;
        List<JsonAgeStageFormat> jsonAgeStageFormat;
        int ageLevel;

        void OnValidate(){
            ageArray = this.transform.GetComponentsInChildren<Age>();
        }

        void Start(){
            JsonStageLoad jsonStageLoad = UIManager.GetComponent<JsonStageLoad>();
            jsonAgeStageFormat = new List<JsonAgeStageFormat>(jsonStageLoad.jsonAgeStageFormat);
            ageLevel = jsonStageLoad.ageLevel;
            setAge();
        }

        public void setAge(){
            for(int i = 0; i < ageArray.Length && i < jsonAgeStageFormat.Count; i++){
                ageArray[i].jsonAgeStage = jsonAgeStageFormat[i];
                ageArray[i].setStage();
                if(i > 0)
                    ageArray[i].prevAgeName = jsonAgeStageFormat[i-1].AgeName;
                if(i >= ageLevel)
                    ageArray[i].lockStage();
            }
        }
    }
}
