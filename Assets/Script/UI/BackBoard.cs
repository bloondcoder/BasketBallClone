using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoard : MonoBehaviour
{
    [SerializeField] private int _specialPoint = 6;
    [SerializeField] private GameObject sparkleEffect;
    private int _activeSpecialPoint;
    public delegate void PointEventHandler(int point);
    public event PointEventHandler PointEvent;
    private BallMovement _ballMovement;
    
    void Start()
    {
        _ballMovement = GameObject.Find("BallPlayer").GetComponent<BallMovement>();
        _ballMovement.RegisterOnLaunchEvent(OnSetActiveChange);
        sparkleEffect.SetActive(false);
    }

    void OnDestroy()
    {
        _ballMovement.RegisterOnLaunchEvent(OnSetActiveChange);
    }

    public void OnSetActiveChange(bool isSetActive)
    {   
        if(isSetActive)
        {
            _activeSpecialPoint = Random.Range(0, 2);
            if(_activeSpecialPoint == 1)
            {
                sparkleEffect.SetActive(true);
            }
            
        }else
        {
            sparkleEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    public void AddSpecialPoint()
    {
        if(_activeSpecialPoint == 1)
        {
            PointEvent?.Invoke(Random.Range(3, _specialPoint+1));
            _activeSpecialPoint = 0;
            sparkleEffect.SetActive(false);
        }
    }

    public void RegisterPointEvent(PointEventHandler handler)
    {
        PointEvent += handler;
    }
}
