using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerController.cs controls player's movements
/// include idle, run, attack, parry etc.
/// 
/// Author: Rui Jiang
/// Version: 0.0.1
/// </summary>

/// <summary>
/// Required components
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : Entity, IMovable, IAttackable
{
    // Health & Death
    /// <summary>
    /// The health of the player,
    /// player will die when health < 0;
    /// </summary>
    public override short Health { get; set; }
    /// <summary>
    /// Control the healing speed of the player
    /// higher means faster.
    /// </summary>
    public override float HealingSpeed { get; set; }
    /// <summary>
    /// Player cannot be healed when it's true
    /// </summary>
    public override bool CannotHealing { get; set; }
    /// <summary>
    /// Player lost the control
    /// </summary>
    public override bool IsDead { get; set; }
    // Movement
    /// <summary>
    /// Horizontal move speed
    /// </summary>
    public float MoveSpeed { get; set; }
    private bool isFacingRight = true;
    /// <summary>
    /// Vertical move speed
    /// </summary>
    public float JumpSpeed { get; set; }
    // Attack
    /// <summary>
    /// Controls how often the player can attack
    /// </summary>
    public float AttackSpeed { get; set; }
    /// <summary>
    /// the value of damaging an entity
    /// </summary>
    public int Attack { get; set; }

    [Header("Refs.")]
    /// <summary>
    /// Player's Animator which controls all animation clips
    /// </summary>
    [SerializeField] private Animator animator;

    private void Awake()
    {
        // init values
        Health = 20;
        HealingSpeed = 2.0f;
        CannotHealing = false;
        IsDead = false;
        MoveSpeed = 2.5f;
        JumpSpeed = 3.0f;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // init refs.
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack")) AttackEntity();
        else if (Input.GetButton("Horizontal")) StartCoroutine(Move());
        else if (Input.GetButtonDown("Jump")) Jump();
        else if (Input.GetButtonDown("Parry")) Parray();
    }

    /// <summary>
    /// healing the player
    /// </summary>
    /// <param name="value">the healing amount</param>
    public override void Healing(short value)
    {
        Health += value;
    }

    /// <summary>
    /// damage the player and play the animation
    /// </summary>
    /// <param name="value">the damage amount</param>
    public override void Damage(short value)
    {
        Health -= value;
        if (Health < 0) StartCoroutine(Dead());
    }

    /// <summary>
    /// let the player dead, and 
    /// disable player's sprite.
    /// </summary>
    public override IEnumerator Dead()
    {
        IsDead = true;
        gameObject.SetActive(false);
        yield return null;
    }

    /// <summary>
    /// Move the player when user inputed
    /// </summary>
    public IEnumerator Move()
    {
        float movingDirection = Mathf.Sign(Input.GetAxis("Horizontal"));
        bool isMovingLeft = movingDirection == -1;
        bool isMovingRight = movingDirection == 1;
        if (isMovingLeft && isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
        else if (isMovingRight && !isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }
        gameObject.transform.Translate(new Vector3(movingDirection * MoveSpeed * Time.deltaTime, 0, 0));
        yield return null;
    }

    void Jump() { }

    /// <summary>
    /// Attack surrounding entities
    /// </summary>
    public void AttackEntity()
    {
    }

    /// <summary>
    /// Parray the attack from all directions
    /// </summary>
    IEnumerator Parray() { yield return null; }
}
