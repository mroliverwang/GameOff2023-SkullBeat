using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTri : MonoBehaviour
{
    private float _speed;

    private void Awake()
    {
        _speed = -8.0f;
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, _speed , 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().PlayerDead();
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
