using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text _timeUI;
    void Start()
    {
        _timeUI = gameObject.GetComponent<Text>();
        GameManager.instance.RegisterTimeEvent(SetTime);
    }

    void SetTime(float time)
    {
        _timeUI.text = "Time: " + time;
    }

    void OnDestroy()
    {
        GameManager.instance.UnRegisterTimeEvent(SetTime);
    }
}
