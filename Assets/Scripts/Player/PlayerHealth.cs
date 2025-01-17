using System;
using UnityEngine;
using Player;

public class PlayerHealth: MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int Dead = Animator.StringToHash("dead");
    
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Animator _playerAnimator;

    private int _currentHealth;
    public static event Action<int> OnHealthUpdated;
    private void Start ()
    {
        _currentHealth = _playerConfig.BaseHealth;
        OnHealthUpdated?.Invoke(_currentHealth);
    }
    
    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            return;
        }

        if (_currentHealth + healAmount > _playerConfig.MaxHealth)
        {
            _currentHealth = _playerConfig.MaxHealth;
        }
        else
        {
            _currentHealth += healAmount;
        }
        OnHealthUpdated?.Invoke(_currentHealth);
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            OnHealthUpdated?.Invoke(_currentHealth);
            return;
        }

        _currentHealth -= damage;
        _playerAnimator.SetTrigger(Hit);
        
        if (_currentHealth < _playerConfig.MinHealth)
        {
            _currentHealth = 0;
            _playerAnimator.SetTrigger(Dead);
        }
        OnHealthUpdated?.Invoke(_currentHealth);
    }
}
