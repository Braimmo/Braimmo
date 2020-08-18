using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Award : MonoBehaviour
{  
    [SerializeField] public Image awardImage;
    [SerializeField] public Text awardName;
    
    // private int _award;
    // public JsonStageAwardFormat award{
    //     get {return _award;}
    //     set{
    //         _award = value;
    //         if(_award == null){
    //             awardImage.color = new Color(0,0,0,1f);
    //         }else{

    //         }
    //     }
    // }
    
   //[SerializeField] GameObject awardExplainPanelPrefab;
   // [SerializeField] Canvas canvas;
    GameObject awardExplainPanel;
    AwardExplainPanel awardEP;

    void Awake(){
      //  awardName = transform.GetChild(1).GetComponent<Text>();
    }

    public void clickAward(){
        // awardExplainPanel = Instantiate(awardExplainPanelPrefab, transform.position, transform.rotation) as GameObject;
        // awardExplainPanel.transform.SetParent(this.transform);
        // awardExplainPanel.transform.localPosition = new Vector3(transform.position.x - 250, transform.position.y, 0);
        // awardExplainPanel.transform.localScale = new Vector3(1,1,1);
        // awardExplainPanel.transform.SetParent(canvas.transform);
        
        // awardEP = awardExplainPanel.GetComponent<AwardExplainPanel>();
        // print("awardName: "+awardName.text);
        // awardEP.setAwardExplainPanel(awardName.text, awardImage.GetComponent<Image>(), awardInfo);
        //Debug.Log("awardInfo: "+awardInfo);
    }
}