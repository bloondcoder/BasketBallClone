using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreUI : MonoBehaviour
{
    private Text _ballPoint;
    private Text _backBoardPoint;
    void Start()
    {
        _ballPoint = gameObject.GetComponent<Text>();
        GameObject.Find("BallPlayer").GetComponent<BallMovement>().RegisterPointEvent(OnPointChange);
        GameObject.Find("Backboard").GetComponent<BackBoard>().RegisterPointEvent(OnPointChange);
    }

    void OnPointChange(int point)
    {
        if(point != 0)
        {
            object[] parms = new object[2]{point, "ball"};
            StartCoroutine("ChangePointText", parms);
        }
    }

    IEnumerator ChangePointText(object[] parms)
    {
        int point = (int)parms[0];
        string currentText = (string)parms[1]; 
        Text text = null;
        if(currentText == "ball")
        {
            text = _ballPoint;
        }else if(currentText == "backboard")
        {
            text = _backBoardPoint;
        }
        text.text = "+" + point + "pts";
        yield return new WaitForSeconds(0.5f);
        text.text = "";
    }

}
