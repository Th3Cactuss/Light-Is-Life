using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviourPunCallbacks
{
    public float speed;
    private Rigidbody2D rb2d;
    Camera cam;

    void Start()
    {
        if (photonView.IsMine) 
        {
            rb2d = GetComponent<Rigidbody2D>();
            cam = Camera.main;                                          //Camera Work
            cam.transform.parent = gameObject.transform;
            PhotonNetwork.LocalPlayer.JoinTeam(1); //joins the TeamManager
        }

    }

    void Update()
    {
        if (photonView.IsMine) 
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            rb2d.velocity = new Vector2((moveHorizontal * speed) * Time.deltaTime, (moveVertical * speed) * Time.deltaTime);

            // Try out this delta time method??
            //rb2d.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        }

    }



}
