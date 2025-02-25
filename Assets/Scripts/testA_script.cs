using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testA_script : MonoBehaviour
{
    [SerializeField] passiveMoneyTimer_script passiveMoneyTimerBar;



    public string charName;
    public int charPassiveMoney;
    public TextMeshProUGUI charPassiveMoneyText;
    public passiveMoneyTextUpAnimation_script passiveMoneyTextUpAnimationUp;
    public GameObject charPassiveMoneyObject;
    public bool sendPassiveMoneyTextUp = false;

    private Rigidbody2D thisCharRB;
    private Animator thisCharAnim;
    private SpriteRenderer thisCharSpriteR;

    private Vector2 charDecisionTime = new Vector2(1, 10);
    private Vector2 charLastMoveDirection;
    private float decisionTimeCount;
    private float passiveMoneyTimeCount;
    //private float charMoveDistance;
    private int charStatusDescision;
    private int charStatusDescisionSpecial;
    private bool charFacingRight = true;
    private bool charDanceAnim1Bool = false;
    private bool charDanceAnim2Bool = false;

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
        passiveMoneyTimeCount = 5;


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
            
        }
        CharAnimate();

        if(passiveMoneyTimeCount > 0)
        {
            passiveMoneyTimeCount -= Time.deltaTime;
            int tempMaxPassiveMoneyTimeCount = 5;
            passiveMoneyTimerBar.UpdatePassiveMoneyTimeBar(passiveMoneyTimeCount, tempMaxPassiveMoneyTimeCount);
        }
        else
        {
            passiveMoneyTextUpAnimationUp.PlayClipPassiveMoney();
            gameManager_script.Instance.AddPlayerMoney(charPassiveMoney);
            passiveMoneyTimeCount = 5;
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
        AutoPickCharColorCommon();
        AutoPickCharName();
        charPassiveMoney = Random.Range(1, 6);
        charPassiveMoneyText.text = "$" + charPassiveMoney.ToString();
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        float charMoveDistance = Random.Range(0f, 1.6f); //0, 1, 1.5
        charStatusDescision = Random.Range(0, 10); //0, 1, 2, 3, 4 5 6 7 8 9
        charDanceAnim1Bool = false;
        charDanceAnim2Bool = false;
        // MOVE
        if(charStatusDescision >= 0 && charStatusDescision <= 2)
        {
            int charStatusDescisionMovement = Random.Range(0, 8);
            switch(charStatusDescisionMovement)
            {
                case 0:
                //Debug.Log("fish will go RIGHT");
                thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance, 0);
                // charLastMoveDirection.x = thisCharRB.velocity.x;
                // charLastMoveDirection.y = thisCharRB.velocity.y;
                //Debug.Log("000" + thisCharRB.velocity);
                break;
                case 1:
                    //Debug.Log("fish will go LEFT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance * -1, 0);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y;
                    //Debug.Log("111" + thisCharRB.velocity.x);
                    break;
                case 2:
                    //Debug.Log("fish will go UP");
                    thisCharRB.velocity = new Vector2(0, transform.localScale.y * charMoveDistance);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y;
                    //Debug.Log("222" + thisCharRB.velocity);
                    break;
                case 3:
                    //Debug.Log("fish will go DOWN");
                    thisCharRB.velocity = new Vector2(0, transform.localScale.y * -charMoveDistance);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y;
                    //Debug.Log("333" + thisCharRB.velocity);
                    break;
                case 4:
                    //Debug.Log("fish will go UP RIGHT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance, transform.localScale.y * charMoveDistance);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y; 
                    break;
                case 5:
                    //Debug.Log("fish will go UP LEFT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance * -1, transform.localScale.y * charMoveDistance);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y;
                    break;  
                case 6:
                    //Debug.Log("fish will go DOWN RIGHT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance, transform.localScale.y * -charMoveDistance);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y;
                    break;  
                case 7:
                    //Debug.Log("fish will go DOWN LEFT");
                    thisCharRB.velocity = new Vector2(transform.localScale.x * charMoveDistance * -1, transform.localScale.y * -charMoveDistance);
                    // charLastMoveDirection.x = thisCharRB.velocity.x;
                    // charLastMoveDirection.y = thisCharRB.velocity.y;
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
            charStatusDescisionSpecial = Random.Range(0, 2);
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
    }

    void AutoPickCharName()
    {
        int tempNumInt = Random.Range(0,101);
        charName = "character" + tempNumInt.ToString();
    }

    void AutoPickCharColorCommon()
    {
        //new method with materials
        int tempNumInt = Random.Range(0, randomColorListArray.Length);
        thisCharSpriteR.material.color = randomColorListArray[tempNumInt];

        //old method
        // int tempIntR = Random.Range(0, 256);
        // int tempIntG = Random.Range(0, 256);
        // int tempIntB = Random.Range(0, 256);
        // thisCharSpriteR.material = new Color32((byte)tempIntR, (byte)tempIntG, (byte)tempIntB, 255);
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
