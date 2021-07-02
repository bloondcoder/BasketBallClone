using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSound : MonoBehaviour
{

    [SerializeField] private AudioClip _launchC;
    [SerializeField] private AudioClip _backBoardC;
    [SerializeField] private AudioClip _basketC;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _volume = 0.3f;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        BallMovement ballPlayer = GameObject.Find("BallPlayer").GetComponent<BallMovement>();
        ballPlayer.RegisterOnLaunchEvent(PlayLaunchClip);
        ballPlayer.RegisterPointEvent(PlayBasketClip);
        GameObject.Find("Backboard").GetComponent<BackBoard>().RegisterPointEvent(PlayBackBoardClip);  
    }

    void PlayLaunchClip(bool isLaunch)
    {
        if(isLaunch)
        {
            _audioSource?.PlayOneShot(_launchC, _volume);
        }
        
    }

    void PlayBasketClip(int point)
    {
        if(point > 0)
        {
            _audioSource?.PlayOneShot(_basketC, _volume);
        }
        
    }

    void PlayBackBoardClip(int point)
    {
        if(point > 0 )
        {
            _audioSource?.PlayOneShot(_backBoardC, _volume);
        }
        
    }
}
