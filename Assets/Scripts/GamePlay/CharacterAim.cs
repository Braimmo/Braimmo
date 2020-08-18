using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CharacterAim : MonoBehaviour
{
    public string aimChoice;        public Vector3 myPosition;
    public bool haveAim;            public GameObject thePrefab;
    public GameObject theArrowPrefab;
    public GameObject theArrow;
    public float theArrowYPosition;
    public TargetOrderInformation theTargetOrderInformation;
    public int AccountID = 0; //계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ

    void Awake()
    {        
        myPosition = this.transform.parent.transform.position;
        haveAim = false;
        theArrowYPosition = 5.0f;
    }
    void Start()
    {
        print(this.transform.parent.GetChild(1).GetComponent<EachCharacterStat>().name);

        //적이면 Resources/Json_GameInfo에서 로드해야되고 아군이면 Resources/Json_AccountInfo에서 로드해야된다!
        string tempLocation = "";
        if(this.transform.parent.tag == "Enemy")
        {
            tempLocation = "Json_GameInfo/";
        }
        else if(this.transform.parent.tag == "Player")
        {
            tempLocation = "Json_AccountInfo/" + AccountID.ToString() + "/";
        }
        print(tempLocation + this.transform.parent.GetChild(1).GetComponent<EachCharacterStat>().name + "/TargetOrder");
        TextAsset JData = Resources.Load(tempLocation + this.transform.parent.GetChild(1).GetComponent<EachCharacterStat>().name + "/TargetOrder") as TextAsset;
        string jdata = JData.text;
        theTargetOrderInformation = JsonConvert.DeserializeObject<TargetOrderInformation>(jdata);
        
        aimChoice = theTargetOrderInformation.closeFar;
        //theTargetOrderInformation.aimOrderArray 에 대해서도 해보기


        calculateRelativeDistance();
        decideTargetByDistance();
        createArrow();
        theArrow.transform.GetComponent<MeshRenderer>().enabled = false;
        theArrow.transform.RotateAround (transform.position, transform.right, 180f);
        LeanTween.moveY(theArrow, theArrow.transform.position.y - 0.5f, 0.5f).setEaseInOutCubic().setLoopPingPong();
        
    }
    void FixedUpdate()
    {
        if(GameObject.Find("GamePlayManager").GetComponent<GameEndCheck>().endCheck != true && GameObject.Find("GamePlayManager").GetComponent<GameStartCheck>().pauseCheck != true)
        {
            if(aimChoice == "close") //aim from CLOSEST
            {
                if(decidedTarget[0].transform.GetChild(1).tag == "Dead")
                {
                    //내가 보고 있던 캐릭터가 뒤졌다.
                    calculateRelativeDistance();
                    decideTargetByDistance();
                    aim("close");
                }
                else
                {
                    aim("close");
                    if(GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera.transform.name != "MainCamera")
                    {
                        if(GameObject.Find("GamePlayManager").GetComponent<CameraWalk>().enabledCamera.transform.GetComponent<Camera_Each>().target == this.transform.parent.gameObject)
                        {
                            //print("Camera target and I am the same");
                            theArrow.transform.GetComponent<MeshRenderer>().enabled = true;
                            theArrow.transform.position = new Vector3(decidedTarget[0].transform.position.x,decidedTarget[0].transform.position.y + theArrowYPosition,decidedTarget[0].transform.position.z);
                        }
                        else
                        {
                            theArrow.transform.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                    else
                    {
                            theArrow.transform.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
            else if(aimChoice == "far")//aim from FARTHEST
            {
                if(decidedTarget[1].transform.GetChild(1).tag == "Dead")
                {
                    //내가 보고 있던 캐릭터가 뒤졌다.
                    calculateRelativeDistance();
                    decideTargetByDistance();
                    aim("far");
                }
                else
                {
                    aim("far");
                }
            }
            thePrefab.transform.GetChild(1).GetComponent<EachCharacterStat>().aimedDistance = Vector3.Distance(decidedTarget[0].transform.position, this.transform.position);

            if(targets.Length == 0)
            {
                theArrow.transform.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            theArrow.transform.GetComponent<MeshRenderer>().enabled = false;
        }
        calculateRelativeDistance();
    }
    public GameObject[] targets;
    public float[] targetsDistance;
    public GameObject[] decidedTarget;

    public void createArrow()
    {
        theArrow = Instantiate(theArrowPrefab, transform.position, Quaternion.identity);
        theArrow.transform.position = new Vector3(this.transform.parent.gameObject.transform.position.x,this.transform.parent.gameObject.transform.position.y-theArrowYPosition,this.transform.parent.gameObject.transform.position.z);
    }
    public void calculateRelativeDistance()
    {
        myPosition = this.transform.parent.transform.position;
        GameObject[] tempGOArray; int j = 0;
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        if(this.transform.parent.tag == "Enemy")
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            tempGOArray = new GameObject[targets.Length];
            //print(targets.Length);
            for(int i = 0; i < targets.Length; i++)
            {
                if(targets[i].transform.GetChild(1).tag == "Alive") //총 Player 중에서 살아있는 놈만 걸러보자.
                {
                    tempGOArray[j] = targets[i]; //골라낸 놈만 일단 원래 있던 tempGo에다가 저장해주고 5개 중에 4개 나오겠지?
                    j++; //골라내면 +1 해주기.
                }
            }
            targets = new GameObject[j]; //타겟은 그럼 4개인 새로운걸로 바꿔주고
            for(int i = 0; i < j; i++)
            {
                targets[i] = tempGOArray[i]; //타겟을 템프의 4개만
            }
        }
        else if(this.transform.parent.tag == "Player")
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");
            tempGOArray = new GameObject[targets.Length];
            for(int i = 0; i < targets.Length; i++)
            {
                if(targets[i].transform.GetChild(1).tag == "Alive") //총 Player 중에서 살아있는 놈만 걸러보자.
                {
                    tempGOArray[j] = targets[i]; //골라낸 놈만 일단 원래 있던 tempGo에다가 저장해주고 5개 중에 4개 나오겠지?
                    j++; //골라내면 +1 해주기.
                }
            }
            targets = new GameObject[j]; //타겟은 그럼 4개인 새로운걸로 바꿔주고
            for(int i = 0; i < j; i++)
            {
                targets[i] = tempGOArray[i]; //타겟을 템프의 4개만
            }
        }
        targetsDistance = new float[targets.Length];

        int numInRange = 0;
        for(int i = 0; i < targets.Length; i++)
        {
            targetsDistance[i] = Vector3.Distance(targets[i].transform.position, myPosition);
            //print(targets[i].name + " and its position distance is " + targetsDistance[i]);
            if(targetsDistance[i] < thePrefab.transform.GetChild(1).GetComponent<EachCharacterStat>().weaponRange)
            {
                numInRange++;
            }
        }
        thePrefab.transform.GetChild(1).GetComponent<EachCharacterStat>().enemyNumInRange = numInRange;
    }

    public void decideTargetByDistance()
    {
        decidedTarget = new GameObject[2];
        float minDistance = 999, maxDistance = 0;
        int minIndex = 0, maxIndex = 0;
        decidedTarget[0] = this.transform.parent.gameObject; //decidedTarget[0] is MIN
        decidedTarget[1] = this.transform.parent.gameObject; //decidedTarget[0] is MAX
        for(int i = 0; i < targets.Length; i++)
        {
            if(minDistance > targetsDistance[i])
            {
                minDistance = targetsDistance[i];
                minIndex = i;
                decidedTarget[0] = targets[i];
                
            }
            
            if(maxDistance < targetsDistance[i])
            {
                maxDistance = targetsDistance[i];
                maxIndex = i;
                decidedTarget[1] = targets[i];
            }
        }
        haveAim = true;

    }

    public void aim(string aimChoice)
    {
        myPosition = this.transform.parent.transform.position;
        Vector3 enemyPosition;
        if(aimChoice == "close") //aim from CLOSEST
        {
            enemyPosition = decidedTarget[0].transform.position;
            Vector3 relativePos = new Vector3(enemyPosition.x - myPosition.x, 0, enemyPosition.z - myPosition.z);
            Quaternion destinedRotation = Quaternion.LookRotation(relativePos);
            this.transform.parent.transform.rotation = Quaternion.Slerp(this.transform.parent.transform.rotation, destinedRotation,3.0f * Time.deltaTime);
        }
        else //aim from FARTHEST
        {
            //decidedTarget[1]
        }
    }
}