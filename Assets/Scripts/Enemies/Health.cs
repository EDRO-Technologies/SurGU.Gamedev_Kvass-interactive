using System;
using UnityEngine;

[Serializable]
public class Health
{
    public event Action Died;
    public event Action<int> HealthChanged;

    public int MaxAmount { get; private set; }
    public int CurrentAmount { get; private set; }

    public Health(int maxHealth)
    {
        MaxAmount = maxHealth;
        CurrentAmount = MaxAmount;
    }

    public void TakeDamage(int damage)
    {
        CurrentAmount -= damage;

        if (CurrentAmount <= 0)
            Died?.Invoke();
        
        HealthChanged?.Invoke(CurrentAmount);
    }

    public void Heal(int amount)
    {
        if (CurrentAmount + amount > MaxAmount)
            CurrentAmount = MaxAmount;
        else
            CurrentAmount += amount;

        HealthChanged?.Invoke(CurrentAmount);
    }
}