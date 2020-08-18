using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameEndCheck : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] enemies;
    public DataFromGamePlay dataToPass;
    public List<string> IDsToPass;
    public bool endCheck;
    public string theWinLose;

    public Canvas gameCanvas;
    public Canvas endCanvas;
    public GameObject awardPage_1, awardPage_3;
    public bool innerEndCheck;
    public int AccountID;

    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        endCheck = false;
        innerEndCheck = false;
        dataToPass = new DataFromGamePlay();
        IDsToPass = new List<string>();
        theWinLose = "Null";
        endCanvas.enabled = false;
    }


    void FixedUpdate()
    {
        if(endCheck == false && innerEndCheck == false)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(players.Length == 0)
            {
                theWinLose = "Lose";
                print("enemies win!");
                Invoke("EndCredit", 1f);
                print("Ended. giving 1 second delay");
                innerEndCheck = true;
            }
            else if(enemies.Length == 0)
            {
                if(endCheck == false && innerEndCheck == false)
                {
                    theWinLose = "Win";
                    print("players win!");
                    GameObject[] allWinners = GameObject.FindGameObjectsWithTag("Alive");
                    for(int i = 0; i < allWinners.Length; i++)
                    {
                        allWinners[i].transform.parent.GetComponent<CharacterMovement>().danceMotion();
                    }
                    Invoke("EndCredit", 3f);
                    print("Ended. giving 10 second delay for dancing time");
                    innerEndCheck = true;
                }
            }
        }
    }

    public void EndCredit()
    {
        print("EndCredit!!");
        endCheck = true;

        if(theWinLose != "Null")
        {
            CreateDataToPass(theWinLose);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("GameEndScene", LoadSceneMode.Single);
            gameCanvas.enabled = false;
            endCanvas.enabled = true;
            startEndCredit(theWinLose);
        }
        //Time.timeScale = 0;
    }

    public void CreateDataToPass(string winLose)
    {
        dataToPass.gameWin_Lose = winLose;
        float maxExp = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<StageInformation>().experience;
        float maxMoney = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<StageInformation>().money;
        float maxGem = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<StageInformation>().gem;
        if(winLose == "Win")
        {
            dataToPass.experience = maxExp;
            dataToPass.money = Mathf.RoundToInt(Random.Range(maxMoney * 8 / 10, maxMoney + 1));
            dataToPass.gem = Mathf.RoundToInt(Random.Range(maxGem * 8 / 10, maxMoney + 1));
            dataToPass.awardIDs = IDsToPass;
        }
        else if(winLose == "Lose")
        {
            dataToPass.experience = Mathf.RoundToInt(Random.Range(maxExp * 1 / 10, maxExp * 2 / 10));
            dataToPass.money = Mathf.RoundToInt(Random.Range(maxMoney * 1 / 10, maxMoney * 2 / 10));
            dataToPass.gem = 0;
            dataToPass.awardIDs = IDsToPass;
        }

        string jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        UserInformation theUserInfo = JsonConvert.DeserializeObject<UserInformation>(jsonData);
        theUserInfo.gem += dataToPass.gem;
        theUserInfo.money += dataToPass.money;
        theUserInfo.experience += dataToPass.experience;
        string jdata = JsonConvert.SerializeObject(theUserInfo);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json",jdata); //ID 추가해주기


        //GameObject.Find("GameEnd_DontDestroy").GetComponent<FromGameToEnd>().experience = dataToPass.experience;
        //GameObject.Find("GameEnd_DontDestroy").GetComponent<FromGameToEnd>().money = dataToPass.money;
        //GameObject.Find("GameEnd_DontDestroy").GetComponent<FromGameToEnd>().gem = dataToPass.gem;
        //GameObject.Find("GameEnd_DontDestroy").GetComponent<FromGameToEnd>().gameWin_Lose = dataToPass.gameWin_Lose;
        //GameObject.Find("GameEnd_DontDestroy").GetComponent<FromGameToEnd>().awardIDs = dataToPass.awardIDs;
        //WriteJson();
    }

    public void WriteJson()
    {
        string jdata = JsonConvert.SerializeObject(dataToPass);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ConditionData.json",format);
        File.WriteAllText(Application.dataPath + "/Resources/GamePlay/Data_FromGameToEnd.json",jdata); //ID 추가해주기
        AssetDatabase.Refresh();
    }


    GameObject tempObject1, tempObject2, tempObject3, tempObject4, tempObject5, tempObject6, tempObject7;

    public void startEndCredit(string theWinLose)
    {
        var seq = LeanTween.sequence();

        tempObject1 = endCanvas.transform.GetChild(0).transform.gameObject; //backgroundImage
        tempObject2 = endCanvas.transform.GetChild(1).transform.gameObject; //StageImage
        tempObject3 = endCanvas.transform.GetChild(2).transform.gameObject; //Logo_GameWinLose
        tempObject3.GetComponent<Image>().sprite = Resources.Load<Sprite>("GamePlay/GameEnd/Images/Icon_"+theWinLose);

        if(theWinLose == "Win")
        {
            tempObject4 = endCanvas.transform.GetChild(3).transform.gameObject; //RewardBackground_Win
        }
        else if(theWinLose == "Lose")
        {
            tempObject4 = endCanvas.transform.GetChild(4).transform.gameObject; //RewardBackground_Lose
        }

        seq.append(0.3f); //delay everything by 0.5 seconds

        seq.append(  
            LeanTween.value(tempObject1, 0 , 1 , 0.5f).setOnUpdate((float val) =>
            {
                Color c = tempObject1.GetComponent<Image>().color;
                c.a = val;
                tempObject1.GetComponent<Image>().color = c;
            })
        );

        seq.append(  
            LeanTween.value(tempObject2, 0 , 1 , 0.5f).setOnUpdate((float val) =>
            {
                Color c = tempObject2.GetComponent<Image>().color;
                c.a = val;
                tempObject2.GetComponent<Image>().color = c;
            })
        );

        seq.append(0.3f);

        seq.append(
            LeanTween.scale(tempObject3, new Vector3(1,1,1), 0.3f).setEaseInQuint()
        );
        seq.append(0.3f);
        seq.append(
            LeanTween.scale(tempObject4, new Vector3(1,1,1), 0.4f).setEaseInQuint().setOnComplete(showPrompt)
        );
    }

    public void showPrompt()
    {
        var seq = LeanTween.sequence();
        tempObject5 = endCanvas.transform.GetChild(7).transform.gameObject; //Win_ReturnButton
        tempObject6 = endCanvas.transform.GetChild(9).transform.gameObject; //Win_AwardButton
        tempObject7 = endCanvas.transform.GetChild(8).transform.gameObject; //Lose_ConfirmedButton


        if(theWinLose == "Win")
        {
            seq.append(
                LeanTween.scale(tempObject5, new Vector3(1,1,1), 0.3f).setEaseInQuint()
            );
            seq.append(
                LeanTween.scale(tempObject6, new Vector3(1,1,1), 0.3f).setEaseInQuint().setOnComplete(showEarnedValues)
            );
        }
        else if(theWinLose == "Lose")
        {
            seq.append(
                LeanTween.scale(tempObject7, new Vector3(1,1,1), 0.3f).setEaseInQuint().setOnComplete(showEarnedValues)
            );
        }
    }
    public void showEarnedValues()
    {
        tempObject4.transform.GetChild(0).GetChild(0).GetComponent<Text>().DOText(dataToPass.experience.ToString(),1.0f);
        tempObject4.transform.GetChild(0).GetChild(1).GetComponent<Text>().DOText(dataToPass.money.ToString(),1.0f);
        tempObject4.transform.GetChild(0).GetChild(2).GetComponent<Text>().DOText(dataToPass.gem.ToString(),1.0f);

        if(theWinLose == "Win")
        {
            LeanTween.value(tempObject4.transform.GetChild(1).gameObject, 0 , 1f, 0.5f).setOnUpdate((float val) =>
            {
                Color c = tempObject4.transform.GetChild(1).gameObject.GetComponent<Image>().color;
                c.a = val;
                tempObject4.transform.GetChild(1).gameObject.GetComponent<Image>().color = c;
            });
        }
        else if(theWinLose == "Lose")
        {
            //없음
        }
    }


    public void clickBoxOfSecret()
    {
        tempObject5.SetActive(false);
        tempObject6.SetActive(false);
        awardPage_1.SetActive(true);
    }
    public void awardPageThree() //본격 보상 페이지
    {
        awardPage_3.SetActive(true);
        awardPage_1.SetActive(false);
        showAward();
    }

    public GameObject award_1, award_2, award_3, award_4, award_5, award_6;
    public _StageInformation stageInfo;
    public void showAward()
    {
        stageInfo = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<_StageInformation>();
        int stageID = stageInfo.stageID;
        award_1 = awardPage_3.transform.GetChild(1).gameObject;
        award_2 = awardPage_3.transform.GetChild(2).gameObject;
        award_3 = awardPage_3.transform.GetChild(3).gameObject;
        award_4 = awardPage_3.transform.GetChild(4).gameObject;
        award_5 = awardPage_3.transform.GetChild(5).gameObject;
        award_6 = awardPage_3.transform.GetChild(6).gameObject;

        if(stageID == 0)
        {
            //조건 한개, 아이템 한개 를 주자.
            this.transform.GetComponent<CreateAwards>().randomizeAward_Condition(stageID,award_1);
            this.transform.GetComponent<CreateAwards>().randomizeAward_Item(stageID,award_2);
            award_3.SetActive(false);
            award_4.SetActive(false);
            award_5.SetActive(false);
            award_6.SetActive(false);
        }
        else if(stageID == 1)
        {
            this.transform.GetComponent<CreateAwards>().randomizeAward_Condition(stageID,award_1);
            this.transform.GetComponent<CreateAwards>().randomizeAward_Item(stageID,award_2);
            award_3.SetActive(false);
            award_4.SetActive(false);
            award_5.SetActive(false);
            award_6.SetActive(false);
        }
        else if(stageID == 2)
        {
            this.transform.GetComponent<CreateAwards>().randomizeAward_Condition(stageID,award_1);
            this.transform.GetComponent<CreateAwards>().randomizeAward_Item(stageID,award_2);
            this.transform.GetComponent<CreateAwards>().randomizeAward_Item(stageID,award_3);
            award_4.SetActive(false);
            award_5.SetActive(false);
            award_6.SetActive(false);
        }
        //1-1 보상은 1. 액션-앞으로가기 2.조건-적이 0명 이하
        //1-2 보상은 1. 액션-포션사용   2.조건-HP가20%이하  3.아이템-포션
        /*
        if(stageInfo.stageLevel == 1)
        {
            award_1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/Action3");
            award_1.transform.GetChild(1).GetComponent<Text>().text = "액션- 적을 향해 앞으로 이동";
            
            award_2.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/Condition6");
            award_2.transform.GetChild(1).GetComponent<Text>().text = "조건- 적이 0명 이하";
            
            award_3.SetActive(false);
            award_4.SetActive(false);
            award_5.SetActive(false);
            award_6.SetActive(false);
        }
        else if(stageInfo.stageLevel == 2)
        {
            award_1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/Action2");
            award_1.transform.GetChild(1).GetComponent<Text>().text = "액션- 포션 사용";
            
            award_2.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/Condition1");
            award_2.transform.GetChild(1).GetComponent<Text>().text = "조건- HP가 20% 이하";
            
            award_3.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemDB/Potion/0");
            award_3.transform.GetChild(1).GetComponent<Text>().text = "포션 +30";

            award_4.SetActive(false);
            award_5.SetActive(false);
            award_6.SetActive(false);
        }
        */
    }
    
    public void leaveGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
    }
    public void clickBoxOfPage_1()
    {
        if(awardPage_1.GetComponent<Image>().sprite.name == "BG_Closed")
        {
            awardPage_1.GetComponent<Image>().sprite = Resources.Load<Sprite>("GamePlay/GameEnd/Images/BG_Opened");
            Invoke("awardPageThree", 1f);        
        }
    }
}
