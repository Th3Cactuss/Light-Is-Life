using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class FlashLight_Controller : MonoBehaviourPunCallbacks
{
    public int targetTime = 7;
    int remainingTime;
    int time = 1;
    GameObject Flashlight;
    Light2D Light;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) 
        {
            Flashlight = transform.Find("Player_FlashLight").gameObject;
            Light = Flashlight.GetComponent<Light2D>();
            ResetTime(); //starts the timer
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);   // Rotates the camera to face the mouse
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
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
            Debug.Log("Time Left: " + remainingTime);
            StartCoroutine(CountDown());
        }

        else 
        {
            photonView.RPC("LoseBattery", RpcTarget.AllBuffered, gameObject.transform.parent.gameObject.name);  //if the timer hits zero lose some battery
        }
    }

    [PunRPC]
    public void LoseBattery(string name)   
    {
        GameObject player = GameObject.Find(name);
        //if (Light.pointLightOuterAngle > 0)
        //{
            Debug.Log("THIS PLAYER LOSES BATTERY: " + player.name);
            //player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterRadius -= 0.25f;
            //player.transform.Find("Player_Light").GetComponent<FlashLight_Controller>().Light.pointLightOuterAngle -= 30;        //subtracts the light's angle and distance
            //player.GetComponent<Player_UI_Manager>().ChangeSprite(); //changes the battery UI
        //}
        ResetTime();
    }

    void ResetBattery()
    {
                                                        //Resets the flashlight to full power
    }


}
