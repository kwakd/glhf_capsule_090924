using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NsellMenu_script : MonoBehaviour
{
    public static NsellMenu_script Instance;

    //public Sprite defaultTestCharImage;
    public GameObject sellTemplateCopy;
    public TextMeshProUGUI userNumToTotalNum;
    public InputField characterNewName;

    public int userNum = 1;
    public int numPage = 0;
    public int totalNum = 0;
    //public bool isPlayerTypingName = false;
    public string tempStringNewCharName;

    void Start()
    {
        Instance = this;
        transform.GetChild(0).gameObject.SetActive(false);
        characterNewName.characterLimit = 14;
        //userNumToTotalNum.enabled = false;
        
    }

    void OnEnable()
    {
        userNum = 1;
        numPage = 0;
        UpdateSellMenuList();
        
        //userNumToTotalNum.enabled = true;
        userNumToTotalNum.gameObject.SetActive(true);
        //CurrentlySelectedSellMenuList(userNum);
    }

    void Update()
    {
        // character list is more than 0
        if(gameManager_script.Instance.totalCharList.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow) && userNum < transform.childCount-1 && ((userNum + numPage*5) < gameManager_script.Instance.totalCharList.Count) && gameManager_script.Instance.isPlayerTypingName == false)
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum++;

                CurrentlySelectedSellMenuList(userNum);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && userNum >= transform.childCount-1 && ((userNum + numPage*5) < gameManager_script.Instance.totalCharList.Count) && gameManager_script.Instance.isPlayerTypingName == false)
            {
                //Debug.Log("userNum Else If Hit " + userNum);
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum = 1;
                numPage += 1;
                DestorySellMenuList();
                UpdateSellMenuList();
                //CurrentlySelectedSellMenuList(userNum);
                //SetAllPanelsActiveSellMenuList();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow) && userNum > 1 && gameManager_script.Instance.isPlayerTypingName == false)
            {
                //Debug.Log("AAA " + userNum);
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum -= 1;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && userNum <= 5 && numPage > 0 && gameManager_script.Instance.isPlayerTypingName == false)
            {
                Debug.Log("BBB " + userNum);
                userNum = 5;
                numPage -= 1;
                DestorySellMenuList();
                UpdateSellMenuList();
                Debug.Log("CCC " + userNum);
            }
            SetAllPanelsActiveSellMenuList();
            //Debug.Log("userNum = " + userNum);
            SetAllPanelsWhiteSellMenuList();
            CurrentlySelectedSellMenuList(userNum);
            userNumToTotalNum.text = totalNum.ToString() + "/" + gameManager_script.Instance.totalCharList.Count.ToString();

            // sells the unit
            if(Input.GetKeyDown(KeyCode.O) && gameManager_script.Instance.sellMenuToggle && totalNum > 0 && gameManager_script.Instance.isPlayerTypingName == false && gameManager_script.Instance.totalCharList[userNum-1+(numPage*5)].GetComponent<testA_script>().tag != "goalTier")
            {
                Destroy(transform.GetChild(userNum).gameObject);
                gameManager_script.Instance.totalCharList[userNum-1+(numPage*5)].GetComponent<testA_script>().DeleteCharacterA();
                gameManager_script.Instance.PauseMenuToggle();
            }
            
            //changing units name
            if(Input.GetKeyDown(KeyCode.I) && gameManager_script.Instance.sellMenuToggle && totalNum > 0)
            {
                //Debug.Log("I is pressed");
                gameManager_script.Instance.isPlayerTypingName = true;
                characterNewName.gameObject.SetActive(true);
                characterNewName.ActivateInputField();
                //transform.GetChild(userNum).GetChild(4).GetComponent<InputField>().ActivateInputField();
                //transform.GetChild(userNum).GetChild(4).gameObject.SetActive(true);
                //transform.GetChild(userNum).GetChild(4).inputField.ActivateInputField();
            }

        }
        else
        {
            Debug.Log("LIST IS EMPTY");
            userNumToTotalNum.text = "0/0";

        }
        totalNum = (userNum + numPage*5); 
    }

    void OnDisable()
    {
        if(transform.childCount > 1)
        {
            for(int i=1; i<transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        userNumToTotalNum.enabled = false;
    }

    public void DestorySellMenuList()
    {
        if(transform.childCount > 1)
        {
            for(int i=1; i<transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    
    public void SetAllPanelsWhiteSellMenuList()
    {
        if(transform.childCount >= 6)
        {
            for(int i=1; i<transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Image>().color = new Color32(255,255,255,255);
            }
        }
    }

    public void CurrentlySelectedSellMenuList(int x)
    {
        // currently selected in list
        transform.GetChild(x).GetComponent<Image>().color = new Color32(255,165,165,255);
    }

    public void SetAllPanelsActiveSellMenuList()
    {
        
        if(gameManager_script.Instance.totalCharList.Count - (numPage * 5) <= 5)
        {
            for(int i=0; i<gameManager_script.Instance.totalCharList.Count - (numPage * 5); i++)
            {
                transform.GetChild(i+1).gameObject.SetActive(true);
            }
        }
        else
        {
            for(int i=0; i<5; i++)
            {
                transform.GetChild(i+1).gameObject.SetActive(true);
            }
        } 
    }

    public void UpdateSellMenuList()
    {
        GameObject g;
        
        gameManager_script gameManagerScript = FindObjectOfType<gameManager_script>();

        // if less than 5 characters
        if(gameManagerScript.totalCharList.Count <= 5)
        {
            //Debug.Log("UpdateSellMenuList 1");
            for(int i=0; i<gameManagerScript.totalCharList.Count; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Image>().sprite = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().sprite;
                g.transform.GetChild(0).GetComponent<Image>().material = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().material;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charName;

                g.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charPassiveMoney.ToString();

                g.transform.GetChild(3).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().passiveMoneyTimeCountOriginal.ToString();

                transform.GetChild(i+1).gameObject.SetActive(true);
            }
               
        }
        // else if totalCharacter - (numPage * 5) 
        else if(gameManagerScript.totalCharList.Count - (numPage * 5) <= 5)
        {
            //Debug.Log("UpdateSellMenuList 2");
            for(int i=0; i<gameManagerScript.totalCharList.Count - (numPage * 5); i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Image>().sprite = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().sprite;
                g.transform.GetChild(0).GetComponent<Image>().material = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().material;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charName;

                g.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charPassiveMoney.ToString();

                g.transform.GetChild(3).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().passiveMoneyTimeCountOriginal.ToString();

                transform.GetChild(i+1).gameObject.SetActive(true);
            }   
        }
        else
        {
            //Debug.Log("UpdateSellMenuList 3");
            for(int i=0; i<5; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Image>().sprite = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().sprite;
                g.transform.GetChild(0).GetComponent<Image>().material = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().material;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charName;

                g.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charPassiveMoney.ToString();

                g.transform.GetChild(3).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().passiveMoneyTimeCountOriginal.ToString();

                transform.GetChild(i+1).gameObject.SetActive(true);
            }   
        }
        userNumToTotalNum.enabled = true;  
    }

    public void ReadStringInput(string s)
    {
        tempStringNewCharName = s;
        gameManager_script.Instance.totalCharList[totalNum-1].GetComponent<testA_script>().charName = tempStringNewCharName;
        gameManager_script.Instance.isPlayerTypingName = false;
        characterNewName.Select();
        characterNewName.text = "";
        characterNewName.gameObject.SetActive(false);
        gameManager_script.Instance.PauseMenuToggle();
        //UpdateSellMenuList();
        //gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().material;
        //characterNewName.DeactivateInputField();
        //Debug.Log(tempStringNewCharName);
    }

}
