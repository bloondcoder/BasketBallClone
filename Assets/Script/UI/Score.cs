using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _totScore;
    private Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        _totScore = 0;
        GameObject.Find("BallEnemy").GetComponent<BallMovement>().RegisterPointEvent(SetScore);
    }

    // Update is called once per frame
    void SetScore(int score)
    {
        _totScore += score;
        text.text = "Score: " + _totScore;
        GameManager.instance.EnemyFinalScore = _totScore;
    }
}
