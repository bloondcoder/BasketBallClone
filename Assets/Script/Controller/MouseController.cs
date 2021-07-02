using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : IControllerInterface
{
    private Vector3 _startPosition;
    private Vector3 _deltaPosition;
    private Vector3 _mouseOldPosition;
    private float _maxVerticalShift = 1500;
    private float _maxHorizontalShift = 400;

    public bool StartLoading()
    {
        if(Input.GetAxis("Fire1") > 0)
        {
            _startPosition = Input.mousePosition;
            _deltaPosition = Vector3.zero;
            _mouseOldPosition = Input.mousePosition;
            return true;
        }
        return false;
    }

    public float LoadingLaunch()
    {
        

        if(Input.GetMouseButtonUp(0))
        {
            return -1f;
        }

        Vector3 delta = (Input.mousePosition - _mouseOldPosition)*2;

        if(delta.y > 0){
            _deltaPosition += delta;
            _mouseOldPosition = Input.mousePosition;
            return Mathf.Clamp(_deltaPosition.y, 0, _maxVerticalShift) / _maxVerticalShift;
        }

        return 0f;    
        
    }

    public void SpecialBonus(FunctionCallInAction handler)
    {
        if(Input.GetAxis("Fire1") > 0)
        {
            handler(Input.mousePosition);
        }
        
    }

    public Vector3 StartLaunch()
    {
        
        Vector3 diff = _deltaPosition;
        diff.x = Mathf.Clamp(diff.x/4, -_maxHorizontalShift, _maxHorizontalShift) / _maxHorizontalShift;
        diff.y = Mathf.Clamp(diff.y, 0, _maxVerticalShift) / _maxVerticalShift;

        return diff;
    }

}
