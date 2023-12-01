using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public void toGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void toCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void toInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void toStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}

