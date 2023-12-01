using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameUI : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerControl>().isOnHeadPhone)
        {
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,1f);
        }
    }
}
