using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IControllerInterface
{
    private Transform _ringT; 
    private float _bestVelocityY;
    private float _bestVelocityX;
    private float _timeToBasket;
    private float _minVx;
    private float _maxVx;
    private float _loadingTime = 1;
    private float _stopLoading;
    public float LoadingTime{get {return _loadingTime;} set{ _loadingTime = value;}}
    void Start()
    {
        _ringT = GameObject.Find("Ring").transform;
        _timeToBasket = GameManager.instance.TimeToBasket;
        _minVx = GameManager.instance.MinVx;
        _maxVx = GameManager.instance.MaxVx;
    }
    public bool StartLoading()
    {
        //Calculate bestVelocity for Best Score
        StopCoroutine("DuringLoad");
        CalculateBestVelocity();
        StartCoroutine("DuringLoad");
        return true;
    }

    void CalculateBestVelocity()
    {
        Vector3 _distanceFromRing = _ringT.position - transform.position;
    
        Vector2 d = new Vector2(_distanceFromRing.x, _distanceFromRing.z);
        
        float m = d.magnitude;
        float vx = m / _timeToBasket;
        float a = (vx / m);

        float b = (_distanceFromRing.y - 0.5f*Physics.gravity.y*Mathf.Pow(m/vx, 2));
        
        float vy = a*b;

        _bestVelocityY = vy;
        _bestVelocityX = vx;
    }

    public float LoadingLaunch()
    {
        return _stopLoading;
    }

    IEnumerator DuringLoad()
    {
        _stopLoading = .1f;
        yield return new WaitForSeconds(Random.Range(0.1f, _loadingTime));
        _stopLoading = -.1f;

    }
    public Vector3 StartLaunch()
    {

        return new Vector3(0, (_bestVelocityX - _minVx) / (_maxVx - _minVx));
    }
    
    public void SpecialBonus(FunctionCallInAction handler)
    {
        
    }
}
