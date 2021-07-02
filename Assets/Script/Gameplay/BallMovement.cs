using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] _startPositions;
    [SerializeField] private float _minVx = 1;
    [SerializeField] private float _dVx = 3;
    [SerializeField] private float _time = 2;
    [SerializeField] private ParticleSystem _perfectEffect;
    [SerializeField] private float _timeInAir = 3;
    [SerializeField] private int _score = 2;
    [SerializeField] private int _perfectScore = 3;

    public GameObject LookAtGO;
    public delegate void BestVelocityEventHandler(float BestVelocity);
    public event BestVelocityEventHandler BestVelocityEvent;
    public delegate void OnLaunchHandler(bool isLaunch);
    public event OnLaunchHandler OnLaunchEvent;
    public delegate void PointEventHandler(int point);
    public event PointEventHandler PointEvent;
    private Rigidbody _rb;
    private int _point;
    private Vector3 _distanceFromRing;
    private float _bestVelocityY;
    private float _bestVelocityX;
    private bool _isMakeBasket;
    private bool _isLaunch;
    private int _pointIncrement;
    public float MinVx {get {return _minVx;}}
    public float DVx {get {return _dVx;}}
    public int PointIncrement {get {return _pointIncrement;} set{_pointIncrement = value;}}

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();

        _minVx = GameManager.instance.MinVx;
        _dVx = GameManager.instance.MaxVx - _minVx;
        _time = GameManager.instance.TimeToBasket;
        
        for(int i=0; i<_startPositions.Length; i++)
        {
            _startPositions[i].y = LookAtGO.transform.position.y - GameManager.instance.DistY;
        }
        _pointIncrement=1;
        RestartLaunch();
        _isLaunch = false; 
    }

    public void StartLaunch(Vector3 launchForce)
    {
        Vector3 force = new Vector3(0, _bestVelocityY, _minVx + (_dVx)*launchForce.y);

        _rb.freezeRotation=false;
        _rb.AddTorque(new Vector3(-1f, 0.0f, 0.0f));
        _rb.useGravity = true;
        _rb.AddRelativeForce(force, ForceMode.VelocityChange);
        StartCoroutine("OnLaunch");
    }

    private IEnumerator OnLaunch()
    {
        _isLaunch = true;
        OnLaunchEvent?.Invoke(_isLaunch);
        yield return new WaitForSeconds(_timeInAir);
        _isLaunch = false;
        if(!_isMakeBasket)
        {
            PointEvent?.Invoke(0);
        }
        
        RestartLaunch();
        OnLaunchEvent?.Invoke(_isLaunch);
    }

    void CalculateBestVelocity()
    {
        _distanceFromRing = LookAtGO.transform.position - transform.position;
    
        Vector2 d = new Vector2(_distanceFromRing.x, _distanceFromRing.z);
        
        float m = d.magnitude;
        float vx = m / _time;
        float a = (vx / m);

        float b = (_distanceFromRing.y - 0.5f*Physics.gravity.y*Mathf.Pow(m/vx, 2));
        
        float vy = a*b;

        _bestVelocityY = vy;
        _bestVelocityX = vx;

        BestVelocityEvent?.Invoke((_bestVelocityX - _minVx)/_dVx);
    }

    void RestartLaunch()
    {
        FreezeBall();
        _isMakeBasket = false;
        transform.position = _startPositions[Random.Range(0, 4)];
        LookPoint();
        _point = _perfectScore;
        CalculateBestVelocity();
        
    }

    void FreezeBall()
    {

        _rb.velocity = Vector3.zero;
        _rb.freezeRotation = true;
        _rb.useGravity = false;
    }

    void LookPoint()
    {

        Quaternion q = new Quaternion(0,0,0, 0);
        transform.localRotation = q;

        Vector3 ball = transform.position;
        Vector3 asta = LookAtGO.transform.position;

        ball.y = 0;
        asta.y = 0;

        Quaternion orientation = Quaternion.LookRotation(asta - ball);

        transform.rotation = orientation;

    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "basket")
        {  
            _point = _score;
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if(other.tag == "trigger")
        {
            _isMakeBasket = true;
      
            //_perfectEffect.Play();
            PointEvent?.Invoke(_point * _pointIncrement);
        }  

    }

    public void RegisterBestVelocityEvent(BestVelocityEventHandler handler)
    {
        BestVelocityEvent += handler;
    }

    public void RegisterPointEvent(PointEventHandler handler)
    {
        PointEvent += handler;
    }
    public void RegisterOnLaunchEvent(OnLaunchHandler handler)
    {
        OnLaunchEvent += handler;
    }

    public void UnRegisterOnLaunchEvent(OnLaunchHandler handler)
    {
        OnLaunchEvent -= handler;
    }

}
