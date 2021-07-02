using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float distanceFromBall = -1;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _backBoard;
    [SerializeField] private float _minDistanceFromBackboard = 12f;
    
    private bool isSetStart;
    
    void Start()
    {      
        GameManager.instance.RegisterGameEvent(GameStart);
        _ball.GetComponent<BallMovement>().RegisterOnLaunchEvent(SetStart);
        SetPositionAndOrientation();
        isSetStart = false;
    }

    void GameStart(bool start)
    {
        SetPositionAndOrientation();
    }

    void SetStart(bool start)
    {
        isSetStart = start;
        SetPositionAndOrientation();
    }

    void SetPositionAndOrientation()
    {
        
        transform.position = _ball.transform.TransformPoint(new Vector3(0, 0, distanceFromBall));
        transform.LookAt(_backBoard.transform);
        transform.LookAt(_ball.transform);
        velocity = (_backBoard.transform.position - transform.position).magnitude - _minDistanceFromBackboard / time;

    }
    private float velocity;
    [SerializeField] private float time = 1;
    void Update()
    {
        
        if(isSetStart)
        {
            Vector3 direction = _backBoard.transform.position - transform.position;

            if(direction.magnitude > _minDistanceFromBackboard)
            {
                direction.y = 0;
                transform.localPosition += (new Vector3(0, 0.5f, 0) + (direction / direction.magnitude) * velocity) * Time.deltaTime;
            }
        }

    }

    void OnDestroy()
    {
        GameManager.instance.UnRegisterGameEvent(GameStart);
    }

}
