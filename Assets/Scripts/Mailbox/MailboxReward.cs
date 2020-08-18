using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxReward
{
    public string mail_title;
    public string mail_explanation;
    public float experience_Account;   public float experience_Character;
    public float money; public int gem;     
    public List<JsonEquippableItem> itemList;
    //아이템
    public List<Condition_CodeModifier> conditionList;
    //조건
    public List<Action_CodeModifier> actionList;
    //액션

    public MailboxReward(){
        this.mail_title = "";
        this.mail_explanation = "";
        this.experience_Account = 0;
        this.experience_Character = 0;
        this.gem = 0;
        this.money = 0;
        this.itemList = new List<JsonEquippableItem>();
        this.conditionList = new List<Condition_CodeModifier>();
        this.actionList = new List<Action_CodeModifier>();
    }
    public MailboxReward(string mail_title, string mail_explanation,float experience_Account,float experience_Character,float money, int gem, List<JsonEquippableItem> itemList,List<Condition_CodeModifier> conditionList, List<Action_CodeModifier> actionList){
        this.mail_title = mail_title;
        this.mail_explanation = mail_explanation;
        this.experience_Account = experience_Account;
        this.experience_Character = experience_Character;
        this.money = money;
        this.gem = gem;
        this.itemList = itemList;
        this.conditionList = conditionList;
        this.actionList = actionList;
    }

}
