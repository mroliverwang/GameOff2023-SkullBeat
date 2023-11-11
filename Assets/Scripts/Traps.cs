using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Traps : MonoBehaviour
{


    public void ChasePlayer()
    {
        if (transform != null)
        {
            transform.DOMoveY(-3.0f, 2.0f);
        }
    }
}
