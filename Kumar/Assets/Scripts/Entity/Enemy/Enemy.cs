using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity, IMovable, IAttackable
{
    // Health & Death
    /// <summary>
    /// The health of the player,
    /// player will die when health < 0;
    /// </summary>
    public abstract override short Health { get; set; }
    /// <summary>
    /// Control the healing speed of the player
    /// higher means faster.
    /// </summary>
    public abstract override float HealingSpeed { get; set; }
    /// <summary>
    /// Player cannot be healed when it's true
    /// </summary>
    public abstract override bool CannotHealing { get; set; }
    /// <summary>
    /// Player lost the control
    /// </summary>
    public abstract override bool IsDead { get; set; }
    // Movement
    /// <summary>
    /// Horizontal move speed
    /// </summary>
    public abstract float MoveSpeed { get; set; }
    // Attack
    /// <summary>
    /// Controls how often the player can attack
    /// </summary>
    public abstract float AttackSpeed { get; set; }
    /// <summary>
    /// the value of damaging an entity
    /// </summary>
    public abstract int Attack { get; set; }

    /// <summary>
    /// healing the player
    /// </summary>
    /// <param name="value">the healing amount</param>
    public override abstract void Healing(short value);

    /// <summary>
    /// damage the player and play the animation
    /// </summary>
    /// <param name="value">the damage amount</param>
    public override abstract void Damage(short value);

    /// <summary>
    /// let the player dead, and 
    /// disable player's sprite.
    /// </summary>
    public override abstract IEnumerator Dead();

    /// <summary>
    /// Move the player when user inputed
    /// </summary>
    public abstract IEnumerator Move();

    /// <summary>
    /// Attack surrounding entities
    /// </summary>
    public abstract void AttackEntity();
}
