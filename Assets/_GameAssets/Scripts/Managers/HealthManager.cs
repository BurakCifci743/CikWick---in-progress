using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private PlayerHealth_UI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

// Event: Player health reaches zero
    public event Action OnHealthZero;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();

            if (_currentHealth <= 0)
            {
                OnHealthZero?.Invoke();

            }
        }
    }
    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
        }
    }




}
