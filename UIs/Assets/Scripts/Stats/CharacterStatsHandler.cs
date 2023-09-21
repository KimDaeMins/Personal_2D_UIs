using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats _baseStats;
    public string _description;
    public CharacterStats CurrentStats { get; private set; }
    public List<Stats> _statsModifiers = new List<Stats>();

    private void Awake()
    {
        InitSetting();
    }
    public void AddStatModifier(Stats statModifier)
    {
        _statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }
    public void RemoveStatModifier(Stats statModifier)
    {
        _statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        BaseSetting();

        for(int i = 0; i < _statsModifiers.Count; i++)
        {
            UpdateStats((a , b) => a + b , _statsModifiers[i]);
        }
    }

    private void InitSetting()
    {
        CurrentStats = new CharacterStats();
        CurrentStats._name = _baseStats._name;
        CurrentStats._job = _baseStats._job;
        CurrentStats._gold = _baseStats._gold;
        CurrentStats._level = _baseStats._level;
        CurrentStats._exp = _baseStats._exp;
        CurrentStats._expMax = _baseStats._expMax;
        BaseSetting();
    }
    private void BaseSetting()
    {
        CurrentStats._hp = _baseStats._hp;
        CurrentStats._def = _baseStats._def;
        CurrentStats._att = _baseStats._att;
        CurrentStats._critical = _baseStats._critical;
    }
    private void UpdateStats(Func<float, float, float> operation, Stats newModifier)
    {
        CurrentStats._hp = operation(CurrentStats._hp , newModifier._hp);
        CurrentStats._att = operation(CurrentStats._att , newModifier._att);
        CurrentStats._def = operation(CurrentStats._def , newModifier._def);
        CurrentStats._critical = operation(CurrentStats._critical , newModifier._critical);
    }
}
