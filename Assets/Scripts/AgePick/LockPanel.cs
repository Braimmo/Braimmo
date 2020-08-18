using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AgePick;
using TMPro;

namespace AgePick{
    public class LockPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

         void Awake(){
            
         }
        public void setLockpanelText(string prevAgeName){
            text.text = "["+prevAgeName+"]을 모두 클리어해야 합니다.";
        }

        public void clickOK(){
            this.gameObject.SetActive(false);
        }
    }
}
