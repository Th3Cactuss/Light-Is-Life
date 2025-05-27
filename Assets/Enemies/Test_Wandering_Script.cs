using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Wandering_Script : MonoBehaviour
{
    public int speed;
    public Vector2 TimeRange;
    public Vector2 Direction;
    float time;
    // Start is called before the first frame update

    void Start()
    {
        ChooseDirection();
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
            ChooseDirection();
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = Direction * speed;
    }

    void ChooseDirection()
    {
        float num = Random.Range(0, 90);
        Debug.Log("NUM: " + num);
        float x = num;
        float y = 90 - num;

        float xDirection = Mathf.Sin(Mathf.Deg2Rad * x);
        float yDirection = Mathf.Sin(Mathf.Deg2Rad * y);

        xDirection *= xDirection;
        yDirection *= yDirection;

        Debug.Log("Nums: " + xDirection + " " + yDirection);

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
