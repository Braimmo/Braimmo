using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AgePick;
using TMPro;

namespace AgePick
{
    public class Age : MonoBehaviour
    {
        [SerializeField] Text name;
        [SerializeField] GameObject lockImage;
        [SerializeField] Image ageImage;
        [SerializeField] Sprite lockedStageSprite;
      //  [SerializeField] GameObject lockPanelPrefab;

        [SerializeField] GameObject stageExplainPanel;
        [SerializeField] GameObject StageName;
        [SerializeField] GameObject StageNameText;
        public JsonAgeStageFormat jsonAgeStage;
        public string prevAgeName;
        private bool isLocked;
        private LockPanel lp;
        passDataBetweenScene passData;
        
        void Awake(){
            //GameObject UnlockPanel = GameObject.Find("unlockPanel");
            //unlockPanel = UnlockPanel.GetComponent<UnlockPanel>();
            //lp = lockPanelPrefab.GetComponent<LockPanel>();
            isLocked = false;
        }
        public void setStage(){
            int stageSpriteIndex = jsonAgeStage.AgeSprite;
            ageImage.sprite = Resources.Load<Sprite>("AgeDB/Age/"+stageSpriteIndex);
            name.text = jsonAgeStage.AgeName; 

            var text = name.gameObject.GetComponent<TextMesh>();
            text.text = jsonAgeStage.AgeName; 

            lockImage.SetActive(false);
        }

        public void lockStage(){
            GetComponent<Image>().color = new Color32(130, 130, 130, 255);
            lockImage.SetActive(true);
            gameObject.GetComponent<Button>().interactable = false;
            transform.GetChild(1).GetComponent<Button>().interactable = false;
            StageName.GetComponent<SpriteRenderer>().sprite = lockedStageSprite;
           // StageName.transform.GetChild(0).GetComponent<TextMesh>().color = new Color32(57,57,57,255);
           StageNameText.GetComponent<TextMesh>().color = new Color32(57,57,57,255);
            isLocked = true;
        }

        public void clickAgeName(){            
            if(!isLocked){
                Debug.Log("click unlock panel");
                stageExplainPanel.SetActive(true);
                //unlockPanel.setUnlockPanel(jsonAgeStage);
            }
            else if(isLocked){
                // lockPanelPrefab.SetActive(true);
                // Debug.Log("setActive");
                // lp.setLockpanelText(prevAgeName);
            }
        }

        public void clickAge(){
            passDataBetweenScene.passData.setAgeInfo(jsonAgeStage);
            UnityEngine.SceneManagement.SceneManager.LoadScene("StoryPick");
        }
    }
}

