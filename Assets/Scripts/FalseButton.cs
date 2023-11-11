using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ActivateTriggerEvent : UnityEvent<GameObject> { }


public class FalseButton : MonoBehaviour
{

    public ActivateTriggerEvent m_activateTriggerEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            //play animation

            //check size and weight

            //execute door open event
            Activate(collision.gameObject);
        }
    }

    private void Activate(GameObject g)
    {

        m_activateTriggerEvent.Invoke(g);
    }
}
