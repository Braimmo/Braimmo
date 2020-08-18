using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject inventoryPrefab;
    [SerializeField] GameObject equipmentPanelPrefab;
    [SerializeField] GameObject explainPanelPrefab;

    public static GameObject explainPanel;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    ItemExplainPanel itemExplainPanel;
    EquippableItem item;
    bool isEquipped;
    bool isItemNull;

    private void Awake(){
        isItemNull = false;

        GameObject CV = GameObject.Find("Background");
        canvas = CV.GetComponent<Canvas>();

        GameObject IV = GameObject.Find("Inventory");
        inventoryPrefab = IV.gameObject;

        GameObject EP = GameObject.Find("Equipment Panel");
        equipmentPanelPrefab = EP.gameObject;

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        GameObject UIManager = GameObject.Find("UIManager");
    }

    public void OnPointerDown(PointerEventData eventData){
        //지우지마시오
    }

    public void OnPointerClick(PointerEventData eventData){
        item = null;
        //if(!(savingPanelUI.activeSelf)){
            if(eventData.pointerEnter.tag == "Inventory"){
                item = (EquippableItem)eventData.pointerEnter.GetComponent<ItemSlot>().Item;
                isEquipped = false;
                if(item == null){
                    Debug.Log("null");
                    isItemNull = true;
                }
            }
            else if(eventData.pointerEnter.tag == "EquipmentPanel"){
                item = (EquippableItem)eventData.pointerEnter.GetComponentInParent<EquipmentSlot>().Item;
                isEquipped = true;
                if(item == null){
                    Debug.Log("null");
                    isItemNull = true;
                }
            }
            
            if(!isItemNull){
                Debug.Log("OnPointerDown");

                explainPanel = Instantiate(explainPanelPrefab, transform.position, transform.rotation);
                explainPanel.transform.SetParent(transform);
                explainPanel.transform.localScale = new Vector3(1,1,1);
                itemExplainPanel = explainPanel.GetComponent<ItemExplainPanel>();   

                if(!isEquipped){
                    Debug.Log("inventory");
                    explainPanel.GetComponent<RectTransform>().localPosition = new Vector3(this.gameObject.transform.position.x - (explainPanel.GetComponent<RectTransform>().rect.width/2), this.gameObject.transform.position.y,0);
                    explainPanel.transform.SetParent(canvas.transform);
                    itemExplainPanel.ExplainPanel = explainPanel;
                    itemExplainPanel.IsEquipped = false;
                    itemExplainPanel.Item = (EquippableItem)this.gameObject.GetComponent<ItemSlot>().Item;
                }
                else if(isEquipped){
                    Debug.Log("equipment panel");
                    explainPanel.GetComponent<RectTransform>().localPosition = new Vector3(this.gameObject.transform.position.x + (explainPanel.GetComponent<RectTransform>().rect.width/2), this.gameObject.transform.position.y,0);
                    explainPanel.transform.SetParent(canvas.transform);
                    itemExplainPanel.ExplainPanel = explainPanel;
                    itemExplainPanel.IsEquipped = true;
                    itemExplainPanel.Item = (EquippableItem)this.gameObject.GetComponent<EquipmentSlot>().Item;
                }
            }
       // }
    }


}
