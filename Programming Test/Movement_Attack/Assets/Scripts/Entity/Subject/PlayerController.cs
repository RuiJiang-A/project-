using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player move speed
    float moveSpeed = 2.5f;
    // player current face direction
    bool isFacingRight = false;
    public Transform attackPoint;
    float attackRange = 0.5f;
    float attack = 50.0f;
    public LayerMask enemyLayer;
    float maxHealth = 150;
    bool isDead = false;
    [SerializeField] float health;

    Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead) return;
        float movingDirection = Mathf.Sign(Input.GetAxis("Horizontal"));
        bool isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        bool isParrying = animator.GetCurrentAnimatorStateInfo(0).IsName("Combat Idle");
        // Attack
        if (Input.GetButtonDown("Attack")) StartCoroutine("Attack");
        // Parry
        else if (Input.GetButtonDown("Parry")) Parry();
        // Move
        else if (Input.GetButton("Horizontal") && !isAttacking && !isParrying)
        {
            // if character is facing left and wants to face right
            if (!isFacingRight && movingDirection == 1)
            {
                isFacingRight = true;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            // if character is facing right and wants to face left
            else if (isFacingRight && movingDirection == -1)
            {
                isFacingRight = false;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            // move the character
            animator.SetInteger("AnimState", 2);
            gameObject.transform.Translate(new Vector3(movingDirection * moveSpeed * Time.deltaTime, 0, 0));
        }
        else
            animator.SetInteger("AnimState", 0);
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        Collider2D[] hitEntities = Physics2D.OverlapCircleAll(
            attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D hitEntity in hitEntities)
        {
            hitEntity.GetComponent<Enemy>().Damage(attack);
        }
    }

    void Parry()
    {
        animator.SetInteger("AnimState", 1);
    }

    public void Damage(float value)
    {
        health -= value;
        if (health <= 0)
            Die();
        animator.SetBool("Hurt", true);
    }

    void Die()
    {
        animator.SetBool("Death", true);
        isDead = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine("Disappear");
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
