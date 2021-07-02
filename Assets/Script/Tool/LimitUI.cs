using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitUI : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    [SerializeField] private float fillAmount;
    private RectTransform position;
    public float FillAmount {get {return fillAmount;} set{fillAmount = value;}}

    void Start()
    {
        position = gameObject.GetComponentInParent<RectTransform>();
        
    }

    public void OnChangeFillAmount(float fill)
    {
        transform.localPosition = new Vector3(0, 700*fill - 350, 0);
    }
}
