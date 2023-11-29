using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Earphone : MonoBehaviour
{
    public Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = new Vector3(-0.51f, 0.22f, 1);
    }

    public void fly()
    {
        transform.DOLocalMoveX(5f, 3f);
        transform.DOLocalMoveY(10f, 4f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            transform.localPosition = initialPosition;
            GetComponentInParent<PlayerControl>().isOnHeadPhone = false;
        });
    }
}
