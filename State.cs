/*  Basic state format.
*
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public float moveSpeed { get { return _moveSpeed; } }
    public int attackPower { get { return _attackPower; } }
    public float attackSpeed { get { return _attackSpeed; } }

    [SerializeField] protected int _maxHealth = 1;
    [SerializeField] protected float _moveSpeed = 1;
    [SerializeField] protected int _attackPower = 1;
    [SerializeField] protected float _attackSpeed = 1;

    protected int _health;

    public void DamageToHealth(int amount)
    {
        _health -= amount;
        if (_health < 0) { Death(); }
    }
    protected virtual void Death()
    {
        Debug.Log("Death");
    }
}