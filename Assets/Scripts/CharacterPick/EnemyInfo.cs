using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CharacterPick{
    public class EnemyInfo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] public Image enemyImage;
        public int enemyID;
        public EnemyInformation enemyInfo;
        
        [SerializeField] GameObject EnemyExplainPanel;
        CharacterExplainPanel ep;

        private float clickTime = 0.3f,  timeMouseDown = 0f, pressTime = 0f;
        private bool mouseDown;
        private bool isExplainPanelInstantiated;
        GameObject newExplainPanel;
        EnemyExplainPanel explainScript;

        void Awake(){
            isExplainPanelInstantiated = false;

        }
        
        void Update(){
            if(mouseDown){
                timeMouseDown += Time.deltaTime;
                if(timeMouseDown > clickTime){
                    createExplainPanel();
                    isExplainPanelInstantiated = true;
                }
            }
            if(newExplainPanel == null){
                isExplainPanelInstantiated = false;
            }
        }
        
        public void OnPointerDown(PointerEventData eventData){
            mouseDown = true;
        }

        public void OnPointerUp(PointerEventData eventData){
            mouseDown = false;
            pressTime = timeMouseDown;
            timeMouseDown = 0f;
        }


         void createExplainPanel()
        {
            if (!isExplainPanelInstantiated)
            {
                Debug.Log("invoke explain panel");
                newExplainPanel = Instantiate(EnemyExplainPanel, transform.position, transform.rotation) as GameObject;
                newExplainPanel.transform.SetParent(this.transform);
                newExplainPanel.transform.localPosition = new Vector3(transform.position.x + 200, 0, 0);
                newExplainPanel.transform.localScale = new Vector3(1, 1, 1);
                newExplainPanel.transform.SetParent(this.transform.parent.parent);

                explainScript = newExplainPanel.GetComponent<EnemyExplainPanel>();
                explainScript.setEnemyExplainPanel(enemyInfo);
            }
        }

    }
}
