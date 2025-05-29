using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

public class Player_Name_Manager : MonoBehaviourPunCallbacks, IPunObservable, IDataPersistance
{
    public string PlayerId;

    public TextMeshProUGUI nametag;

    public bool gameInitialized;
    // Start is called before the first frame update
    void Start()
    {
        nametag = gameObject.transform.Find("NameTagParent").transform.Find("NameTag").gameObject.ConvertTo<TextMeshProUGUI>();
    }

    public void Temp()
    {
        photonView.RPC("SetId", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SetId()
    {
        if (photonView.IsMine && gameInitialized != true)
        {
            PlayerId = PhotonNetwork.LocalPlayer.UserId;
            gameInitialized = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.name = PlayerId;
        nametag.text = gameObject.name;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PlayerId);
        }

        else
        {
            this.PlayerId = (string)stream.ReceiveNext();
        }
    }

    public void LoadData(GameData data)
    {
        this.gameInitialized = data.gameInitialized;

        if (this.gameInitialized == true)
        {
            this.PlayerId = data.PlayerName;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.PlayerName = this.PlayerId;
        data.gameInitialized = this.gameInitialized;
    }
}
