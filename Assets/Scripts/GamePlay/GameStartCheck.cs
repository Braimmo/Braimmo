using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartCheck : MonoBehaviour
{
    public bool pauseCheck = false; //false면 pause가 아니다. true면 pause가 맞다.
    void Awake()
    {
        PauseGame();
        Invoke("ResumeGame", 2.0f);
    }

    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            ResumeGame();
        }
    }
    void PauseGame()
    {
        pauseCheck = true;
    }

    void ResumeGame()
    {
        GameObject.Find("ReadyButton").SetActive(false);
        pauseCheck = false;
    }
}
