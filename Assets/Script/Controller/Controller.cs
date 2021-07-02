using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Tooltip("Set time between click and start launch.")]
    [SerializeField] private float _loadingTime = 1;
    [Tooltip("Set if the controller is used for Enemy")]
    [SerializeField] private bool isEnemy;
    private BallMovement ballMovement;
    private bool _isLoading;
    private bool _isBallinAir;
    private bool _isGameStart;
    private IControllerInterface _input;
    public delegate void OnDeltaPositionChangeEventHandler(float BestVelocity);
    public event OnDeltaPositionChangeEventHandler OnDeltaPositionChangeEvent;

    // Start is called before the first frame update
    void Start()
    {
        ballMovement = gameObject.GetComponent<BallMovement>();
        ballMovement.RegisterOnLaunchEvent(OnBallinAirChange);
        GameManager.instance.RegisterGameEvent(SetGameStart);
        _isLoading = false;
        _isBallinAir = false;
        _isGameStart = false;

        if(isEnemy)
        {
            _input = gameObject.GetComponent<EnemyController>();
            ((EnemyController)_input).LoadingTime = _loadingTime;
        }else
        {
            _input = GameManager.instance.Input;
        }    
    }
    void Update()
    {
        if(_isGameStart)
        {
            if(_isBallinAir)
            {
                _input.SpecialBonus(CatchBackBoardWithRay);   
            }else
            {
                if(_isLoading)
                {
                    float delta = _input.LoadingLaunch();
                    if(delta > 0)
                    {
                        OnDeltaPositionChangeEvent?.Invoke(delta);
                    }else if(delta < 0)
                    {
                        StopCoroutine("OnLoading");
                        ballMovement.StartLaunch(_input.StartLaunch());
                        _isLoading = false;
                    }
                }else
                {
                    if(_input.StartLoading())
                    {
                        StartCoroutine("OnLoading");
                    }
                }
            }
        }else
        {
            if(Input.GetKeyDown("space"))
            {
                GameManager.instance.GameStart();
            }
        }

    }

    IEnumerator OnLoading()
    {
        _isLoading = true;
        yield return new WaitForSeconds(_loadingTime);
        ballMovement.StartLaunch(_input.StartLaunch());
        _isLoading = false;
    }
    void CatchBackBoardWithRay(Vector3 pointPosition)
    {   

        LayerMask layerMask = LayerMask.GetMask("backboard");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pointPosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask.value)) 
        {
            hit.collider.gameObject.GetComponent<BackBoard>().AddSpecialPoint();
        }
    
    }

    public void SetGameStart(bool isGameStart)
    {
        _isGameStart = isGameStart;
    }
    public void OnBallinAirChange(bool isBallinAir)
    {
        _isBallinAir = isBallinAir;
    }

    public void RegisterOnDeltaPositionChangeEvent(OnDeltaPositionChangeEventHandler handler)
    {
        OnDeltaPositionChangeEvent += handler;
    }

    void OnDestroy()
    {
        ballMovement.UnRegisterOnLaunchEvent(OnBallinAirChange);
        GameManager.instance.UnRegisterGameEvent(SetGameStart);
    }

}
