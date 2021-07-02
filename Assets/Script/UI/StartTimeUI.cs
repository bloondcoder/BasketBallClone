using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimeUI : MonoBehaviour
{
    private Text time;
    // Start is called before the first frame update
    void Start()
    {
        time = gameObject.GetComponent<Text>();
        GameManager.instance.RegisterStartTimeEvent(OnTimeChange);
        GameManager.instance.RegisterGameEvent(OnGameChange);
    }

    // Update is called once per frame
    void OnTimeChange(int residualTime)
    {
        if(residualTime == 0)
        {
            time.text = "START";
        }else
        {
            time.text = residualTime.ToString();
        }
    } 

    void OnGameChange(bool isGame)
    {
        if(isGame)
        {
            time.text = "";
        }else
        {
            time.text = "FINISH";
        }
    }

    void OnDestroy()
    {
        GameManager.instance.UnRegisterStartTimeEvent(OnTimeChange);
        GameManager.instance.UnRegisterGameEvent(OnGameChange);
    }
}
