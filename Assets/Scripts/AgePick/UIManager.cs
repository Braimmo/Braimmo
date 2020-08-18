using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AgePick;

namespace  AgePick
{
    public class UIManager : MonoBehaviour
    {           
        [SerializeField] GameObject stageExplainPanel_alice;
        [SerializeField] GameObject lockPanel;
        
        public static UIManager instance;
        void Awake(){
           
            allPanelManage();
        }
        public void clickGoBack(){
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
        }

        public void clickHomeButton(){
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
        }



        public void allPanelManage(){
            //GameObject unlockPanel = GameObject.Find("unlockPanel");
            stageExplainPanel_alice.SetActive(false);
            //GameObject lockPanel = GameObject.Find("lockPanel");
            lockPanel.SetActive(false);
        }


    }
}
