using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardExplainPanel : MonoBehaviour
{
    [SerializeField] public Text awardName;
    [SerializeField] public Image awardImage;
    [SerializeField] public Text awardInfo;
    public void clickDeleteButton(){
        if(this != null){
            Destroy(gameObject);
        }
    }
    public void setAwardExplainPanel(string name, Image image, string info){
        awardName.text = name;
        awardImage.GetComponent<Image>().sprite = image.sprite;
        awardInfo.text = info;
    }

    // public void OnPointerDown(PointerEventData eventData){
    //     if(eventData.pointerEnter.tag == "awardExplainPanel"){
    //         Debug.Log("pointer down awardExplainPanel");
    //         Destroy(gameObject);
    //     }
    // }
}
