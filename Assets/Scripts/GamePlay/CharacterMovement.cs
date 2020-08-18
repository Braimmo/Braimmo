using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed;
    UseWeapon weapon;
    public Image healthBarImage;
    public float shootCooldown;
    public float shootCooldownReset;
    Vector3 movement = new Vector3(0,0,0);

    public Rigidbody rb;
    public Animator anim;
    public MakeAlgorithmForGame_1 checkCondition;
    public string actionImage = "";
    public string[] conditionImages = {""};
    public string[] conditionValues = {""};
    public string[] conditionStates = {""};
    public GameObject heartEffectPrefab;
    public bool isHit;
    float randomNumGeneratingTime; int theRandomNum;
    void Awake()
    {
        anim = this.transform.GetComponent<Animator>();
        movementSpeed = this.transform.GetChild(1).GetComponent<EachCharacterStat>().speed;
        weapon = this.transform.GetChild(0).GetComponent<UseWeapon>();
        checkCondition = this.transform.GetComponent<MakeAlgorithmForGame_1>();
        healthBarImage = this.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
        rb = this.GetComponent<Rigidbody>();
        shootCooldown = 0.5f;
        shootCooldownReset = 1.0f;
        isHit = false;
        randomNumGeneratingTime = 0f;
    }
    void FixedUpdate()
    { 
        if(GameObject.Find("GamePlayManager").GetComponent<GameEndCheck>().endCheck != true && GameObject.Find("GamePlayManager").GetComponent<GameStartCheck>().pauseCheck != true)
        {
            anim.enabled = true;
            movementSpeed = this.transform.GetChild(1).GetComponent<EachCharacterStat>().speed * 2 / 3;
            float temp1 = this.transform.GetChild(1).transform.GetComponent<EachCharacterStat>().aimedDistance;
            float temp2 = this.transform.GetChild(1).transform.GetComponent<EachCharacterStat>().weaponRange;

            //총 쏘는게 제일 먼저 온다.
            //그 다음에 shoot Cooldown Reset이 되면서 총을 쏘기 때문에 movement를 못하게 되는 원리이다.
            //그런데 물약은 총을 쏘든 뭘 하든 무조건 마셔야 하기 때문에 shootCooldown이 없다. 그 대신 마실 때 무빙이나 총질을 못하게 해야 하니까 shootCooldown을 reset해줘야한다.
            //내가 맞을 때 보여주는 모션은 쿨다운도 아니고 무빙할때도 보여줘야 하기 때문에 
            if(checkCondition.CheckCondition_(11) && shootCooldown <= 0f && randomNumGeneratingTime <= 0  && this.transform.GetChild(0).GetComponent<CharacterAim>().targets.Length > 0)
            {
                //특수공격임. 근데 지금 뭐 딱히 없어서 포션 먹는 모션으로 대체하겠음.
                generateRandomNum();
                if(theRandomNum > 9) //9는 절대 일어날 일 없음.
                {
                    useSpecial();
                    shootCooldown = 0.5f;
                }
                randomNumGeneratingTime = 1.0f;
            }
            if (((checkCondition.CheckCondition_(1) && shootCooldown <= 0f)) && (temp1 < temp2)  && this.transform.GetChild(0).GetComponent<CharacterAim>().targets.Length > 0)
            {
                actionImage = "Action1";
                conditionImages = this.transform.GetComponent<CheckConditionData>().conditionImageList;
                conditionValues = this.transform.GetComponent<CheckConditionData>().conditionValueList;
                conditionStates = this.transform.GetComponent<CheckConditionData>().conditionStateList;

                print(this.transform.name + " is shooting.");
                //check if the character's in range to shoot
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Speed", -1f);
                anim.SetTrigger("Shoots");
                StartCoroutine(weapon.Shoots());
                shootCooldown = shootCooldownReset;
            }
            else if (shootCooldown <= 0f  && this.transform.GetChild(0).GetComponent<CharacterAim>().targets.Length > 0) //this is for movement 여기는 총을 쏘면 1초가 다시 되면서 그 동안 움직이지 못하게 하기 위함이다.
            {
                if (checkCondition.CheckCondition_(3)) //move forward
                {
                    actionImage = "Action3";
                    conditionImages = this.transform.GetComponent<CheckConditionData>().conditionImageList;
                    conditionValues = this.transform.GetComponent<CheckConditionData>().conditionValueList;
                    conditionStates = this.transform.GetComponent<CheckConditionData>().conditionStateList;
                    movement.z = 1;
                }
                else if (checkCondition.CheckCondition_(4)) //move backward
                {
                    actionImage = "Action4";
                    conditionImages = this.transform.GetComponent<CheckConditionData>().conditionImageList;
                    conditionValues = this.transform.GetComponent<CheckConditionData>().conditionValueList;
                    conditionStates = this.transform.GetComponent<CheckConditionData>().conditionStateList;
                    movement.z = -1;
                }
                else
                {
                    movement.z = 0;
                }

                if (checkCondition.CheckCondition_(5)) //move left
                {
                    actionImage = "Action5";
                    conditionImages = this.transform.GetComponent<CheckConditionData>().conditionImageList;
                    conditionValues = this.transform.GetComponent<CheckConditionData>().conditionValueList;
                    conditionStates = this.transform.GetComponent<CheckConditionData>().conditionStateList;
                    movement.x = -1;
                }
                else if (checkCondition.CheckCondition_(6)) //move right
                {
                    actionImage = "Action6";
                    conditionImages = this.transform.GetComponent<CheckConditionData>().conditionImageList;
                    conditionValues = this.transform.GetComponent<CheckConditionData>().conditionValueList;
                    conditionStates = this.transform.GetComponent<CheckConditionData>().conditionStateList;
                    movement.x = 1;
                }
                else
                {
                    movement.x = 0;
                }
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.z);
                anim.SetFloat("Speed", movement.sqrMagnitude);

                transform.position += transform.TransformDirection(movement) * movementSpeed * Time.fixedDeltaTime;
            }
            else
            {
                actionImage = "";
            }

            if (checkCondition.CheckCondition_(2)  && this.transform.GetChild(0).GetComponent<CharacterAim>().targets.Length > 0)
            {   //포션 사용
                if(this.transform.GetChild(1).GetComponent<EachCharacterStat>().potionCount > 0)
                {
                    drinkPotion();
                }
            }
            if(isHit == true)
            {//쳐맞는 모션
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Speed", -1f);
                anim.SetTrigger("isBeingHit");
                print("is being hit!");
                isHit = false;
            }
            //그래도 시간은 흐른다.
            shootCooldown -= Time.deltaTime;
            randomNumGeneratingTime -= Time.deltaTime;
        }
        else
        {
            anim.enabled = false;
        }
    }
    public void dieMotion()
    {
        //뒤지는모션
        anim.SetBool("Dead",true);
        anim.SetTrigger("Dies");
    }
    bool dancing = false;
    public void danceMotion()
    {
        if(dancing == false)
        {
            anim.SetTrigger("Dance");
            dancing = true;
        }
    }

    public void drinkPotion()
    {
        //포션 쳐먹자.
        actionImage = "Action2";
        conditionImages = this.transform.GetComponent<CheckConditionData>().conditionImageList;
        conditionValues = this.transform.GetComponent<CheckConditionData>().conditionValueList;
        conditionStates = this.transform.GetComponent<CheckConditionData>().conditionStateList;

        this.transform.GetChild(1).GetComponent<EachCharacterStat>().potionCount--;

        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        anim.SetFloat("Speed", -1f);
        anim.SetTrigger("drinks");

        print("potion used!");
        float potionAmount = 30.0f;
        float currentHealth = this.transform.GetChild(1).GetComponent<EachCharacterStat>().health;
        float maxHealth = this.transform.GetChild(1).GetComponent<EachCharacterStat>().maxHealth;
        currentHealth = currentHealth + potionAmount;

        if(currentHealth > maxHealth)
            currentHealth = maxHealth;

        this.transform.GetChild(1).GetComponent<EachCharacterStat>().health = currentHealth;

        float healthPercent = currentHealth / maxHealth;
        DOTween.To(()=> healthBarImage.fillAmount, x=> healthBarImage.fillAmount = x, healthPercent, 1f);

        GameObject theHeartEffect = Instantiate(heartEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(theHeartEffect, 1f);

        shootCooldown = shootCooldownReset;
    }

    public void useSpecial()
    {
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        anim.SetFloat("Speed", -1f);
        anim.SetTrigger("drinks");
        print("Special Action!!");
        float potionAmount = 100.0f;
        float currentHealth = this.transform.GetChild(1).GetComponent<EachCharacterStat>().health;
        float maxHealth = this.transform.GetChild(1).GetComponent<EachCharacterStat>().maxHealth;
        currentHealth = currentHealth + potionAmount;

        if(currentHealth > maxHealth)
            currentHealth = maxHealth;

        this.transform.GetChild(1).GetComponent<EachCharacterStat>().health = currentHealth;

        float healthPercent = currentHealth / maxHealth;
        DOTween.To(()=> healthBarImage.fillAmount, x=> healthBarImage.fillAmount = x, healthPercent, 1f);

        GameObject theHeartEffect = Instantiate(heartEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(theHeartEffect, 1f);
    }

    void generateRandomNum()
    {
        theRandomNum = Random.Range(0,10);
    }
}