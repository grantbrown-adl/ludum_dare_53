using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectAfterTime : MonoBehaviour
{
    /// disable after x seconds
    [SerializeField] private float disableAfter;
    [SerializeField] private float timer;
    [SerializeField] private bool shrinkItem;
    [SerializeField] private float decreaseScaleAmount;

    private void OnEnable()
    {
        timer = disableAfter;
    }
    private void LateUpdate()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            if(shrinkItem)
            {
                transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
                if(transform.localScale.x < 0) gameObject.SetActive(false);
            }
            else { timer = 0; gameObject.SetActive(false); };
        }

        //if(timer <= 0.0f) { timer = 0; gameObject.SetActive(false); } 
    }
}
