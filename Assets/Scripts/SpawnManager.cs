using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] danger;

    private float[] _coolDown;

    // Start is called before the first frame update
    void Awake()
    {
        //danger = new GameObject[8];
        _coolDown = new float[]
        {
            2.0f, 4.0f, 4.0f, 5.0f, 
            6.0f, 6.0f, 8.0f, 9.0f, 
        };
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _coolDown.Length; i++)
        {
            _coolDown[i] -= Time.deltaTime;
            if (_coolDown[i] <= 0)
            {
                Instantiate(danger[i], danger[i].transform.position, danger[i].transform.rotation);
                _coolDown[i] = 3f;
            }
        }
    }
}
