using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

public class Player_Name_Manager : MonoBehaviourPunCallbacks, IPunObservable
{
    public string PlayerId;

    public TextMeshProUGUI nametag;
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
        if (photonView.IsMine)
        {
            PlayerId = PhotonNetwork.LocalPlayer.UserId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.name = PlayerId;
        nametag.text = PlayerId;
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
}
