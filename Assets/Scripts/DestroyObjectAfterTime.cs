using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
    /// disable after x seconds
    [SerializeField] private float destroyAfter;
    [SerializeField] private float timer;
    [SerializeField] private bool shrinkItem;

    private void OnEnable()
    {
        timer = destroyAfter;
    }
    private void LateUpdate()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            if(shrinkItem)
            {
                transform.localScale -= Vector3.one * Time.deltaTime;
                if(transform.localScale.x < 0) Destroy(this.gameObject);
            }
            else Destroy(this.gameObject);
        }
    }
}
