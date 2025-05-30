using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class FlashLight_Controller : MonoBehaviourPunCallbacks
{
    public int targetTime = 60;
    int remainingTime;
    int time = 1;

    public float lightLength;
    public float lightAngle;

    GameObject Flashlight;

    Light2D Light;

    public List<Ray> FlashLightColliders;
    // Start is called before the first frame update
    void Start()
    {
        Flashlight = transform.Find("Player_FlashLight").gameObject;
        Light = Flashlight.GetComponent<Light2D>();

        if (photonView.IsMine) 
        {
            photonView.RPC("SetBattery", RpcTarget.AllBufferedViaServer, gameObject.transform.parent.gameObject.name); //sets the battery sprite
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            FlashLightRotation();
            RayRotation();
        }
        
    }

    void FlashLightRotation()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(gameObject.transform.parent.transform.position);   // Rotates the light to face the mouse

        mousePos.x = objectPos.x - mousePos.x;
        mousePos.y = objectPos.y - mousePos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    void RayRotation()
    {
        float angle = gameObject.transform.rotation.z;

        float angleRadians = angle * Mathf.Rad2Deg;

        float x = Mathf.Cos(angleRadians);
        float y = Mathf.Sin(angleRadians);

        Vector3 TargetPosition = new Vector3(x, y, 0);

        Vector3 FinalPosition = gameObject.transform.position + TargetPosition;

        Debug.Log(angleRadians + "   " + FinalPosition);

        Debug.DrawRay(gameObject.transform.position, FinalPosition, Color.blue);
    }
    void ResetTime()
    {
        remainingTime = targetTime;
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(time);
        if (remainingTime > 0)
        {
                remainingTime -= time;
                StartCoroutine(CountDown());   
        }



        else 
        {
            photonView.RPC("LoseBattery", RpcTarget.AllBufferedViaServer, gameObject.transform.parent.gameObject.name);  //if the timer hits zero lose some battery
            yield break;
        }
    }

    [PunRPC]
    public void SetBattery(string name)
    {
        GameObject player = GameObject.Find(name);
        player.GetComponent<Player_UI_Manager>().SetSprite(); //changes the battery UI
        ResetTime(); //starts the timer
    }

    [PunRPC]
    public void LoseBattery(string name)   
    {
        GameObject player = GameObject.Find(name);
        if (Light.pointLightOuterAngle > 0)
        {
            player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterRadius -= 0.25f;
            player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterAngle -= 30;        //subtracts the light's angle and distance

            this.lightLength = player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterRadius;
            this.lightAngle = player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterAngle;

            player.GetComponent<Player_UI_Manager>().ChangeSprite(); //changes the battery UI
        }
        ResetTime();
    }   

    [PunRPC]
     void resetBattery(string name)
    {
        GameObject player = GameObject.Find(name);
        player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterRadius = 3.0f;
        player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterAngle = 90;


        this.lightLength = player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterRadius;
        this.lightAngle = player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterAngle;

        player.GetComponent<Player_UI_Manager>().ResetSprite();
        remainingTime = targetTime;
                                                                                                                            //Resets the flashlight to full power
    }

    public void ResetBattery(string name)
    {
        photonView.RPC("resetBattery", RpcTarget.AllViaServer, name);
    }


}
