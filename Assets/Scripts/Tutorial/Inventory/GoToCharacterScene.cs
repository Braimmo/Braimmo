using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToCharacterScene : MonoBehaviour
{
    public void goToCharacterScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterScene");
    }
}
