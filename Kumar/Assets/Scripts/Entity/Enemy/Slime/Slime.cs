using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Slime : Enemy
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
    public override float MoveSpeed { get; set; }
    private bool isFacingRight = false;
    float movingDirection = -1;
    // Attack
    /// <summary>
    /// Controls how often the player can attack
    /// </summary>
    public override float AttackSpeed { get; set; }
    /// <summary>
    /// the value of damaging an entity
    /// </summary>
    public override int Attack { get; set; }
    [SerializeField] private float sightRange = 2f;

    // TODO: Format it into a class
    public enum State { Initialize, Idle, Attack };

    State state = new State();

    [Header("Refs.")]
    /// <summary>
    /// Player's Animator which controls all animation clips
    /// </summary>
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform detectionRay;

    private void Awake()
    {
        // init values
        Health = 20;
        HealingSpeed = 2.0f;
        CannotHealing = false;
        IsDead = false;
        MoveSpeed = 2.5f;
        movingDirection = gameObject.transform.localScale.x;
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        StartCoroutine(Move());
    }

    /// <summary>
    /// healing the player
    /// </summary>
    /// <param name="value">the healing amount</param>
    public override void Healing(short value) { }

    /// <summary>
    /// damage the player and play the animation
    /// </summary>
    /// <param name="value">the damage amount</param>
    public override void Damage(short value) { }
    /// <summary>
    /// let the player dead, and 
    /// disable player's sprite.
    /// </summary>
    public override IEnumerator Dead() { yield return null; }

    /// <summary>
    /// Move the player when user inputed
    /// </summary>
    public override IEnumerator Move()
    {
        switch (state)
        {
            case State.Idle:
                Debug.Log("@Idle");
                StartCoroutine(SeekPlayer());
                MoveSpeed = 2.5f;
                break;
            case State.Attack:
                AttackEntity();
                MoveSpeed = 4;
                break;
            default:
                break;
        }
        yield return null;
    }

    private IEnumerator SeekPlayer()
    {
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
        gameObject.transform.Translate(new Vector3(
            movingDirection * MoveSpeed * Time.deltaTime, 0, 0));

        RaycastHit2D hit = Physics2D.Raycast(detectionRay.position, new Vector3(movingDirection, 0, 0), sightRange, enemyLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(movingDirection, 0, 0)) * sightRange, Color.red);
        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("Find a " + hit.collider.name + " " + "& Speed up");
            state = State.Attack;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        yield return null;
    }
    /// <summary>
    /// Attack surrounding entities
    /// </summary>
    public override void AttackEntity()
    {
        gameObject.transform.Translate(new Vector3(
            movingDirection * MoveSpeed * Time.deltaTime, 0, 0));
    }
}
