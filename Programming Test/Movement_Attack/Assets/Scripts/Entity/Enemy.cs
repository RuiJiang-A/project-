using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    float maxHealth = 150;
    [SerializeField] float health;
    public Transform attackPoint;
    float attackRange = 0.5f;
    float attack = 50.0f;
    public LayerMask playerLayer;
    [SerializeField] int enemyType = 0;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        if (enemyType == 1)
            StartCoroutine("Attack");
    }

    // Update is called once per frame
    void Update()
    {

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
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine("Disappear");
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            Collider2D[] hitEntities = Physics2D.OverlapCircleAll(
                attackPoint.position, attackRange, playerLayer);

            foreach (Collider2D hitEntity in hitEntities)
            {
                Animator playerAnimator = hitEntity.GetComponent<Animator>();
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Combat Idle"))
                    Damage(50);
                else
                    hitEntity.GetComponent<PlayerController>().Damage(attack);
            }
        }
    }
}
