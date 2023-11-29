using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPhone : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!collision.gameObject.GetComponent<PlayerControl>().isOnHeadPhone)
            {
                if (collision.gameObject.GetComponent<PlayerControl>()._playerInput.Player.Interact.triggered)
                {
                    collision.gameObject.GetComponent<PlayerControl>().isOnHeadPhone = true;
                    collision.gameObject.GetComponent<MusicEffectOnPlayer>().isActive = true;
                    Destroy(gameObject);
                }
                
            }
        }
    }
    
}
