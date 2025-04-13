using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Spawner : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    List<Player> Players = new List<Player>();
    public List<GameObject> players = new List<GameObject>();
    PhotonTeamsManager PTM;
    void Start()
    {
       PTM = gameObject.GetComponent<PhotonTeamsManager>();
       GameObject TempPlayer = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity); //Spawns Player
        TempPlayer.GetComponent<Player_Name_Manager>().Temp();
    }





    // Update is called once per frame
    void Update()
    {

    }

}
