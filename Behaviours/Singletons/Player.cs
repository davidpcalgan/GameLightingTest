using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player
{
    private Player() {
        _health = 100;
        _energy = 100;
        _asthma = 0;
        asthmaAttack = false;
    }
    private static Player instance = null;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
    }

    public bool asthmaAttack;
    private float _health, _energy, _asthma;
    public float health { get { return _health; } set { _health = value; _health = (_health > 100 ? 100 : _health); } }
    public float energy { get { return _energy; } set { _energy = value; _energy = (_energy > 100 ? 100 : _energy); } }
    public float asthma { get { return _asthma; } set { 
            _asthma = value;
            if (_asthma > 100)
            {
                _asthma = 100;
                asthmaAttack = true;
            }
            else if (_asthma < 50)
            {
                asthmaAttack = false;
            }
            if (_asthma < 0)
            {
                _asthma = 0;
            }
        } 
    }

    public void DamageAsthma (float amount, bool DoT = true)
    {
        asthma = _asthma + amount * (DoT ? Time.deltaTime : 1);
    }

    public void RestAsthma ()
    {
        asthma = _asthma - 10 * Time.deltaTime;
    }
}
