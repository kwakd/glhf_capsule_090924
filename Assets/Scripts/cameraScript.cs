using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private gameManager_script gameManagerScript;

    private float cameraSpeed = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<gameManager_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if((this.gameObject.transform.position.x < 10 && this.gameObject.transform.position.x > -10) && (this.gameObject.transform.position.y < 5 && this.gameObject.transform.position.y > -5) && !gameManagerScript.sellMenuToggle)
        {
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(cameraSpeed * Time.deltaTime,0,0));
            }
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime,0,0));
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0,-cameraSpeed * Time.deltaTime,0));
            }
            if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0,cameraSpeed * Time.deltaTime,0));
            }
        }
        else
        {
            if(this.gameObject.transform.position.x <= -10)
            {
                this.gameObject.transform.position = new Vector3(-9.9f, this.gameObject.transform.position.y, -10.0f);
            }
            else if(this.gameObject.transform.position.x >= 10)
            {
                this.gameObject.transform.position = new Vector3(9.9f, this.gameObject.transform.position.y, -10.0f);
            }

            if(this.gameObject.transform.position.y <= -5)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -4.9f, -10.0f);
            }
            else if(this.gameObject.transform.position.y >= 5)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 4.9f, -10.0f);
            }
        }
    }
}
