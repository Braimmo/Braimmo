using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;
using UnityEngine.EventSystems;

namespace CharacterPick{
    public class PickedCharacter : MonoBehaviour
    {
        public Image characterImage;
       // public Image innerSlot;
        public int characterIndex;
        public int characterID = -1;

        CharacterSlots characterSlots;
        PickedCharacterPanel pickedCharacterSlot;


        void Start(){
            GameObject CS = GameObject.Find("characterSlots");
            characterSlots = CS.GetComponent<CharacterSlots>();
            GameObject PCS = GameObject.Find("pickedCharacter");
            pickedCharacterSlot = PCS.GetComponent<PickedCharacterPanel>();
        }
        public void clickPickedCharacter(){
            if(characterImage.sprite != null){
                characterID = -1;
                characterImage.sprite = null;
                characterImage.GetComponent<Image>().color = new Color32(255,255,255,0);
                characterSlots.characters[characterIndex].isClicked = false;
                characterSlots.characters[characterIndex].checkImage.GetComponent<Image>().color = new Color32(255,255,255,0);
                pickedCharacterSlot.checkTargetSlot();
            }
        }
    }
}
