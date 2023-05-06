using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] StatusBar _healthBar;
    [SerializeField] float _currentHealth;
    [SerializeField] float _maxHealth;
    [SerializeField] float _initialMaxHealth;
    [SerializeField] EnemyScript _enemyScript;
    [SerializeField] SimpleFlash _simpleFlash;

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public SimpleFlash SimpleFlash { get => _simpleFlash; set => _simpleFlash = value; }

    private void Awake()
    {
        //_currentHealth = _maxHealth = _initialMaxHealth;
        _enemyScript = GetComponent<EnemyScript>();
        _simpleFlash = GetComponent<SimpleFlash>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth = _initialMaxHealth * ScoreManager.Instance.CurrentHealthMod;
    }

    private void Update()
    {
        _healthBar.SetStatusBarState(_currentHealth, _maxHealth);
    }

    public void ReceiveDamage(float damageValue)
    {
        DamagePopup.CreateFloat(transform.position, damageValue, false, _enemyScript.SpriteRenderer.flipX ? -1 : 1, "black");  

        _currentHealth -= damageValue;
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        else
        {
            if(gameObject.activeInHierarchy) _simpleFlash.Flash();
            //_enemyScript.StopMovement(_enemyScript.Duration);
        }
    }

    private void Die()
    {
        int random = Random.Range(0, 100);
        if(random > 95) SoundManager.Instance.PlayClip(SoundManager.Instance.Death);
        _simpleFlash.ResetMaterial();
        _currentHealth = _maxHealth;
        _enemyScript.CleanupObject();
        DamagePopup.CreateText(new Vector2(-15.0f,10.0f ), $"+{_enemyScript.GoldValue.ToString("n2")}", false, -1, "yellow");
        ResourceManager.Instance.UpdateGold(_enemyScript.GoldValue);
        ScoreManager.Instance.IncrementScore((int)MaxHealth);
        ObjectPoolScript.ReturnInstance(this.gameObject);
    }
}
