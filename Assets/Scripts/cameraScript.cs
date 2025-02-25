using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class cameraScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private float virtualCameraZoomFloat;
    public TextMeshProUGUI virtualCameraZoomFloatText;

    private gameManager_script gameManagerScript;
    

    private float cameraSpeed = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<gameManager_script>();
        virtualCameraZoomFloat = 4.5f;
        virtualCamera.m_Lens.OrthographicSize = virtualCameraZoomFloat;
        virtualCameraZoomFloatText.text = "Zoom: " + virtualCameraZoomFloat.ToString();
        
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
        // to make is to that the camera doesnt get stuck on the edges -> itll teleport the camera a very small distance so it wont get stuck
        else
        {
            if(this.gameObject.transform.position.x <= -10)
            {
                this.gameObject.transform.position = new Vector3(-9.999f, this.gameObject.transform.position.y, -10.0f);
            }
            else if(this.gameObject.transform.position.x >= 10)
            {
                this.gameObject.transform.position = new Vector3(9.999f, this.gameObject.transform.position.y, -10.0f);
            }

            if(this.gameObject.transform.position.y <= -5)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -4.999f, -10.0f);
            }
            else if(this.gameObject.transform.position.y >= 5)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 4.999f, -10.0f);
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            ZoomCameraToggle();
            //Debug.Log("CAMERA TOGGLE: " + virtualCameraZoomBool);
        }
    }

    public void ZoomCameraToggle()
    {
        if(virtualCameraZoomFloat > 3.5)
        {
            virtualCameraZoomFloat -= 1.0f;
        }
        else
        {
            virtualCameraZoomFloat = 6.5f;
        }
        
        virtualCamera.m_Lens.OrthographicSize = virtualCameraZoomFloat;
        virtualCameraZoomFloatText.text = "Zoom: " + virtualCameraZoomFloat.ToString();
    }
}
