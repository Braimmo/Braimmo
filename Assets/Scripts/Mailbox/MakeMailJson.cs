using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;

public class MakeMailJson : MonoBehaviour
{
    int AccountID = 0; // 받아와야된다. ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    public List<MailboxReward> mailboxRewards;

    void Start(){
        Debug.Log("makeMailJson");
        makeMailJson();
    }
  public void makeMailJson(){
        mailboxRewards = new List<MailboxReward>();


        List<JsonEquippableItem> jsonEquippedItem = new List<JsonEquippableItem>();
        //  jsonEquippedItem.Add(new JsonEquippableItem("0_0_0_0", "helmet", 0f, 0f, 70f, 0f, 0f, 0, false, -1));
        //  jsonEquippedItem.Add(new JsonEquippableItem("1_0_0_0", "armor", 100f, 0f, 0f, 0f, 0f, 0, false, -1));
    
        List<Condition_CodeModifier> conditionList = new List<Condition_CodeModifier>();
        conditionList.Add(new Condition_CodeModifier(101, 1035, "HPMT",             "내 체력 20% 이상",             0, 0f, 0f, 888.8f, 20f,"Condition1","Up", -1));
        conditionList.Add(new Condition_CodeModifier(101, 1036, "HPMT",             "내 체력 25% 이상",             0, 0f, 0f, 888.8f, 25f,"Condition1","Up", -1));

        List<Action_CodeModifier> actionList = new List<Action_CodeModifier>();

      for(int i = 0; i<3;i++){
          MailboxReward mailboxReward = new MailboxReward();
          mailboxReward.mail_title = "로그인 보상입니다.";
          mailboxReward.mail_explanation = "2020.08.11(화) 로그인 보상입니다.";
          mailboxReward.experience_Account = 20;
          mailboxReward.experience_Character = 30;
          mailboxReward.money = 500;
          mailboxReward.gem = i;
          mailboxReward.itemList = jsonEquippedItem;
          mailboxReward.conditionList = conditionList;
          mailboxReward.actionList = actionList;       
          mailboxRewards.Add(mailboxReward);   
      }
        string jsonData = JsonConvert.SerializeObject(mailboxRewards);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/mailboxData.json", jsonData);
  }
}
