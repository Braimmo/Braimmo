using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;

public class TargetOrder : MonoBehaviour
{
    public GameObject ClosestCheck;     public GameObject FarthestCheck;
    public GameObject ForBoss;          public GameObject ForMiniBoss;
    public GameObject ForMelee;         public GameObject ForRange;
    public GameObject ForTank;
    public GameObject[] ForOrder;
    public TargetOrderInformation theTargetOrderInformation;
    public string closeFar = "close";
    public int AccountID;
    public string passedID;
    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        passedID = GameObject.Find("charID_DontDestroy").GetComponent<fromCharToCode>().name;
        ForOrder = new GameObject[5];
        ForOrder[0] = ForBoss;
        ForOrder[1] = ForMiniBoss;
        ForOrder[2] = ForMelee;
        ForOrder[3] = ForRange;
        ForOrder[4] = ForTank;

        TextAsset JData;

        //만약 codeeditor_IDpasser에서 온 경우일 경우 이걸 해야된당...
        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면?
            JData = Resources.Load(theTempID.theID + passedID + "/TargetOrder") as TextAsset;
        }
        else
        {//만약 이게 없으면?
            JData = Resources.Load("Json_AccountInfo/"+ AccountID.ToString() + "/" + passedID + "/TargetOrder") as TextAsset; //끝
        }
        string jdata = JData.text;


        theTargetOrderInformation = JsonConvert.DeserializeObject<TargetOrderInformation>(jdata);
        loadOnScene();
    }

    public void loadOnScene()
    {
        if(theTargetOrderInformation.closeFar == "close")
            ClickClosest();
        else if(theTargetOrderInformation.closeFar == "far")
            ClickFarthest();

        for(int i = 0; i < 5; i++)
        {
            ForOrder[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = theTargetOrderInformation.aimOrderArray[i].ToString();
        }
    }
    public void ClickClosest()
    {
        closeFar = "close";
        Sequence ClickSequence = DOTween.Sequence();
        ClickSequence.Append(ClosestCheck.transform.DOScale(new Vector2(0.6f,0.6f),0.2f));
        ClickSequence.Append(ClosestCheck.transform.DOScale(new Vector2(1,1), 0.1f));

        ClosestCheck.GetComponent<Image>().DOColor(new Color(1,1,1,1f),0.2f).SetOptions(true);
        FarthestCheck.GetComponent<Image>().DOColor(new Color(1,1,1,0.5f),0.2f).SetOptions(true);
    }
    public void ClickFarthest()
    {
        closeFar = "far";
        Sequence ClickSequence = DOTween.Sequence();
        ClickSequence.Append(FarthestCheck.transform.DOScale(new Vector2(0.6f,0.6f),0.2f));
        ClickSequence.Append(FarthestCheck.transform.DOScale(new Vector2(1,1), 0.1f));
        
        ClosestCheck.GetComponent<Image>().DOColor(new Color(1,1,1,0.5f),0.2f).SetOptions(true);
        FarthestCheck.GetComponent<Image>().DOColor(new Color(1,1,1,1f),0.2f).SetOptions(true);
    }

    public void ClickForOrder(GameObject tempObject)
    {
        string tempOrder = tempObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        if(tempOrder == "")
        {
            AddOrder(tempObject);
        }
        else
        {
            DeleteOrder(tempObject,tempOrder);
        }
    }

    public void DeleteOrder(GameObject tempObject, string tempOrder)
    {
        int tempOrderInt = int.Parse(tempOrder);
        for(int i = 0; i < ForOrder.Length; i++)
        {
            int otherOrder = 0;
            if(ForOrder[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text != "")
                otherOrder = int.Parse(ForOrder[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
            if(otherOrder >= tempOrderInt)
            {
                ForOrder[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
        }
    }

    public void AddOrder(GameObject tempObject)
    {
        int largestOrder = 0;
        if(ForOrder[0].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text != "")
            largestOrder = int.Parse(ForOrder[0].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
        for(int i = 0; i < ForOrder.Length - 1; i++)
        {
            int secondOrder = 0;
            if(ForOrder[i+1].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text != "")
                secondOrder = int.Parse(ForOrder[i+1].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
            if(largestOrder < secondOrder)
                largestOrder = secondOrder;
        }
        tempObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (1+largestOrder).ToString();
    }

    public void saveTargetOrder()
    {
        theTargetOrderInformation.closeFar = closeFar;
        int[] tempArray;
        int temp1, temp2, temp3, temp4, temp5; //순서 숫자
        temp1 = int.Parse(ForOrder[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        temp2 = int.Parse(ForOrder[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        temp3 = int.Parse(ForOrder[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        temp4 = int.Parse(ForOrder[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        temp5 = int.Parse(ForOrder[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        tempArray = new int[] {temp1, temp2, temp3, temp4, temp5};
        theTargetOrderInformation.aimOrderArray = tempArray;

        string jdata = JsonConvert.SerializeObject(theTargetOrderInformation);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ConditionData.json",format);

        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면? temp에서 넘어온 아이다.
            File.WriteAllText(Application.dataPath + "/Resources/" + theTempID.theID + passedID + "/TargetOrder.json",jdata);
        }
        else
        {//만약 이게 없으면?
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ AccountID.ToString() + "/" + passedID + "/TargetOrder.json",jdata); //ID 추가해주기
        }
        Debug.Log("TargetOrder is saved");
        AssetDatabase.Refresh();

    }
}

public class TargetOrderInformation
{
    public string closeFar;
    public int[] aimOrderArray;
    
    public TargetOrderInformation(string closeFar, int[] aimOrderArray)
    {
        this.closeFar = closeFar;
        this.aimOrderArray = aimOrderArray;
    }    
    
    public TargetOrderInformation()
    {
        this.closeFar = "close";
        this.aimOrderArray = new int[] {4,3,0,2,1};
    }
}