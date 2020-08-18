using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllCharacters : MonoBehaviour
{
    [SerializeField] Transform slotsParent;

    public CharacterSlot[] characterSlots;
    public List<CharacterInformation_item> allCharacter;  
    void Awake(){
        GameObject UIManager = GameObject.Find("UIManager");
        allCharacter = UIManager.GetComponent<JsonLoadItem>().characterStats;

        characterSlots = slotsParent.GetComponentsInChildren<CharacterSlot>();
        setCharacterSlots();
    }

    public void setCharacterSlots(){
        for(int i = 0; i < allCharacter.Count; i++){
            characterSlots[i].Character = allCharacter[i];
        }
    }
}
