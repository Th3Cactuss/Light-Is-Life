using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviourPunCallbacks, IDataPersistance
{
    public float speed;
    private Rigidbody2D rb2d;
    Camera cam;

    [SerializeField] private Vector2 Player_Position;

    void Start()
    {
        if (photonView.IsMine) 
        {
            rb2d = GetComponent<Rigidbody2D>();
            gameObject.transform.position = Player_Position;
            cam = Camera.main;                                          //Camera Work
            cam.transform.parent = gameObject.transform;
            cam.transform.position = gameObject.transform.position - new Vector3(0,0,1);
            PhotonNetwork.LocalPlayer.JoinTeam(1); //joins the TeamManager
        }

    }

    void Update()
    {
        if (photonView.IsMine) 
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            rb2d.velocity = new Vector2(((moveHorizontal * Time.deltaTime) * speed * 25), ((moveVertical * Time.deltaTime) * speed * 25)); //leave this for now

            Player_Position = gameObject.transform.position;

            // Try out this delta time method??
            //rb2d.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        }

    }

    public void LoadData(GameData data)
    {
        this.Player_Position = data.PlayerPosition;
        Debug.Log("Claire");
    }

    public void SaveData(ref GameData data)
    {
        data.PlayerPosition = this.Player_Position;
        Debug.Log("Davis");
    }



}
