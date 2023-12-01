using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class WinGameController : MonoBehaviour
{





    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if(OverAllSceneController.sceneIndex + 1 <= 7)
        {
            OverAllSceneController.sceneIndex++;
            SceneManager.LoadScene(OverAllSceneController.sceneIndex);
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }
        
        
    }


    public void ButtonClick()
    {
        if (OverAllSceneController.sceneIndex + 1 <= 7)
        {
            OverAllSceneController.sceneIndex++;
            SceneManager.LoadScene(OverAllSceneController.sceneIndex);
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }


    }





}





