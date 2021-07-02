using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System;

[Serializable] public class OnFireBallChange : UnityEvent<float> {} 
[Serializable] public class OnFireBallStart : UnityEvent<float> {} 

public class Fireball : MonoBehaviour
{
    [Tooltip("Set the limit to activate FireBall")]
    [SerializeField] private float _fireBallActivation = 1.0f;
    [Tooltip("Set the multiplier to score")]
    [SerializeField] private int _fireBallIncrement = 2;
    [Tooltip("Set time of the multiplier to score")]
    [SerializeField] private float _fireBallTime = 5;
    private TrailRenderer _trail; 
    private float _fireBallLoading;
    private bool isFireBall;
    private BallMovement _ballMovement;
    public OnFireBallChange onFireBallChange;
    public OnFireBallChange onFireBallStart;
    

    void Start()
    {
        _fireBallLoading = 0;
        isFireBall = false;
        _ballMovement = GameObject.Find("BallPlayer").GetComponent<BallMovement>();
        _trail = gameObject.GetComponent<TrailRenderer>();
        _trail.enabled = false;
        _ballMovement.RegisterPointEvent(AddFireBallLoading);
        _ballMovement.PointIncrement = 1;
    }

    public void AddFireBallLoading(int point)
    {
        if(point == 0)
        {
            StopDecrement();
        }else{
            if(!isFireBall)
            {
                
                _fireBallLoading += point * 0.12f;
                onFireBallChange?.Invoke(_fireBallLoading);
                if(_fireBallLoading >= _fireBallActivation)
                {
                    _fireBallLoading = _fireBallActivation;
                    isFireBall = true;
                    _ballMovement.PointIncrement = _fireBallIncrement;
                    StartCoroutine("Decrement");
                }
            }
        }   
    }

    public void StopDecrement()
    {
            _ballMovement.PointIncrement = 1;
            StopCoroutine("Decrement");
            _trail.enabled = false;
            isFireBall = false;
            _fireBallLoading=0;
            onFireBallChange?.Invoke(_fireBallLoading);
    }

    IEnumerator Decrement()
    {
        _trail.enabled = true;
        onFireBallStart.Invoke(_fireBallTime);
        yield return new WaitForSeconds(_fireBallTime); 
        
        isFireBall = false;
        _fireBallLoading=0;
        _ballMovement.PointIncrement = 1;
        _trail.enabled = false;
    }
}
