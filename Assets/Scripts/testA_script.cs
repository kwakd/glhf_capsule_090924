using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testA_script : MonoBehaviour
{
    [SerializeField] public passiveMoneyTimer_script passiveMoneyTimerBar;

    public string charName;
    public int charPassiveMoney;
    public int charPassiveMoneyIntRandomNumChecker;
    public int passiveMoneyTimeCountOriginal;
    public TextMeshProUGUI charPassiveMoneyText;
    public passiveMoneyTextUpAnimation_script passiveMoneyTextUpAnimationUp;
    public GameObject charPassiveMoneyObject;
    public bool sendPassiveMoneyTextUp = false;

    private Rigidbody2D thisCharRB;
    private Animator thisCharAnim;
    private SpriteRenderer thisCharSpriteR;

    private Vector2 charDecisionTime = new Vector2(1, 10);
    private Vector2 passiveMoneyTime = new Vector2(50, 60); //change to 50,60 only at 5-6 for testing purposes
    private Vector2 charLastMoveDirection;
    private float decisionTimeCount;
    private float passiveMoneyTimeCount;
    private int charStatusDescision;
    private int charStatusDescisionSpecial;
    private bool charFacingRight = true;
    private bool charDanceAnim1Bool = false;
    private bool charDanceAnim2Bool = false;
    private bool charSpecialAnim1Bool = false;

    private Color [] randomColorListArray = {Color.red, Color.blue, Color.cyan, Color.gray, Color.green, Color.grey, Color.magenta, Color.red, Color.white, Color.yellow};

    // Start is called before the first frame update
    void Start()
    {
        thisCharRB = GetComponent<Rigidbody2D>();
        thisCharAnim = GetComponent<Animator>();
        thisCharSpriteR = GetComponent<SpriteRenderer>();

        passiveMoneyTimerBar = GetComponentInChildren<passiveMoneyTimer_script>();
        charPassiveMoneyText = GetComponentInChildren<TextMeshProUGUI>();
        passiveMoneyTextUpAnimationUp = GetComponentInChildren<passiveMoneyTextUpAnimation_script>();

        decisionTimeCount = Random.Range(charDecisionTime.x, charDecisionTime.y);
        passiveMoneyTimeCount = Random.Range(passiveMoneyTime.x, passiveMoneyTime.y);
        passiveMoneyTimeCountOriginal = (int)passiveMoneyTimeCount;

        AutoGenerateStats();
        //decisionTimeCount = Random.Range(charDecisionTime.x, charDecisionTime.y);

    }

    void FixedUpdate()
    {
        if(thisCharRB.velocity.x > 0 && !charFacingRight)
        {
            FlipSprite();
        }
        else if(thisCharRB.velocity.x < 0 && charFacingRight)
        {
            FlipSprite();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // characters descision counter
        if (decisionTimeCount > 0) 
        {
            decisionTimeCount -= Time.deltaTime;
            //Debug.Log(decisionTimeCount);
        }
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(charDecisionTime.x, charDecisionTime.y);
            
            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
            CharAnimate();
        }
        
        // characters passive money income to the player
        if(passiveMoneyTimeCount > 0)
        {
            passiveMoneyTimeCount -= Time.deltaTime;
            int tempMaxPassiveMoneyTimeCount = passiveMoneyTimeCountOriginal;
            passiveMoneyTimerBar.UpdatePassiveMoneyTimeBar(passiveMoneyTimeCount, tempMaxPassiveMoneyTimeCount);
        }
        else
        {
            passiveMoneyTextUpAnimationUp.PlayClipPassiveMoney();
            gameManager_script.Instance.AddPlayerMoney(charPassiveMoney);
            passiveMoneyTimeCount = (float)passiveMoneyTimeCountOriginal;
        }

        

        // if(sendPassiveMoneyTextUp)
        // {
        //     charPassiveMoneyObject.transform.position += new Vector3(transform.position.x, 0.025f, 9);
        //     if(charPassiveMoneyObject.transform.position.y > transform.position.y + 0.25)
        //     {
        //         sendPassiveMoneyTextUp = false;
        //         charPassiveMoneyObject.transform.position = new Vector3(transform.position.x, transform.position.y, 9);
        //     }
        // }

    }

    void AutoGenerateStats()
    {
        int tempInt = Random.Range(0, 101);
        if((this.tag == "legendaryTier" || this.tag == "epicTier") && tempInt >= 95)
        {
            AutoPickCharColorCommon();
            // potentially make this version also give more money
        }
        else if((this.tag == "commonTier" || this.tag == "uncommonTier"))
        {
            AutoPickCharColorCommon();
        }
        AutoPickCharName();
        AutoPickCharPassiveMoney();
        charPassiveMoneyText.text = "$" + charPassiveMoney.ToString();
    }

    void AutoPickCharPassiveMoney()
    {
        if(this.tag == "commonTier")
        {
            charPassiveMoneyIntRandomNumChecker = Random.Range(1, 6);
            charPassiveMoney = charPassiveMoneyIntRandomNumChecker + passiveMoneyTimeCountOriginal;
        }
        else if(this.tag == "uncommonTier")
        {
            charPassiveMoneyIntRandomNumChecker = Random.Range(6, 26);
            charPassiveMoney = charPassiveMoneyIntRandomNumChecker + passiveMoneyTimeCountOriginal;
        }
        else if(this.tag == "epicTier")
        {
            charPassiveMoneyIntRandomNumChecker = Random.Range(50, 76);
            charPassiveMoney = charPassiveMoneyIntRandomNumChecker + passiveMoneyTimeCountOriginal;
        }
        else if(this.tag == "legendaryTier")
        {
            charPassiveMoneyIntRandomNumChecker = Random.Range(100, 201);
            charPassiveMoney = charPassiveMoneyIntRandomNumChecker + passiveMoneyTimeCountOriginal;
        }

    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        float charMoveDistance = Random.Range(0f, 1.6f); //0, 1, 1.5
        charStatusDescision = Random.Range(0, 10); //0, 1, 2, 3, 4 5 6 7 8 9
        charDanceAnim1Bool = false;
        charDanceAnim2Bool = false;
        charSpecialAnim1Bool = false;

        // MOVE
        if(charStatusDescision >= 0 && charStatusDescision <= 2)
        {
            int charStatusDescisionMovement = Random.Range(0, 8);
            switch(charStatusDescisionMovement)
            {
                case 0:
                //Debug.Log("fish will go RIGHT");
                thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance, 0);
                break;
                case 1:
                    //Debug.Log("fish will go LEFT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance * -1, 0);
                    break;
                case 2:
                    //Debug.Log("fish will go UP");
                    thisCharRB.velocity = new Vector2(0, transform.localScale.y * charMoveDistance);
                    break;
                case 3:
                    //Debug.Log("fish will go DOWN");
                    thisCharRB.velocity = new Vector2(0, transform.localScale.y * -charMoveDistance);
                    break;
                case 4:
                    //Debug.Log("fish will go UP RIGHT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance, transform.localScale.y * charMoveDistance);
                    break;
                case 5:
                    //Debug.Log("fish will go UP LEFT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance * -1, transform.localScale.y * charMoveDistance);
                    break;  
                case 6:
                    //Debug.Log("fish will go DOWN RIGHT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance, transform.localScale.y * -charMoveDistance);
                    break;  
                case 7:
                    //Debug.Log("fish will go DOWN LEFT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance * -1, transform.localScale.y * -charMoveDistance);
                    break;
                default:
                    break;
            }
        }
        // DONT DO ANYTHING
        else if(charStatusDescision >= 3 && charStatusDescision <= 8)
        {
            thisCharRB.velocity = new Vector2(0, 0);
        }
        // SPECIAL ACTION
        else if(charStatusDescision >= 9 && charStatusDescision <= 9)
        {
            if(this.tag == "legendaryTier")
            {
                Debug.Log("Legendary TIER HIT DEBUG");
                charStatusDescisionSpecial = Random.Range(0, 3);
            }
            else
            {
                charStatusDescisionSpecial = Random.Range(0, 2);
            }

            switch(charStatusDescisionSpecial)
            {
                case 0:
                    thisCharRB.velocity = new Vector2(0, 0);
                    charDanceAnim1Bool = true;
                    break;
                case 1:
                    thisCharRB.velocity = new Vector2(0, 0);
                    charDanceAnim2Bool = true;
                    break;
                case 2:
                    thisCharRB.velocity = new Vector2(0, 0);
                    charSpecialAnim1Bool = true;
                    break;    
                default:
                    break;
            }
        }

        //Debug.Log("x: " + thisCharRB.velocity.x + " || y: " + thisCharRB.velocity.y);

    }

    void CharAnimate()
    {
        thisCharAnim.SetFloat("charMoveMagnitude", thisCharRB.velocity.magnitude);
        thisCharAnim.SetBool("charDanceAnim1", charDanceAnim1Bool);
        thisCharAnim.SetBool("charDanceAnim2", charDanceAnim2Bool);
        if(this.tag == "legendaryTier")
        {
            thisCharAnim.SetBool("charSpecialAnim1", charSpecialAnim1Bool);
        }
        //thisCharAnim.SetBool("charDanceAnim3", charDanceAnim3Bool); WIP legendary
    }

    void AutoPickCharColorCommon()
    {
        //new method with materials
        int tempNumInt = Random.Range(0, randomColorListArray.Length);
        
        thisCharSpriteR.material.color = randomColorListArray[tempNumInt];
        Color.Lerp(randomColorListArray[tempNumInt], Color.white, 1.0f);
        //old method
        // int tempIntR = Random.Range(0, 256);
        // int tempIntG = Random.Range(0, 256);
        // int tempIntB = Random.Range(0, 256);
        // thisCharSpriteR.material = new Color32((byte)tempIntR, (byte)tempIntG, (byte)tempIntB, 255);
    }

    void AutoPickCharName()
    {
        int tempNumInt = Random.Range(0,101);
        charName = "character" + tempNumInt.ToString();
    }


    void FlipSprite()
    {
        charFacingRight = !charFacingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    public void DeleteCharacterA()
    {
        gameManager_script.Instance.totalCharList.Remove(this.gameObject);
        Object.Destroy(this.gameObject);
    }
    
    void OnDisable()
    {
        gameManager_script.Instance.playerTotalMoney += charPassiveMoney;
    }
}
