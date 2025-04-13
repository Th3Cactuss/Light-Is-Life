using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player_UI_Manager : MonoBehaviourPunCallbacks
{
    public List<Sprite> Battery_Sprites;
    int num = 0;

    public GameObject UI_Obj;
    public Canvas UI;
    public Image Battery;
    // Start is called before the first frame update
    void Start()
    {
        UI_Obj = gameObject.transform.Find("UI").ConvertTo<GameObject>();
        UI = UI_Obj.transform.Find("UI").ConvertTo<Canvas>();
        Battery = UI.transform.Find("Battery").ConvertTo<Image>();

        if (photonView.IsMine != true)
        {
            UI_Obj.SetActive(false);
        }
    }

    public void ChangeSprite()
    {
        num++;
        Battery.sprite = Battery_Sprites[num];
    }

    public void ResetSprite()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
