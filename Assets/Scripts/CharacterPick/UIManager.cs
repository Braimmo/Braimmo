using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using CharacterPick;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


namespace CharacterPick
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject _CharacterSlots;
        [SerializeField] GameObject _PickedCharacterPanel;
        [SerializeField] GameObject _EnemyPanel;
        public int[] selectedCharacterIDs;
        PickedCharacterPanel pickedCharacterPanel;
        passDataBetweenScene passData;
        LoadCharInfo charInfo;

        void Start()
        {
            charInfo = transform.GetComponent<LoadCharInfo>();
            passData = charInfo.passData;

            
            loadUI();
        }

        void loadUI(){
            pickedCharacterPanel = _PickedCharacterPanel.GetComponent<PickedCharacterPanel>();
            
            _CharacterSlots.GetComponent<CharacterSlots>().loadInfo(charInfo.characterNum, charInfo.characters);
            pickedCharacterPanel.loadInfo(charInfo._stageInfo);
            _EnemyPanel.GetComponent<EnemyManager>().loadInfo(charInfo.enemyNum, charInfo._stageInfo.enemyIds);
        }

        public void goHome()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
        }

        public void goSetting()
        {
            Debug.Log("go setting scene");
        }
        public void clickGoBack()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StoryPick");
        }

        public void clickStartGame()
        {
            if (pickedCharacterPanel.isFull)
            {
                selectedCharacterIDs = new int[pickedCharacterPanel.pickedCharacterNum];
                for (int i = 0; i < pickedCharacterPanel.pickedCharacterNum; i++)
                {
                    selectedCharacterIDs[i] = PickedCharacterPanel.pickedCharacter[i].characterID;
                }

                passData.setCharacterInfo(selectedCharacterIDs);
                passData.savePickedStageInfoAsJson();
                UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlayScene");
            }
            else
            {
                
            }

        }
    }
}
