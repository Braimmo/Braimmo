using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AgePick;
using TMPro;
using UnityEngine.SceneManagement;

namespace AgePick{
    public class UnlockPanel : MonoBehaviour
    {
        [SerializeField] Text stageName;
        [SerializeField] Text announceText; 
        [SerializeField] Image emblemImage;
        [SerializeField] Text emblemName; 
        [SerializeField] Image charIamge;
        [SerializeField] Text charName;
        [SerializeField] Text ageInfo;

        //UIManager UIManager;
        JsonAgeStageFormat _age;

        passDataBetweenScene passData;
        void Awake(){

            Debug.Log("unlock panel awake");

            passData = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<passDataBetweenScene>();
        }


        public void setUnlockPanel(JsonAgeStageFormat age){
            _age = age;
            stageName.text = age.AgeName;
            announceText.text = age.AgeName+ " 클리어시 보상으로 받을 수 있습니다.";
            emblemImage.sprite = Resources.Load<Sprite>("AgeDB/Emblem/"+age.EmblemSprite);
            emblemName.text = age.EmblemName;
            charIamge.sprite = Resources.Load<Sprite>("AgeDB/Character/"+age.CharSprite);
            charName.text = age.CharName;
            ageInfo.text = age.AgeInfo;
        }
       
        public void clickGoBackButton(){
            this.gameObject.SetActive(false);
        }

        public void clickEnterButton()
        {
            // saveStage.setAgeInfo(_age);
            passData.setAgeInfo(_age);
                UnityEngine.SceneManagement.SceneManager.LoadScene("StoryPick");

        }
    }
}
