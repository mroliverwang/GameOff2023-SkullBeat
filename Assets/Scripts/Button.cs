using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject door;

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
