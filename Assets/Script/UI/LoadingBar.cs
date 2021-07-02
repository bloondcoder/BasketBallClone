using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private float _perfectPoint;
    private Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();


    }

    void UpdatePowerBar(float currentLoad)
    {   
        //text.text = _minVx + _dVx*currentLoad + "/" + _perfectPoint; 
    }

    void UpdatePerfectPoint(float perfectPoint)
    {
        _perfectPoint = perfectPoint;
    }
}
