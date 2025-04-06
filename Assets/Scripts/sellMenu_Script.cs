using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sellMenu_Script : MonoBehaviour
{
    private gameManager_script gameManagerScript;

    //public Sprite defaultTestCharImage;
    public GameObject sellTemplateCopy;

    public int userNum = 1;
    public int numPage = 0;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        
    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        GameObject g;
        userNum = 1;
        numPage = 0;

        gameManagerScript = FindObjectOfType<gameManager_script>();
        
        if(gameManagerScript.totalCharList.Count < 6)
        {
            for(int i=1; i<=gameManagerScript.totalCharList.Count; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                //g.transform.GetChild(0).GetComponent<Image>().sprite = defaultTestCharImage;
                //Changes Sprite
                g.transform.GetChild(0).GetComponent<Image>().material = gameManagerScript.totalCharList[i-1].GetComponent<SpriteRenderer>().material;
                //Changes Sprite Material
                // g.transform.GetChild(0).GetComponent<Image>().color = gameManagerScript.totalCharList[i-1].GetComponent<SpriteRenderer>().color;
                //Changes
                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i-1].GetComponent<testA_script>().charName;

                
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for(int i=1; i<6; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Image>().sprite = gameManagerScript.totalCharList[i-1].GetComponent<SpriteRenderer>().sprite;

                g.transform.GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i-1].GetComponent<testA_script>().charName;

                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //currently selected fish
        if(gameManagerScript.totalCharList.Count > 0)
        {
            transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,165,165,255);

            if(Input.GetKeyDown(KeyCode.DownArrow) && userNum < transform.childCount-1 && ((userNum + numPage*9) < gameManagerScript.totalCharList.Count))
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum++;
                Debug.Log("userNum = " + userNum);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && userNum >= transform.childCount-1 && ((userNum + numPage*5) < gameManagerScript.totalCharList.Count))
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum = 1;
                numPage++;
                for(int i=1; i <= transform.childCount; i++)
                {
                    if((i + numPage*5) <= gameManagerScript.totalCharList.Count)
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = gameManagerScript.totalCharList[i-1+(5*numPage)].GetComponent<SpriteRenderer>().sprite; 

                        transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i-1+(5*numPage)].GetComponent<testA_script>().charName;                       
                    }
                    else
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                        transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "";
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.UpArrow) && userNum > 1)
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum--;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && userNum <= 1 && numPage > 0)
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum = 5;
                numPage--;
                for(int i=1; i <= transform.childCount; i++)
                {
                    if((i + numPage*5) <= gameManagerScript.totalCharList.Count)
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = gameManagerScript.totalCharList[i-1+(5*numPage)].GetComponent<SpriteRenderer>().sprite; 

                        transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = gameManagerScript.totalCharList[i-1+(5*numPage)].GetComponent<testA_script>().charName;                         
                    }
                    else
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                        transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "";
                    }
                    
                }
            }

            // if(Input.GetKeyDown(KeyCode.O))
            // {
            //     Destroy(transform.GetChild(userNum).gameObject);
            //     gameManagerScript.fishList[userNum-1+(5*numPage)].GetComponent<fishObject>().DeleteFish();
            //     gameManagerScript.myAudioSource.PlayOneShot(gameManagerScript.musicList[0], 0.7f);
            //     gameManagerScript.PauseMenuToggle();
            // }
        }
        else
        {
            Debug.Log("LIST IS EMPTY");
        }
        
    }

    void OnDisable()
    {
        for(int i=1; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }
}
