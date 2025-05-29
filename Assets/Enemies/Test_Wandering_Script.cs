  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Test_Wandering_Script : MonoBehaviourPunCallbacks
{
    public int speed;
    public Vector2 TimeRange;
    public Vector2 Direction;
    float time;
    // Start is called before the first frame update

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ChooseDirection", RpcTarget.AllViaServer);
            //ChooseDirection();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        else
        {
            photonView.RPC("ChooseDirection", RpcTarget.AllViaServer);
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = Direction * speed;
    }

    [PunRPC]
    void ChooseDirection()
    {
        float num = Random.Range(0, 90);
        float x = num;
        float y = 90 - num;

        float xDirection = Mathf.Sin(Mathf.Deg2Rad * x);
        float yDirection = Mathf.Sin(Mathf.Deg2Rad * y);

        xDirection *= xDirection;
        yDirection *= yDirection;


        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            speed *= -1;
        }

        Direction = new Vector2(xDirection, yDirection);
        time = Random.Range(TimeRange.x, TimeRange.y);
    }

    void HuntPlayer()
    {

    }
}
