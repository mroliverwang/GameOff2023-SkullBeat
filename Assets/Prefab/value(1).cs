using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class value : MonoBehaviour
{
    public Transform Player; 
    private AudioSource audioSource;

    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player!= null)
        {
            // 计算对象与Player之间的距离
            float distance = Vector3.Distance(transform.position, Player.position);
            

            // 设置音频的音量
            if (distance<=10)
            {
                volume = 1;
            }
            else
            {
                volume = Mathf.Clamp01(1 - distance / 20);
            }

            audioSource.volume = volume;
            // 在控制台中打印距离
            Debug.Log("Distance to Player: " + distance+" "+"Audio volumn: " + volume);
        }
        else
        {
            Debug.LogWarning("Player reference is not set. Please assign the Player object in the Inspector.");
        }
    }
}
