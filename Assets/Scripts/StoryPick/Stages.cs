using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stages : MonoBehaviour
{
    public StagePanel[] stagePanel;
    //[SerializeField] GameObject stageExplainPanelPrefab;
    public GameObject stageExplainPanel;
    public List<StageInformation> stageInfo;    
    StoryModeManager storyModeManager;
    stageExplainPanel sePanel;

    public int stageNum = 0;
    public int lastStage;
    void OnValidate()
    {
        stagePanel = this.transform.GetComponentsInChildren<StagePanel>();
        Debug.Log("stagePanel: " + stagePanel.Length);

        // GameObject UIManager = GameObject.Find("StoryUIManager");
        // storyModeManager = UIManager.GetComponent<StoryModeManager>();
    }

    void Start()
    {
        GameObject SMM = GameObject.Find("StoryUIManager");
        JsonStoryStageLoad json = SMM.GetComponent<JsonStoryStageLoad>();
        //StoryModeManager manager = SMM.GetComponent<StoryModeManager>();

        stageInfo = new List<StageInformation>(json.stageInfo);
        lastStage = json.lastStage;

        sePanel = stageExplainPanel.GetComponent<stageExplainPanel>();
        sePanel.stageInfo = new List<StageInformation>(stageInfo);
        setStagePanel();
    }

    public void setStagePanel()
    {
        int i = 0;
        for (; i < lastStage && i < stagePanel.Length; i++)
        {
            stagePanel[i].stageLevel = i + 1;
        }
        for (; i < stagePanel.Length; i++)
        {
            stagePanel[i].stageLevel = i + 1;
            stagePanel[i].isClickable = false;
        }
        setStageExplainPanel(lastStage);
    }


    public void goStage(int level)
    {   
        setStageExplainPanel(level);
    }

    public void setStageExplainPanel(int level)
    {
       // sePanel.initializeStageInfo();
        sePanel.setStageInfo(level);

    }

}
