using Assets.PixelHeroes.Scripts.CharacterScrips;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.PixelHeroes.Scripts.ExampleScripts
{
    public class EnemyAI : MonoBehaviour
    {
        public Character Character;
        public float WalkSpeed = 1f;
        public float RunSpeed = 2f;
        public float AttackRange = 1f;
        public Transform Player;

        public Transform EnemyBody;
        public float DetectionRange = 5f;
        public float AttackCooldown = 1f;

        private Animator _animator;
        private Vector2 _input;
        private Rigidbody2D r2d;
        private bool isAttacking = false;

        private string[] attackAnimations = { "Attack" };
        private bool isMoving = false;
        private bool isDead = false;

        public Slider healthSlider;
        public TMP_Text hitPoints;
        private Collider2D enemyCollider;

        public void Start()
        {


            enemyCollider = GetComponent<Collider2D>();

        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            r2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
            if (Player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

                if (distanceToPlayer <= DetectionRange)
                {
                    if (distanceToPlayer > AttackRange && !isDead)
                    {
                        MoveTowardsPlayer();
                    }
                    else if (!isAttacking)
                    {
                        StartCoroutine(AttackPlayer());
                    }
                }
                else
                {
                    Idle();
                }



                Health health;
                if (health = this.GetComponent<Health>())
                {
                    healthSlider.maxValue = health.maxHealth;
                    healthSlider.value = health.currentHealth;
                    hitPoints.text = health.currentHealth.ToString() + " HP";
                    if (health.isDead || health.currentHealth == 0)
                    {
                        isDead = true;
                        _animator.SetBool("Dead", true);
                        enemyCollider.isTrigger = true;
                        //Destroy(collider);
                        StartCoroutine(HandleDeath());

                    }
                }

            }
        }




        private void MoveTowardsPlayer()
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            r2d.velocity = direction * WalkSpeed;
            SetMovementAnimation(true);

            if (direction.x < 0)
            {
                Turn(-1);
            }
            else if (direction.x > 0)
            {
                Turn(1);
            }
        }

        private IEnumerator AttackPlayer()
        {
            isAttacking = true;
            r2d.velocity = Vector2.zero;
            SetMovementAnimation(false);

            // Choose a random attack animation
            string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
            _animator.SetTrigger(randomAttackAnimation);

            // Detect colliders to apply damage
            DetectColliders();

            // Wait for the duration of the attack animation or cooldown period
            yield return new WaitForSeconds(AttackCooldown);

            isAttacking = false;
        }

        private void Idle()
        {
            r2d.velocity = Vector2.zero;
            SetMovementAnimation(false);
        }

        private void SetMovementAnimation(bool isMoving)
        {
            this.isMoving = isMoving;
            _animator.SetBool("Idle", !isMoving);
            _animator.SetBool("Walking", isMoving);
            _animator.SetBool("Running", false);
        }

        private void Turn(int direction)
        {
            Vector3 scale = EnemyBody.localScale;
            scale.x = direction * Mathf.Abs(scale.x);
            EnemyBody.localScale = scale;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            Gizmos.DrawWireSphere(position, DetectionRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(position, AttackRange);
        }

        public void DetectColliders()
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, AttackRange))
            {
                Health health;
                if (health = collider.GetComponent<Health>())
                {
                    health.GetHit(1, transform.gameObject);
                    if (gameObject.layer != collider.gameObject.layer)
                    {
                        collider.GetComponent<Animator>().SetBool("Hit", true);
                    }
                }
            }
        }
        private IEnumerator HandleDeath()
        {
            yield return new WaitForSeconds(0.9f);
            Destroy(gameObject);
        }

    }
}
