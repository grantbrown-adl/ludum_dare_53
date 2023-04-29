using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] StatusBar _healthBar;
    [SerializeField] float _currentHealth;
    [SerializeField] float _maxHealth;
    [SerializeField] EnemyScript _enemyScript;
    [SerializeField] SimpleFlash _simpleFlash;

    public float CurrentHealth { get => _currentHealth; private set => _currentHealth = value; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _enemyScript = GetComponent<EnemyScript>();
        _simpleFlash = GetComponent<SimpleFlash>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ReceiveDamage(3.0f);
        }
        _healthBar.SetStatusBarState(_currentHealth, _maxHealth);
    }

    public void ReceiveDamage(float damageValue)
    {
        DamagePopup.Create(transform.position, (int)damageValue, false, _enemyScript.SpriteRenderer.flipX ? -1 : 1, "black");  

        _currentHealth -= damageValue;
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        else _simpleFlash.Flash();
    }

    private void Die()
    {
        _currentHealth = _maxHealth;
        _enemyScript.ClearWaypoints();
        ObjectPoolScript.ReturnInstance(this.gameObject);
    }
}
