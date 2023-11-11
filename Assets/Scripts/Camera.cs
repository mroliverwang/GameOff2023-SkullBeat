using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;
    private float _cameraSpeed;
    private float _cameraVerticalSpeed;
    private Vector3 _initialPosition;
    
    void Start()
    {
        _initialPosition = transform.position;
        _cameraSpeed = 8f;
        _cameraVerticalSpeed = 3f;
    player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void LateUpdate()
    {

        if (player.transform.position.x > _initialPosition.x + 1)
        {
            //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.Translate(new Vector3(player.transform.position.x - transform.position.x, 0, 0) * _cameraSpeed * Time.deltaTime);


            if (Mathf.Abs(player.transform.position.y - transform.position.y) > 2f)
            {
                transform.Translate(new Vector3(0, player.transform.position.y + 1f - transform.position.y, 0) * _cameraVerticalSpeed * Time.deltaTime);
            }
        }
    }
}
