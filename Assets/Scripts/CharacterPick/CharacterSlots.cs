using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;

namespace CharacterPick
{
    public class CharacterSlots : MonoBehaviour
    {
        [SerializeField] GameObject characterSlotPrefab;
        public List<CharacterInfo> characters;
        public List<CharacterInformation_item> jsonCharacters;
        public int characterNum;
        LoadCharInfo charInfo;
        private int slotSize = 12;


        void Awake()
        {
            characters = new List<CharacterInfo>();
            CharacterInfo[] _characters = this.transform.GetComponentsInChildren<CharacterInfo>();
            for (int i = 0; i < _characters.Length; i++)
            {
                characters.Add(_characters[i]);
            }
            print("character Slot character Count: "+characters.Count);
        }


        public void loadInfo(int charNum, List<CharacterInformation_item> jsonCharInfo)
        {   
            characterNum = charNum;
            jsonCharacters = new List<CharacterInformation_item>(jsonCharInfo);
            setCharacterSlot();
        }

        public void setCharacterSlot()
        {
            if (characterNum > slotSize)
            {
                int count = (characterNum - slotSize) / 3;
                count = (characterNum % slotSize == 0) ? count : count + 1;
                for (int k = 0; k < count; k++)
                {
                    GameObject newCharSlots = Instantiate(characterSlotPrefab, transform.position, transform.rotation);
                    newCharSlots.transform.SetParent(this.transform);
                    newCharSlots.transform.localScale = new Vector3(1, 1, 1);
                    CharacterInfo[] _characters = newCharSlots.GetComponentsInChildren<CharacterInfo>();
                    for (int j = 0; j < _characters.Length; j++)
                    {
                        characters.Add(_characters[j]);
                    }
                }
            }
            int i;
            for (i = 0; i < characterNum; i++)
            {
                characters[i].characterIndex = i;
                characters[i].charInfo = jsonCharacters[i];
                characters[i].transform.GetComponent<Button>().interactable = true;
                characters[i].characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterDB/" + jsonCharacters[i].ID_Character_Account);
                Image img = characters[i].characterImage.GetComponent<Image>();
                var tmpColor = img.color;
                tmpColor.a = 255f;
                img.color = tmpColor;
            }
            for (; i < characters.Count; i++)
            {
                characters[i].transform.GetComponent<Button>().interactable = false;
            }
        }
    }
}
