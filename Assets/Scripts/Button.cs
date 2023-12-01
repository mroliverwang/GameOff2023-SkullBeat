using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Button : MonoBehaviour
{

    public GameObject door;

    private float y;

    private void Awake()
    {
        y = transform.localPosition.y - 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player" )
        {
            

            if (gameObject.tag == "GoodButton")
            {
                Debug.Log(gameObject.tag);
                if (door != null)
                {
                    door.GetComponent<Door>().DoorOpen();
                    transform.DOLocalMoveY(y, 0.4f);
                }
            }
            else
            {
                //check size and weight
                if (collision.gameObject.GetComponent<Rigidbody2D>().mass > 1f)
                {
                    Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>().mass);
                    //execute door open event
                    if (door != null)
                    {
                        door.GetComponent<Door>().DoorOpen();
                        transform.DOLocalMoveY(y, 0.4f);
                    }
                }

            }


        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            //play animation
            if (gameObject.tag == null)
            {
                //check size and weight
                if (collision.gameObject.GetComponent<Rigidbody2D>().mass > 1.4f)
                {
                    //execute door open event
                    if (door != null)
                    {
                        door.GetComponent<Door>().DoorOpen();
                    }
                }

            }
            else
            {
                if (door != null)
                {
                    door.GetComponent<Door>().DoorOpen();
                }
            }

        }
    }*/
}
