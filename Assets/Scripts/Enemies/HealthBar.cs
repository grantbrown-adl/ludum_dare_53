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
    [SerializeField] float _goldValue;

    public float CurrentHealth { get => _currentHealth; private set => _currentHealth = value; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _enemyScript = GetComponent<EnemyScript>();
        _simpleFlash = GetComponent<SimpleFlash>();
    }

    private void Update()
    {
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
        else
        {
            _simpleFlash.Flash();
            //_enemyScript.StopMovement(_enemyScript.Duration);
        }
    }

    private void Die()
    {
        _currentHealth = _maxHealth;
        _enemyScript.ClearWaypoints();
        ResourceManager.Instance.UpdateGold(_goldValue);
        ObjectPoolScript.ReturnInstance(this.gameObject);
    }
}
