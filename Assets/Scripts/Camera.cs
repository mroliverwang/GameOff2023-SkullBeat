using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;
    private float _cameraSpeed;
    private Vector3 _initialPosition;
    
    void Start()
    {
        _initialPosition = transform.position;
        _cameraSpeed = 8f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void LateUpdate()
    {

        if(player.transform.position.x > _initialPosition.x + 1)
        {
         
            //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.Translate(new Vector3 (player.transform.position.x - transform.position.x, transform.position.y, 0) *_cameraSpeed * Time.deltaTime);
            
        }
        
    }
}
