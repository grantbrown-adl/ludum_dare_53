using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAfterTime : MonoBehaviour
{    /// disable after x seconds
    [SerializeField] private float _poolAfter;
    [SerializeField] private float _timer;
    [SerializeField] private bool _shrinkItem;
    [SerializeField] private float _decreaseScaleAmount;
    [SerializeField] private bool _instantPool;
    [SerializeField] private Vector3 _currentScale;

    public float Timer { get => _timer; set => _timer = value; }
    public float PoolAfter { get => _poolAfter; set => _poolAfter = value; }
    public bool InstantPool { get => _instantPool; set => _instantPool = value; }

    private void OnEnable()
    {
        _currentScale = transform.localScale;
        _timer = 0;
        _instantPool = false;
    }
    private void LateUpdate()
    {
        _timer += Time.deltaTime;

        if((_timer >= _poolAfter && _poolAfter > 0) || _instantPool)
        {
            if(_shrinkItem)
            {
                transform.localScale -= Vector3.one * _decreaseScaleAmount * Time.deltaTime;
                if(transform.localScale.x < 0)
                {
                    ObjectPoolScript.ReturnInstance(gameObject);
                    _timer = 0;
                    transform.localScale = _currentScale;
                }
                //if(transform.localScale.x < 0) gameObject.SetActive(false);
            }
            else 
            {
                ObjectPoolScript.ReturnInstance(gameObject); 
                _timer = 0;
            };
            //else { _timer = 0; gameObject.SetActive(false); };
        }

        //if(timer <= 0.0f) { timer = 0; gameObject.SetActive(false); } 
    }
}
