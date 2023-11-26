using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicEffectOnPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private float[] clipSamples;
    private float[] freqband;
    private float[] bandBuffer;
    private float[] decrease;

    public float[] freqbandHigh;
    public float[] audioband;
    public float[] audiobandbuffer;

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

    [SerializeField]
    private bool _isScaling;
    [SerializeField]
    private bool _ableToScale;

    private int _freqChannel;
    private float _minDiff;

    
    private float _maxJump;
    private float _minSpeed;

    private Rigidbody2D _rb;

    void Awake()
    {
        _maxSize = 3.4f;
        _minSize = 1f;
        updateTime = 0.1f;


        _initialScale = transform.localScale;
        arraySize = 512;
        clipSamples = new float[arraySize];
        freqband = new float[8];
        bandBuffer = new float[8];
        decrease = new float[8];
        freqbandHigh = new float[8];
        audioband = new float[8];
        audiobandbuffer = new float[8];

        _isScaling = false;
        _ableToScale = true;

        _freqChannel = 1;
        _minDiff = 0.8f;

        _maxJump = 34f;
        _minSpeed = 6f;

        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //if( _isScaling &&  _ableToScale)
        //{

        //_ableToScale = false;
        /*transform.DOScale(new Vector3(Mathf.Min(audiobandbuffer[_freqChannel] * 3.5f + 1f, _maxSize),
        Mathf.Min(audiobandbuffer[_freqChannel] * 4f + 1f, _maxSize), transform.localScale.z), 0.4f)
            .OnPlay(() =>
            {
                //_ableToScale = false;
                //_isScaling = false;


            })               
            .OnComplete(() =>
            {
                /*
                transform.DOScale(Vector3.one, 1f)
                .OnComplete(()=>
                {
                    _isScaling = false;
                    _ableToScale = true;

                });
            });*/

        if (audioSource.isPlaying) {


            GetMusicData();
            MakeFrequencyBands();
            BandBuffer();
            CreateAudioBands();



            transform.DOScale(new Vector3(Mathf.Min(audiobandbuffer[_freqChannel] * 3.5f + 1f, _maxSize),
               Mathf.Min(audiobandbuffer[_freqChannel] * 4f + 1f, _maxSize), transform.localScale.z), 0.2f);

            //change mass, speed, jumpPower
            _rb.mass = 1 * transform.localScale.x;

            GetComponent<PlayerControl>()._jumpPower = Mathf.Min(GetComponent<PlayerControl>().initialJumpPower * (1 + audiobandbuffer[_freqChannel] * 3.5f ),
                _maxJump);


            GetComponent<PlayerControl>()._speed = Mathf.Max(GetComponent<PlayerControl>().initialSpeed * (1 - audiobandbuffer[_freqChannel] * 3.5f ),
                _minSpeed);

            
        }
    }

    private void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if(freqband [i] > freqbandHigh[i])
            {
                freqbandHigh[i] = freqband[i];
            }
            audioband[i] = (freqband[i] / freqbandHigh[i]);
            audiobandbuffer[i] = (bandBuffer[i] / freqbandHigh[i]);
        }
    }

    private void GetMusicData()
    {
        /*audioSource.GetOutputData(clipSamples, 0); // fill array with samples
        float sum = 0f;
        for (int i = 0; i < arraySize; i++)
        {
            sum += clipSamples[i]*clipSamples[i]; // sum squared samples*_
        }
        rmsValue = Mathf.Sqrt(sum / arraySize); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        //dbValue = Mathf.Clamp(dbValue, _minSize, _maxSize);

        //Debug.Log(dbValue);*/

        audioSource.GetSpectrumData(clipSamples, 0, FFTWindow.Blackman);


    }


    private void MakeFrequencyBands()
    {
        int count = 0;

        for(int i = 0; i<8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j<sampleCount; j++)
            {
                average += clipSamples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqband[i] = average * 10;
        }
    }

    private void BandBuffer()
    {
        for(int g = 0; g < 8; ++g)
        {
            if (freqband[g] > bandBuffer[g] && Mathf.Abs(freqband[g] - bandBuffer[g]) > _minDiff) {
                bandBuffer[g] = freqband[g];
                decrease[g] = 0.0005f;
                
            }
            else
            {
                if (bandBuffer[g] - decrease[g] > 0)
                {
                    bandBuffer[g] -= decrease[g];

                    decrease[g] *= 1.1f;
                }
                //bandBuffer[g] = 0;
            }

        }


        /*
        //&& freqband[_freqChannel]>1f
        if (Mathf.Abs(freqband[_freqChannel] - bandBuffer[_freqChannel]) > 0.5f )
        {
            bandBuffer[_freqChannel] = freqband[_freqChannel];
            //decrease[_freqChannel] = 0.0005f;
            _isScaling = true;
        }
        else
        {
            //bandBuffer[g] -= decrease[g];
            //decrease[g] *= 1.1f;
            bandBuffer[_freqChannel] = 0;
        }*/
    }
}

