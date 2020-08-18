using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StagePanel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{   
    public int stageLevel;
    public bool isClickable = true;
    private SpriteRenderer sp;
    private stageExplainPanel stageExplainPanel;


    public Color32 normalColor = new Color32(255,255,255,255);
    public Color32 highlightedColor = new Color32(207,207,207,255);
    public Color32 pressedColor = new Color32(200,200,200,255);

    void Awake(){
        sp = gameObject.GetComponent<SpriteRenderer>();
        //collider = gameObject.GetComponent<BoxCollider2D>();
        stageExplainPanel = GameObject.Find("stageExplainPanel").GetComponent<stageExplainPanel>();
         addPhysics2DRaycaster();
    }

    //  void Update(){
    //     if(isTouched()){
    //         print("it is touched "+stageName);
    //     }
    // }

     void addPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isClickable){
            sp.color = highlightedColor;   
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
        if(isClickable){
            sp.color = normalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isClickable){
            sp.color = pressedColor;
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            stageExplainPanel.setStageInfo(stageLevel);
        }
    }



    // private bool isTouched(){
    //     bool result = false;
    //     if(Input.touchCount == 1){
    //         if(Input.touches[0].phase == TouchPhase.Ended){
    //             Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    //             Vector2 touchPos = new Vector2(wp.x, wp.y);
    //             if(collider == Physics2D.OverlapPoint(touchPos)){
    //                 result = true;
    //             }
    //         }
    //     }
    //     if(Input.GetMouseButtonUp(0)){
    //         Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         Vector2 mousePos = new Vector2(wp.x, wp.y);
    //         if(collider == Physics2D.OverlapPoint(mousePos)){
    //             result = true;
    //         }
    //     }
    //     return result;
    // }
   
}
