using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallUI : MonoBehaviour
{
    [Tooltip("Frequency time of UI. Range from 0.01 to 1.0")]
    [Range(0.01f, 1.0f)]
    [SerializeField] private float frequency = 0.1f;
    private Image _fireBallBar;
    private bool isActive;
    
    public void Start()
    {
        _fireBallBar = gameObject.GetComponent<Image>();
        _fireBallBar.fillAmount = .0f;
        isActive = false;
    }

    public void OnFireBallChange(float increment)
    {
        if(increment == 0)
        {
            _fireBallBar.fillAmount = 0.0f;
            isActive = false;
            StopCoroutine("DecrementBar");
        }else{
            if(!isActive)
            {
                _fireBallBar.fillAmount = Mathf.Clamp(increment, 0.0f, 1.0f);
            }

        }
        
    }

    public void OnFireBallStart(float time)
    {
        isActive = true;
        StartCoroutine("DecrementBar", time);
    }

    IEnumerator DecrementBar(object value)
    {
        float time = (float)value;
        float period = time * frequency;
        float startTime = time;
        while(time >= 0)
        {
            _fireBallBar.fillAmount = time / startTime;
            time -= period; 
            yield return new WaitForSeconds(period);
        }
        _fireBallBar.fillAmount = 0.0f;
        isActive = false;
        
    }
}
