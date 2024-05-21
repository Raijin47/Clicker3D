using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private int _currentStage = 1;
    [SerializeField] private double _baseHealth = 1000;
    [SerializeField] private double _degreeIncreaseHealth = 4;
    private double _currentHealth;
    private double _maxHealth;

    private void Start()
    {
        //симул€ци€ чрезмерно большой офлайн награды
        CurrentHealth += 1e34;
    }

    public double CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth >= _maxHealth)
            {                
                while (_currentHealth >= _maxHealth)
                {
                    _currentStage++;
                    _currentHealth -= _maxHealth;
                    UpdateMaxHealth();
                }
            }
        }
    }

    private void UpdateMaxHealth()
    {
        _maxHealth = Calculate(_currentStage, _baseHealth, _degreeIncreaseHealth);
    }

    public double Calculate(double level, double baseValue, double degree)
    {
        level += 1;

        double value = baseValue * Math.Pow(level, degree);
        value = Math.Round(value);
        return value;
    }
}
