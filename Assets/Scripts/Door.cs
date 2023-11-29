using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorOpen()
    {
        transform.DOMoveY(15.0f, 2.0f);
    }

    public void DoorClose()
    {
        transform.DOMoveY(-15.0f, 2.0f);
    }
}
