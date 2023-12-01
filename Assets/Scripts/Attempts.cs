using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attempts : MonoBehaviour
{
    public static int attempt = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().SetText(attempt+"");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
