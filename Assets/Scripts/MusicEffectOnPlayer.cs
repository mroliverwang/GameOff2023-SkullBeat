using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicEffectOnPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public float[] clipSamples;

    public int arraySize;
    public float refValue  = 0.1f; // RMS value for 0 dB
    public float rmsValue;   // sound level - RMS
    public float oldRms;
    public float dbValue;    // sound level - dB
    public float volume; // set how much the scale will vary

    public float updateTime;

    private float _maxSize;
    private float _minSize;

    private Vector3 _initialScale;


    void Awake()
    {
        _maxSize = 5f;
        _minSize = 1f;

        arraySize = 1024;
        clipSamples = new float[arraySize];
        
        refValue = 0.1f;

        rmsValue = 0;
        dbValue = 0; 
        volume = 10f;

        oldRms = 0;

        updateTime = 0.1f;

        _initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        transform.localScale = new Vector3(1, 1, 1);

        updateTime -= Time.fixedDeltaTime;

        oldRms = rmsValue;

        //if (updateTime < 0)
        //{
            GetMusicData();

        //transform.localScale *= 1 + Mathf.Abs(dbValue / -20);
        //transform.localScale *= 1+ Mathf.RoundToInt( rmsValue*10);


        Debug.Log(Mathf.Abs(rmsValue));

        if (Mathf.Abs(oldRms - rmsValue) > 0.05f){

            if (rmsValue < 0.1)
            {
                transform.DOScale(_initialScale *= 1 - (rmsValue * 10), 0.5f);
            }
            else
            {
                transform.DOScale(_initialScale *= 1 + (rmsValue * 10), 0.5f);
            }           
        }

            updateTime = 0.1f;
        //}
    }


    private void GetMusicData()
    {
        audioSource.GetOutputData(clipSamples, 0); // fill array with samples
        float sum = 0f;
        for (int i = 0; i < arraySize; i++)
        {
            sum += clipSamples[i]*clipSamples[i]; // sum squared samples*_
        }
        rmsValue = Mathf.Sqrt(sum / arraySize); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        //dbValue = Mathf.Clamp(dbValue, _minSize, _maxSize);

        //Debug.Log(dbValue);
    }
}

