using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager_script : MonoBehaviour
{
    public static gameManager_script Instance;

    public int playerTotalMoney;
    public TextMeshProUGUI playerTotalMoneyText;

    public bool sellMenuToggle;
    public bool isPlayerTypingName = false; 

    public GameObject testCharA;
    public GameObject sellMenu;

    
    //public GameObject tempChar;

    public List<GameObject> totalCharList = new List<GameObject>();
    public List<GameObject> totalEpicCharList = new List<GameObject>();
    public List<GameObject> totalLegendaryCharList = new List<GameObject>();

    void Start()
    {
        Instance = this;
        sellMenu.SetActive(false);
        sellMenuToggle = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && !sellMenuToggle && playerTotalMoney >= 100)
        {
            playerTotalMoney -= 100;
            SpawnChar();
        }

        // devkey to spawn unit
        if(Input.GetKeyDown(KeyCode.Q) && !sellMenuToggle)
        {
            SpawnChar();
        }
        //devkey to spawn better units
        if(Input.GetKeyDown(KeyCode.E) && !sellMenuToggle)
        {
            DevSpawnChar();
        }

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

        GameObject tempChar;
        int tempInt = Random.Range(0, 101);
        //common
        if(tempInt >= 0 && tempInt <= 45)
        {
            tempChar = Instantiate(testCharA, new Vector3(testSpawnLocationX, testSpawnLocationY, 0), testCharA.transform.rotation);
            totalCharList.Add(tempChar);
        }
        //uncommon
        else if(tempInt > 45 && tempInt <= 78)
        {
            Debug.Log("WIP SHOULD HAVE SPAWNNED UNCOMMON");
            tempChar = Instantiate(testCharA, new Vector3(testSpawnLocationX, testSpawnLocationY, 0), testCharA.transform.rotation);
            totalCharList.Add(tempChar);
        }
        //epic
        else if(tempInt > 78 && tempInt <= 98)
        {
            int temptempInt = Random.Range(0,totalEpicCharList.Count);
            tempChar = Instantiate(totalEpicCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalEpicCharList[temptempInt].transform.rotation);
            totalCharList.Add(tempChar);
        }
        //legendary
        else if(tempInt > 98 && tempInt <= 100)
        {
            int temptempInt = Random.Range(0,totalLegendaryCharList.Count);
            tempChar = Instantiate(totalLegendaryCharList[temptempInt], new Vector3(testSpawnLocationX, testSpawnLocationY, 0), totalLegendaryCharList[temptempInt].transform.rotation);
            totalCharList.Add(tempChar);
        }

    }

    public void DevSpawnChar()
    {
        int testSpawnLocationX = Mathf.FloorToInt(Random.Range(-4f, 4.5f));
        int testSpawnLocationY = Mathf.FloorToInt(Random.Range(-2.5f, 3f));

        GameObject tempChar;
        int tempInt = Random.Range(1, 2); //0, 1

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

    public void PauseMenuToggle()
    {
        if(!sellMenuToggle)
        {
            sellMenu.SetActive(true);
            sellMenuToggle = true;
        }
        else
        {
            sellMenu.SetActive(false);
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