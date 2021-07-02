using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleUI : MonoBehaviour
{
    [SerializeField] private string rule;
    private Text time;
    // Start is called before the first frame update
    void Start()
    {
        time = gameObject.GetComponent<Text>();
        GameManager.instance.RegisterGameEvent(OnGameChange);
        time.text = rule;
    }

    void OnGameChange(bool isGame)
    {
        if(isGame)
        {
            time.text = "";
        }else
        {
            time.text = rule;
        }
    }

    void OnDestroy()
    {
        GameManager.instance.UnRegisterGameEvent(OnGameChange);
    }
}
