using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NsellMenu_script : MonoBehaviour
{
    private NsellMenu_script Instance;

    //public Sprite defaultTestCharImage;
    public GameObject sellTemplateCopy;
    public TextMeshProUGUI userNumToTotalNum;

    public int userNum = 1;
    public int numPage = 0;
    public int totalNum = 0;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        //userNumToTotalNum.enabled = false;
        Instance = this;
    }

    void OnEnable()
    {
        userNum = 1;
        numPage = 0;
        UpdateSellMenuList();
        
        userNumToTotalNum.enabled = true;
        userNumToTotalNum.gameObject.SetActive(true);
        //CurrentlySelectedSellMenuList(userNum);
    }

    void Update()
    {
        // character list is more than 0
        if(gameManager_script.Instance.totalCharList.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow) && userNum < transform.childCount-1 && ((userNum + numPage*5) < gameManager_script.Instance.totalCharList.Count) )
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum++;

                CurrentlySelectedSellMenuList(userNum);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && userNum >= transform.childCount-1 && ((userNum + numPage*5) < gameManager_script.Instance.totalCharList.Count))
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

            if(Input.GetKeyDown(KeyCode.UpArrow) && userNum > 1)
            {
                //Debug.Log("AAA " + userNum);
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum -= 1;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && userNum <= 5 && numPage > 0)
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

            if(Input.GetKeyDown(KeyCode.O) && gameManager_script.Instance.sellMenuToggle)
            {
                Destroy(transform.GetChild(userNum).gameObject);
                gameManager_script.Instance.totalCharList[userNum-1+(numPage*5)].GetComponent<testA_script>().DeleteCharacterA();
                gameManager_script.Instance.PauseMenuToggle();
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

                g.transform.GetChild(0).GetComponent<Image>().color = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().color;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charName;

                g.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charPassiveMoney.ToString();

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

                g.transform.GetChild(0).GetComponent<Image>().color = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().color;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charName;

                g.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charPassiveMoney.ToString();

                transform.GetChild(i+1).gameObject.SetActive(true);
            }   
        }
        else
        {
            //Debug.Log("UpdateSellMenuList 3");
            for(int i=0; i<5; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Image>().color = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<SpriteRenderer>().color;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charName;

                g.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + gameManagerScript.totalCharList[i + (numPage * 5)].GetComponent<testA_script>().charPassiveMoney.ToString();

                transform.GetChild(i+1).gameObject.SetActive(true);
            }   
        }
        userNumToTotalNum.enabled = true;  
    }

}
