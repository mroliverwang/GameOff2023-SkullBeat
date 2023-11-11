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

            //play animation

            //check size and weight

            //execute door open event
            if (door != null)
            {
                door.GetComponent<Door>().DoorOpen();
            }
        }
    }
}
