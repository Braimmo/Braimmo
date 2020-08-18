using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;



public struct _ImageText{
        
        public Sprite sprite;
        public string text;
}

public class RewardSave : MonoBehaviour
{
        

    public Image rewardImageSlot;
    public GameObject mailPanel;
    public MailboxReward mailboxReward;
    public UserInformation userInformation = new UserInformation();
    public List<Condition_CodeModifier> conditionList = new List<Condition_CodeModifier>();
    public List<Action_CodeModifier> actionList = new List<Action_CodeModifier>();
    public List<JsonEquippableItem> itemList = new List<JsonEquippableItem>();
    public List<CharacterInformation_item> characterList = new List<CharacterInformation_item>();
    static RewardSave newMailPanel;

    public int AccountID;
    string JData;
    int reward_count;    
   
    int check_reward_money = 0; int check_reward_gem = 0;
    int check_reward_exp_account = 0;   int check_reward_exp_character = 0;
    int check_reward_item =0;   int check_reward_condition = 0;         int check_reward_action = 0;

    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
    }
    public void onClickMail(){
    MailboxReward mailboxReward = CreateRewardSlot.mailboxRewards[MailListManager.siblingIndex];

    newMailPanel = Instantiate(this, new Vector3(this.transform.position.x, this.transform.position.y,0),Quaternion.identity); 
    newMailPanel.transform.localScale = new Vector3(1,1,1);
    newMailPanel.transform.SetParent(GameObject.Find("Canvas").transform);    
    newMailPanel.transform.GetChild(0).GetComponent<Text>().text = mailboxReward.mail_title;
    newMailPanel.transform.GetChild(1).GetComponent<Text>().text = mailboxReward.mail_explanation;
    
    reward_count = checkRewardCount(mailboxReward);
    Transform rewardImageSlotTransfrom = newMailPanel.transform.GetChild(2);
    Image[] newRewardImageSlot = new Image[reward_count];

    for(int i = 0; i<reward_count;i++){
            newRewardImageSlot[i] = Instantiate(rewardImageSlot,new Vector3(rewardImageSlotTransfrom.position.x,rewardImageSlotTransfrom.position.y,0),Quaternion.identity);
            newRewardImageSlot[i].transform.SetParent(newMailPanel.transform.GetChild(2).GetChild(0));
                _ImageText imageText = new _ImageText();
                imageText = getImageText(mailboxReward);
            newRewardImageSlot[i].transform.GetChild(0).GetComponent<Image>().sprite = imageText.sprite;
            newRewardImageSlot[i].transform.GetChild(0).GetComponent<Image>().color = new Color32(255,255,255,255);
            newRewardImageSlot[i].transform.GetChild(1).GetComponent<Text>().text = imageText.text;
    }    
    }
      _ImageText getImageText(MailboxReward mailboxReward){
            _ImageText imageText = new _ImageText();
            
            if(check_reward_money ==1){
                    imageText.sprite = Resources.Load<Sprite>("HomeScene/MoneyAsset");
                    imageText.text = mailboxReward.money.ToString();
                    check_reward_money = 0;
            }
            else if(check_reward_gem ==1){
                    imageText.sprite = Resources.Load<Sprite>("HomeScene/GemAsset");
                    imageText.text = mailboxReward.gem.ToString();
                    check_reward_gem = 0;
            }
            else if(check_reward_exp_account == 1){
                    imageText.sprite = Resources.Load<Sprite>("CharacterDB/0");
                    imageText.text = mailboxReward.experience_Account.ToString();
                    check_reward_exp_account = 0;
            }
             else if(check_reward_exp_character == 1){
                    imageText.sprite = Resources.Load<Sprite>("CharacterDB/0");
                    imageText.text = mailboxReward.experience_Character.ToString();
                    check_reward_exp_character = 0;
            }
              else if(check_reward_item != 0){
                imageText.sprite = Resources.Load<Sprite>("CharacterDB/0");
                    imageText.text = mailboxReward.itemList[check_reward_item-1].itemName;
                    check_reward_item--;
              }

              else if(check_reward_condition != 0){
                      imageText.sprite = Resources.Load<Sprite>("CodeEditor/"+mailboxReward.conditionList[check_reward_condition-1].conditionPrefab);
                      imageText.text =  mailboxReward.conditionList[check_reward_condition-1].conditionName + 
                                        mailboxReward.conditionList[check_reward_condition-1].conditionValue;
                        check_reward_condition --;
              }


            return imageText;
            

    }
    int checkRewardCount(MailboxReward mailboxReward){
        int count = 0 ;
        if(mailboxReward.money != 0)
           check_reward_money++;
        
        if(mailboxReward.gem != 0)
            check_reward_gem++;
        if(mailboxReward.experience_Account !=0)
            check_reward_exp_account++;
        if(mailboxReward.experience_Character != 0)
            check_reward_exp_character++;

        check_reward_action = mailboxReward.actionList.Count;
        check_reward_condition = mailboxReward.conditionList.Count;
        check_reward_item = mailboxReward.itemList.Count;

        count += check_reward_money + check_reward_gem   +   check_reward_exp_account   
        +  check_reward_exp_character  + check_reward_action  + check_reward_condition  + check_reward_item;
           Debug.Log("Reward_count = "+count);
            return count;
    } 


    public void onClickMailReceive(){               
    MailboxReward mailboxReward = CreateRewardSlot.mailboxRewards[MailListManager.siblingIndex];
        string jsonData;
        //userinfo 받아오기            
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        userInformation = JsonConvert.DeserializeObject<UserInformation>(JData);
        //condition 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ConditionData.json");
        conditionList = JsonConvert.DeserializeObject<List<Condition_CodeModifier>>(JData);
        //action 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/Alice/ActionData.json");
        actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(JData);
        //Item 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json");
        itemList = JsonConvert.DeserializeObject<List<JsonEquippableItem>>(JData);
        //character 모두 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json");
        characterList = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(JData);

        userInformation.experience += mailboxReward.experience_Account;
        userInformation.gem += mailboxReward.gem;
        userInformation.money += mailboxReward.money;

        jsonData = JsonConvert.SerializeObject(userInformation);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json", jsonData);
//userInformation 저장 완료.

        foreach(Condition_CodeModifier condition in mailboxReward.conditionList){
           conditionList.Add(condition); 
        }        
        jsonData = JsonConvert.SerializeObject(conditionList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ConditionData.json", jsonData);
//conditionData 저장 완료. 

        foreach(Action_CodeModifier action in mailboxReward.actionList){
           actionList.Add(action); 
        }
        jsonData = JsonConvert.SerializeObject(actionList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/Alice/ActionData.json", jsonData);
//actionData 저장 완료. 

        foreach(JsonEquippableItem item in mailboxReward.itemList){
           itemList.Add(item); 
        }        
        jsonData = JsonConvert.SerializeObject(itemList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json", jsonData);
    
//item 저장 완료

        foreach(CharacterInformation_item character in characterList){
            character.experience += mailboxReward.experience_Character;
        }    

        jsonData = JsonConvert.SerializeObject(characterList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json", jsonData);
//character별 경험치 저장 완료. 
      
        List<MailboxReward> mailboxRewards = new List<MailboxReward>();       
        Debug.Log("CreateRewardSlot.mailboxRewards.Count = " + CreateRewardSlot.mailboxRewards.Count); 
        Debug.Log("MailListManager.siblingIndex = " + MailListManager.siblingIndex); 

        for(int i =0; i<CreateRewardSlot.mailboxRewards.Count;i++){
                if(i==MailListManager.siblingIndex)
                continue;
                else{
                        mailboxRewards.Add(CreateRewardSlot.mailboxRewards[i]);
                }
        }
        CreateRewardSlot.mailboxRewards = mailboxRewards;        
        jsonData = JsonConvert.SerializeObject(mailboxRewards);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/mailboxData.json", jsonData);
        Destroy(CreateRewardSlot.newRewardSlot[MailListManager.siblingIndex]);
        newMailPanel.gameObject.SetActive(false);
    
}



    public void onClickReceiveWholeRewards(){
        
        string jsonData;
        //userinfo 받아오기            
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        userInformation = JsonConvert.DeserializeObject<UserInformation>(JData);
        //condition 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ConditionData.json");
        conditionList = JsonConvert.DeserializeObject<List<Condition_CodeModifier>>(JData);
        //action 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/Alice/ActionData.json");
        actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(JData);
        //Item 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json");
        itemList = JsonConvert.DeserializeObject<List<JsonEquippableItem>>(JData);
        //character 모두 받아오기
        JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json");
        characterList = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(JData);


            for(int i = 0; i<CreateRewardSlot.mailboxRewards.Count;i++){               
            MailboxReward mailboxReward = CreateRewardSlot.mailboxRewards[i];
        
        userInformation.experience += mailboxReward.experience_Account;
        userInformation.gem += mailboxReward.gem;
        userInformation.money += mailboxReward.money;

//userInformation 저장 완료.
        foreach(Condition_CodeModifier condition in mailboxReward.conditionList){
           conditionList.Add(condition); 
        }        
     //conditionData 저장 완료. 
        foreach(Action_CodeModifier action in mailboxReward.actionList){
           actionList.Add(action); 
        }
      //actionData 저장 완료. 
        foreach(JsonEquippableItem item in mailboxReward.itemList){
           itemList.Add(item); 
        }         
//item 저장 완료
        foreach(CharacterInformation_item character in characterList){
            character.experience += mailboxReward.experience_Character;
        }    
        Destroy(CreateRewardSlot.newRewardSlot[i]);

    //character별 경험치 저장 완료. 
}

        jsonData = JsonConvert.SerializeObject(userInformation);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json", jsonData);

        jsonData = JsonConvert.SerializeObject(conditionList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ConditionData.json", jsonData);

        jsonData = JsonConvert.SerializeObject(actionList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/Alice/ActionData.json", jsonData);

        jsonData = JsonConvert.SerializeObject(itemList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json", jsonData);
  
        jsonData = JsonConvert.SerializeObject(characterList);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json", jsonData);

        List<MailboxReward> mailboxRewards = new List<MailboxReward>();

        jsonData = JsonConvert.SerializeObject(mailboxRewards);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/mailboxData.json", jsonData);
    }
    }


