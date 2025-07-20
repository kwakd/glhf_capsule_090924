using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager_script : MonoBehaviour
{
    public static gameManager_script Instance;

    public int playerTotalMoney;
    public TextMeshProUGUI playerTotalMoneyText;

    public TextMeshProUGUI normalMenuText;
    public TextMeshProUGUI sellMenuText;

    public bool sellMenuToggle;
    public bool isPlayerTypingName = false;
    public bool playerPassiveBarToggle = false; 
    public bool isPurchaseAnimPlaying = false;
    public bool kingPurchased = false;

    public GameObject testCharA;
    public GameObject goalCharA;
    public GameObject sellMenu;

    
    //public GameObject tempChar;

    public List<GameObject> totalCharList = new List<GameObject>();
    public List<GameObject> totalUncommonCharList = new List<GameObject>();
    public List<GameObject> totalEpicCharList = new List<GameObject>();
    public List<GameObject> totalLegendaryCharList = new List<GameObject>();

    private Animator thisGameManagerScript_Animator;

    void Start()
    {
        Instance = this;
        sellMenu.SetActive(false);
        sellMenuToggle = false;

        thisGameManagerScript_Animator = GetComponent<Animator>();
        thisGameManagerScript_Animator.enabled = false;
        sellMenuText.enabled = false;
        playerTotalMoney = 500;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && !sellMenuToggle && playerTotalMoney >= 100 && !isPurchaseAnimPlaying)
        {
            playerTotalMoney -= 100;
            SpawnChar();
        }
        if(Input.GetKeyDown(KeyCode.S) && !sellMenuToggle && playerTotalMoney >= 1000 && !isPurchaseAnimPlaying && kingPurchased != true)
        {
            playerTotalMoney -= 1000;
            kingPurchased = true;
            SpawnCharGoal();
        }

        // devkey to spawn unit
        if(Input.GetKeyDown(KeyCode.Q) && !sellMenuToggle && !isPurchaseAnimPlaying)
        {
            SpawnChar();
        }
        //devkey to spawn better units
        if(Input.GetKeyDown(KeyCode.E) && !sellMenuToggle)
        {
            DevSpawnChar();
        }
        //devkey to spawn common/uncommon
        if(Input.GetKeyDown(KeyCode.L) && !sellMenuToggle)
        {
            DevSpawnCharCommon();
        }

        //activate or disable slider
        if(Input.GetKeyDown(KeyCode.A) && !sellMenuToggle)
        {
            if(!playerPassiveBarToggle)
            {
                for(int i=0; i<totalCharList.Count; i++)
                {
                    totalCharList[i].GetComponent<testA_script>().passiveMoneyTimerBar.DisableSelfSlider();
                }
                playerPassiveBarToggle = true;
            }
            else
            {
                for(int i=0; i<totalCharList.Count; i++)
                {
                    totalCharList[i].GetComponent<testA_script>().passiveMoneyTimerBar.ActivateSelfSlider();
                }
                playerPassiveBarToggle = false;
            }
        }

        // open Menu
        if(Input.GetKeyDown(KeyCode.P) && !isPlayerTypingName)
        {
            PauseMenuToggle();
        }



        playerTotalMoneyText.text = "$" + playerTotalMoney.ToString();
    }

    public void SpawnChar()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

       // GameObject tempChar;
        int tempInt = Random.Range(0, 101);
        //common
        if(tempInt >= 0 && tempInt <= 45)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            thisGameManagerScript_Animator.enabled = true;
            isPurchaseAnimPlaying = true;
            thisGameManagerScript_Animator.Play("PurchaseAnimation_Common");
        }
        //uncommon
        else if(tempInt > 45 && tempInt <= 78)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            thisGameManagerScript_Animator.enabled = true;
            isPurchaseAnimPlaying = true;
            thisGameManagerScript_Animator.Play("PurchaseAnimation_Uncommon");
        }
        //epic
        else if(tempInt > 78 && tempInt <= 98)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            thisGameManagerScript_Animator.enabled = true;
            isPurchaseAnimPlaying = true;
            thisGameManagerScript_Animator.Play("PurchaseAnimation_Epic");
        }
        //legendary
        else if(tempInt > 98 && tempInt <= 100)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            thisGameManagerScript_Animator.enabled = true;
            isPurchaseAnimPlaying = true;
            thisGameManagerScript_Animator.Play("PurchaseAnimation_Legendary");
        }
    }

    public void SpawnCharCommon()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;

        //Debug.Log("test Common");
        tempChar = Instantiate(testCharA, new Vector3(testSpawnLocationX, testSpawnLocationY, 0), testCharA.transform.rotation);
        totalCharList.Add(tempChar);
        GetComponent<SpriteRenderer>().enabled = false;
        thisGameManagerScript_Animator.enabled = false;
        isPurchaseAnimPlaying = false;
    }
    public void SpawnCharUncommon()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;

        int temptempInt = Random.Range(0,totalUncommonCharList.Count);
        tempChar = Instantiate(totalUncommonCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalUncommonCharList[temptempInt].transform.rotation);
        totalCharList.Add(tempChar);
        GetComponent<SpriteRenderer>().enabled = false;
        thisGameManagerScript_Animator.enabled = false;
        isPurchaseAnimPlaying = false;
    }
    public void SpawnCharEpic()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;

        int temptempInt = Random.Range(0,totalEpicCharList.Count);
        tempChar = Instantiate(totalEpicCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalEpicCharList[temptempInt].transform.rotation);
        totalCharList.Add(tempChar);
        GetComponent<SpriteRenderer>().enabled = false;
        thisGameManagerScript_Animator.enabled = false;
        isPurchaseAnimPlaying = false;
    }
    public void SpawnCharLegendary()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;

        int temptempInt = Random.Range(0,totalLegendaryCharList.Count);
        tempChar = Instantiate(totalLegendaryCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalLegendaryCharList[temptempInt].transform.rotation);
        totalCharList.Add(tempChar);
        GetComponent<SpriteRenderer>().enabled = false;
        thisGameManagerScript_Animator.enabled = false;
        isPurchaseAnimPlaying = false;
    }
    public void SpawnCharGoal()
    {
        GameObject tempChar;
        tempChar = Instantiate(goalCharA, new Vector3(0, 0, 0), transform.rotation);
        tempChar.transform.localScale = new Vector3(2, 2, 2);
        // if i want to add the KING to the list
        //totalCharList.Add(tempChar);
    }

    public void DevSpawnChar()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;
        int tempInt = Random.Range(0, 2); //0, 1

        //epic
        if(tempInt == 0)
        {
            int temptempInt = Random.Range(0,totalEpicCharList.Count);
            tempChar = Instantiate(totalEpicCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalEpicCharList[temptempInt].transform.rotation);
            totalCharList.Add(tempChar);
        }
        //legendary
        else
        {
            int temptempInt = Random.Range(0,totalLegendaryCharList.Count);
            tempChar = Instantiate(totalLegendaryCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalLegendaryCharList[temptempInt].transform.rotation);
            totalCharList.Add(tempChar);
        }
    }

    public void DevSpawnCharCommon()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;
        int tempInt = Random.Range(0, 1); //0, 1

        //Common
        if(tempInt == 0)
        {
            tempChar = Instantiate(testCharA, new Vector3(testSpawnLocationX, testSpawnLocationY, 0), testCharA.transform.rotation);
            totalCharList.Add(tempChar);
        }
        //Uncommon
        else
        {
            int temptempInt = Random.Range(0,totalUncommonCharList.Count);
            tempChar = Instantiate(totalUncommonCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalUncommonCharList[temptempInt].transform.rotation);
            totalCharList.Add(tempChar);
        }

    }

    public void PauseMenuToggle()
    {
        if(!sellMenuToggle)
        {
            sellMenu.SetActive(true);
            normalMenuText.enabled = false;
            sellMenuText.enabled = true;
            sellMenuToggle = true;
        }
        else
        {
            sellMenu.SetActive(false);
            normalMenuText.enabled = true;
            sellMenuText.enabled = false;
            sellMenuToggle = false;
        }
    }

    public void AddPlayerMoney(int x)
    {
        playerTotalMoney += x;
    }
}

//TODO
// gotta fix up this menu system but got a decent system somewhat going.
    // eventually change testCharA spawn to an actual character