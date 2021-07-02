using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    private Text _scoreLine;
    private int _totPoint;
    void Start()
    {
        _totPoint = 0;
        _scoreLine = gameObject.GetComponent<Text>();
        GameObject.Find("BallPlayer").GetComponent<BallMovement>().RegisterPointEvent(SetScore);
        GameObject.Find("Backboard").GetComponent<BackBoard>().RegisterPointEvent(SetScore);  
    }

    void SetScore(int point)
    {
        _totPoint += point;
        //Placeholder per UI Seria
        _scoreLine.text = "Score: " + _totPoint;
        GameManager.instance.FinalScore = _totPoint;
    }

}
