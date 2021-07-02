using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarUI : MonoBehaviour
{

    [SerializeField] private Image _loadingBar;
    [SerializeField] private LimitUI _perfectLoading;
    private float height;

    void OnEnable()
    {
        GameObject ball = GameObject.Find("BallPlayer");
        ball.GetComponent<BallMovement>().RegisterBestVelocityEvent(SetPerfectScore);
        ball.GetComponent<Controller>().RegisterOnDeltaPositionChangeEvent(SetPower);
    }

    void Start()
    {
        height = GetComponent<RectTransform>().rect.height;
        
    }

    //perfectScore in percentage
    void SetPerfectScore(float perfectScore)
    {
        _perfectLoading.OnChangeFillAmount(perfectScore);
    }

    void SetPower(float power)
    {
        _loadingBar.fillAmount = power;
    }

}
