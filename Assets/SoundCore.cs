using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCore : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);


            // 设置音频的音量
            if (distance <= 10)
            {
                player.GetComponent<MusicEffectOnPlayer>().distanceFactor = 1;
            }
            else
            {
                player.GetComponent<MusicEffectOnPlayer>().distanceFactor = Mathf.Min(Mathf.Abs(1 - (distance - 10f) / 10), 1f);
            }
             
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.gameObject;
            collision.gameObject.GetComponent<MusicEffectOnPlayer>().isActive = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<MusicEffectOnPlayer>().isActive = false;
        }
    }
}
