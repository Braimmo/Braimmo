using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoryModeManager : MonoBehaviour
{   
    [SerializeField] GameObject StageExplainPanel;
    public int lastStage;
    public int StageNum;
    public Stages Stages;
    stageExplainPanel sex;

    // AgePick.saveStageInfo saveStage;
    //싱글톤 passDataBetweenScene 만들기
    public passDataBetweenScene passData;

    void Awake()
    {


        if (GameObject.Find("PassStageInfoBetweenScenes_dontDestroy") != null)
        {
             passData = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<passDataBetweenScene>();
        }
        else{
            Debug.Log("NO dontdestroy");
        }
        GameObject ST = GameObject.Find("Stages");
        Stages = ST.GetComponent<Stages>();
        Stages.stageNum = StageNum;
        Stages.lastStage = lastStage;

        sex = StageExplainPanel.GetComponent<stageExplainPanel>();
    }



    public void goHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }
    public void goSetting()
    {
        Debug.Log("go setting scene");
    }

    public void gameStart(int level, StageInformation stageInfo)
    {
        print("uimanager entry");
        passData.setStoryInfo(stageInfo);
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterPick");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Resetter");
        //StartCoroutine(LoadAsynchronously("CharacterPick"));
       // sex.allow();
    }

    public void clickGoBackButton()
    {
        print("click go back button");
        UnityEngine.SceneManagement.SceneManager.LoadScene("AgePickScene");
    }

    IEnumerator LoadAsynchronously(string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone){
            Debug.Log(operation.progress);
            yield return null;
        }
    }
    
}
