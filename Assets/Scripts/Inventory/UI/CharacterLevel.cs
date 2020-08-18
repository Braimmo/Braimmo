using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLevel : MonoBehaviour
{
    [SerializeField] Text characterName;
    [SerializeField] Text characterLevel;
    [SerializeField] Text characterExp;



    public void setCharacterLevelUI(CharacterInformation_item characterStat){
        characterName.text = characterStat.name;
        characterLevel.text = characterStat.level.ToString();
        characterExp.text = characterStat.experience.ToString() /*+ " / " + characterStat.maxExperience.ToString()*/;
    }


}
