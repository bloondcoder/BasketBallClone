using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : IControllerInterface
{
    private Vector2 _startPosition;
    private Vector2 _deltaPosition;
    private float _maxVerticalShift = 1500;
    private float _maxHorizontalShift = 200;

    public bool StartLoading()
    {
        if(Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startPosition = Input.GetTouch(0).position;
            _deltaPosition = Vector2.zero;
            return true;
        }
        return false;
    }

    public float LoadingLaunch()
    {   

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            return -1.0f;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.y > 0){
            _deltaPosition += Input.GetTouch(0).deltaPosition * 2;
            return Mathf.Clamp(_deltaPosition.y, 1, _maxVerticalShift) / _maxVerticalShift;
        }
        
        return 0f;    
        
    }

    public void SpecialBonus(FunctionCallInAction handler)
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            handler(Input.GetTouch(0).position);
        }
        
    }

    public Vector3 StartLaunch()
    {
        
        float xNormalized = Mathf.Clamp(_deltaPosition.x/4, -_maxHorizontalShift, _maxHorizontalShift) / _maxHorizontalShift;
        float yNormalized = Mathf.Clamp(_deltaPosition.y, 0, _maxVerticalShift) / _maxVerticalShift;

        Vector3 velocity = new Vector3(xNormalized, yNormalized, 0);


        return velocity;
    }
}
