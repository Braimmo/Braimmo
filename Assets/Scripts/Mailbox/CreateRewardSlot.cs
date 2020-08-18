using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;


public class CreateRewardSlot : MonoBehaviour
{ 
    public GameObject reward_Slot;
    public Text Title_text;
    public static List<MailboxReward> mailboxRewards = new List<MailboxReward>();
    int AccountID = 0;//임시 값 ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    int reward_count; 
    public static GameObject[] newRewardSlot;
    
    public void Start()
    {
        string JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/mailboxData.json");
        mailboxRewards = JsonConvert.DeserializeObject<List<MailboxReward>>(JData);
        reward_count = mailboxRewards.Count;
        newRewardSlot = new GameObject[reward_count];
        for(int i=0; i<reward_count;i++){
            Title_text.text = mailboxRewards[i].mail_title;       
            newRewardSlot[i] = Instantiate(reward_Slot, new Vector3(transform.position.x, transform.position.y,0),Quaternion.identity); 
            newRewardSlot[i].transform.SetParent(transform);
            newRewardSlot[i].transform.localScale = new Vector3(1,1,1);
        }
    }

    
}
