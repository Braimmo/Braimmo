using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowEachStat : MonoBehaviour
{
    public GameObject theCharacter;
    private EachCharacterStat theCharacterStat;
    public TextMeshProUGUI theInfo;
    public string theInput;
    public GameObject image_condition;
    public GameObject image_action;
    public GameObject[] image_condition_each;

    public GameObject statBox;
    
    void Awake()
    {
        image_condition_each = new GameObject[9];
        for(int i = 0; i < 9; i ++)
        {
            image_condition_each[i] = image_condition.transform.GetChild(i).gameObject;
        }

        image_action.SetActive(false);
        image_condition.SetActive(false);
    }
    void LateUpdate()
    {
        try
        {
            if(GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera.transform.name != "MainCamera")
            {
                try
                {
                    theCharacter = GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera.transform.GetComponent<Camera_Each>().target;
                    theCharacterStat = theCharacter.transform.GetChild(1).GetComponent<EachCharacterStat>();

                    float tempDist = (Mathf.Round(theCharacterStat.aimedDistance*100)*0.01f);

                    theInput =              "\t"+ theCharacterStat.name +"\n";
                    theInput = theInput +   "HP\t "+ theCharacterStat.health + " / " + theCharacterStat.maxHealth +"\n";
                    theInput = theInput +   "방어력\t "+ theCharacterStat.defense+"\n";
                    theInput = theInput +   "DMG\t "+ theCharacterStat.damage+"\n";
                    theInput = theInput +   "무기 거리\t "+ theCharacterStat.weaponRange+"\n";
                    theInput = theInput +   "타겟 거리\t "+ tempDist.ToString() +"\n";
                    theInput = theInput +   "포션 수\t  "+ theCharacterStat.potionCount.ToString() + "\n";

                    theInfo.text = theInput;

                    if(GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera.GetComponent<Camera_Each>().target.transform.GetComponent<CharacterMovement>().actionImage != "")
                    {
                        //image_action.SetActive(true);
                        //image_condition.SetActive(true);
                        
                        image_action.GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/"+GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera.GetComponent<Camera_Each>().target.transform.GetComponent<CharacterMovement>().actionImage);
                        for(int i = 0; i < 9; i++)
                        {//일단 리셋
                            image_condition_each[i].SetActive(false);
                        }
                        for(int i = 0; i < GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera.GetComponent<Camera_Each>().target.transform.GetComponent<CharacterMovement>().conditionImages.Length; i++)
                        {
                            image_condition_each[i].SetActive(true);
                            image_condition_each[i].GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/"+GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera.GetComponent<Camera_Each>().target.transform.GetComponent<CharacterMovement>().conditionImages[i]);
                            image_condition_each[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera.GetComponent<Camera_Each>().target.transform.GetComponent<CharacterMovement>().conditionValues[i];
                            image_condition_each[i].transform.GetChild(1).GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/Arrow_"+GameObject.Find("GamePlayManager").transform.GetComponent<CameraWalk>().enabledCamera.GetComponent<Camera_Each>().target.transform.GetComponent<CharacterMovement>().conditionStates[i]);
                        }

                    }
                }
                catch (System.Exception)
                {
                }
            }
            else
            {
                image_action.SetActive(false);
                image_condition.SetActive(false);
                statBox.GetComponent<RectTransform>().sizeDelta = new Vector2(1000,1200);
                theInput = "내 캐릭터 수\t " + GameObject.Find("GamePlayManager").GetComponent<GameEndCheck>().players.Length.ToString() + "\n";
                theInput = theInput +   "내 적 수\t " + GameObject.Find("GamePlayManager").GetComponent<GameEndCheck>().enemies.Length.ToString() + "\n";

                theInfo.text = theInput;
            }
        }
        catch (System.Exception)
        {
            
        }
    }

    public void resizeStat()
    {
        if(statBox.GetComponent<RectTransform>().rect.height > 1500)
        {
            statBox.GetComponent<RectTransform>().sizeDelta = new Vector2(1000,1200);
            image_action.SetActive(false);
            image_condition.SetActive(false);
        }
        else
        {
            statBox.GetComponent<RectTransform>().sizeDelta = new Vector2(1000,2500);
            image_action.SetActive(true);
            image_condition.SetActive(true);
        }
    }
}
