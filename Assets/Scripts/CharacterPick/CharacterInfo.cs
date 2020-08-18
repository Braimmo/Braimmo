using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CharacterPick
{
    public class CharacterInfo : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] GameObject characterExplainPanelPrefab;
        [SerializeField] public Image characterImage;
        [SerializeField] public Image checkImage;
        public int characterIndex;
        PickedCharacterPanel pickedCharacterPanel;
        public CharacterInformation_item charInfo;
        public bool isClicked;
        public bool isExplainPanelInstantiated;

        private int _index;
        private float clickTime = 0.3f, timeMouseDown = 0f, pressTime = 0f;
        private bool mouseDown;
        private bool isLongClick;
        GameObject newExplainPanel;
        CharacterExplainPanel explainScript; 

        void Awake()
        {
            isClicked = false;
            isLongClick = false;
            isExplainPanelInstantiated = false;
            GameObject PC = GameObject.Find("pickedCharacter");
            pickedCharacterPanel = PC.GetComponent<PickedCharacterPanel>();
        }

        void Update()
        {
            if (mouseDown)
            {
                timeMouseDown += Time.deltaTime;
                if (timeMouseDown > clickTime)
                {
                    createExplainPanel();
                    isExplainPanelInstantiated = true;
                }
            }
            if(newExplainPanel == null){
                isExplainPanelInstantiated = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            mouseDown = true;

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mouseDown = false;
            pressTime = timeMouseDown;
            timeMouseDown = 0f;
            if (pressTime < clickTime)
            {
                Debug.Log("short click");
                isLongClick = false;
            }
            else
            {
                Debug.Log("long click");
                isLongClick = true;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerEnter.tag == "characterSlot" && !isLongClick)
            {
                if (!isClicked && !pickedCharacterPanel.isFull && characterImage.sprite != null)
                {
                    checkImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    _index = PickedCharacterPanel.index;
                    isClicked = true;
                    PickedCharacterPanel.pickedCharacter[PickedCharacterPanel.index].characterImage.sprite = characterImage.sprite;
                    PickedCharacterPanel.pickedCharacter[PickedCharacterPanel.index].characterImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    PickedCharacterPanel.pickedCharacter[PickedCharacterPanel.index].characterIndex = characterIndex;
                    PickedCharacterPanel.pickedCharacter[PickedCharacterPanel.index].characterID = charInfo.ID_Character_Account;
                    pickedCharacterPanel.checkTargetSlot();

                }
                else if (isClicked)
                {
                    checkImage.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                    isClicked = false;
                    PickedCharacterPanel.pickedCharacter[_index].characterImage.sprite = null;
                    PickedCharacterPanel.pickedCharacter[_index].characterImage.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                    pickedCharacterPanel.checkTargetSlot();
                }
            }

        }



        void createExplainPanel()
        {
            if (!isExplainPanelInstantiated && characterImage.sprite != null)
            {
                Debug.Log("invoke explain panel");
                newExplainPanel = Instantiate(characterExplainPanelPrefab, transform.position, transform.rotation) as GameObject;
                newExplainPanel.transform.SetParent(this.transform.parent.parent.parent.parent);
                newExplainPanel.transform.localPosition = new Vector3(transform.position.x - 300, 0, 0);
                newExplainPanel.transform.localScale = new Vector3(1, 1, 1);
                
                newExplainPanel.transform.SetParent(this.transform.parent.parent.parent.parent.parent);

                explainScript = newExplainPanel.GetComponent<CharacterExplainPanel>();
                explainScript.setCharExplainPanel(charInfo);
            }
        }

    }
}
