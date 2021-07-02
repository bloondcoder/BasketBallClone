using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimeSound : MonoBehaviour
{
    [SerializeField] AudioClip _timeC;
    [SerializeField] AudioClip _startGameC;
    [SerializeField] AudioClip _finalGameC;
    [SerializeField] AudioClip _finishTimeC;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _volume = 0.3f;
    [Range(0, 10)]
    [SerializeField] int _finishTimeLimit = 3;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        GameManager.instance.RegisterStartTimeEvent(OnTimeStartChange);
        GameManager.instance.RegisterGameEvent(OnGameChange);
        GameManager.instance.RegisterTimeEvent(OnTimeChange);
    }

    // Update is called once per frame
    void OnTimeStartChange(int residualTime)
    {
        if(residualTime == 0)
        {
            _audioSource?.PlayOneShot(_startGameC, _volume);
        }else
        {
            _audioSource?.PlayOneShot(_timeC, _volume);
        }
    } 

    void OnGameChange(bool isGame)
    {
        if(!isGame)
        {
            _audioSource?.PlayOneShot(_finalGameC, _volume);
        }
    }

    void OnTimeChange(float time)
    {
        if(time <= _finishTimeLimit)
        {
            _audioSource?.PlayOneShot(_finishTimeC, _volume);
        }
    }

    void OnDestroy()
    {
        GameManager.instance.UnRegisterStartTimeEvent(OnTimeStartChange);
        GameManager.instance.UnRegisterGameEvent(OnGameChange);
        GameManager.instance.UnRegisterTimeEvent(OnTimeChange);
    }

}
