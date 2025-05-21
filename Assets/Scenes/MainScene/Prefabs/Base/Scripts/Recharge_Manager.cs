using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Recharge_Manager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Brian O'DOnnel");        //Finds the player and calls the function to reset battery
                collision.gameObject.transform.Find("Player_Light").gameObject.GetComponent<FlashLight_Controller>().ResetBattery(collision.gameObject.name);
            }
        }
    }

