using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCheck : MonoBehaviour
{
    public bool pauseCheck = false; //false면 pause가 아니다. true면 pause가 맞다.
    public GameObject speedObject;
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

    public void ChangeGameSpeed()
    {
        if(speedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "x 1")
        {
            Time.timeScale = 2f;
            speedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x 2";
        }
        else
        {
            Time.timeScale = 1f;
            speedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x 1";
        }
    }
}
