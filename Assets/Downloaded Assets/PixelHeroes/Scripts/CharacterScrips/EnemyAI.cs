using Assets.PixelHeroes.Scripts.CharacterScrips;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        public int Attack = 1;
        public int ExpMultiplier = 1;

        public Transform Player;
        private Transform MaterialsAndCoinsDropOff;
        public Transform EnemyBody;
        public float DetectionRange = 5f;
        public float AttackCooldown = 1f;


        public GameObject coinPrefab; // The coin prefab to instantiate
        public int numberOfCoinsToDrop = 5; // Number of coins to drop
        public int CoinValueToDrop = 1;


        public GameObject heartPrefab; // The coin prefab to instantiate
        public int numberOfHeartsToDrop = 1; // Number of coins to drop
        public int HeartValueToDrop = 1;


        public GameObject MaterialPrefab; // The coin prefab to instantiate
        public int numberOfMaterialToDrop = 1; // Number of coins to drop
        public int MaterialValueToDrop = 1;


        public float dropRadius = 1f;
        public float dropDuration = 0.5f; // Duration of the drop animation
        public Vector3 dropOffset = new Vector3(0, 2, 0); // Offset for the drop effect
        public float initialUpwardsDistance = 1f;
        public float upwardsDuration = 0.2f;

        private Animator _animator;
        private Vector2 _input;
        private Rigidbody2D r2d;
        private bool isAttacking = false;

        private string[] attackAnimations = { "Attack" };
        private bool isMoving = false;
        private bool isDead = false;
        private bool isRecovering = false;
        public float recoveryTime = 1f; // Time in seconds to recover from being hit

        public Slider healthSlider;
        public TMP_Text hitPoints;
        private Collider2D enemyCollider;
        public GameObject floatingTextPrefab; // Reference to the floating text prefab
        public Transform damageCanvas; // Reference to the Damage Canvas object

       

        public void Start()
        {

            MaterialsAndCoinsDropOff = GameObject.Find("MaterialsAndCoinsDropOff").transform;

            enemyCollider = GetComponent<Collider2D>();

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                Player = playerObj.transform;
            }

        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            r2d = GetComponent<Rigidbody2D>();
        }

        private void ShowFloatingText(int damage)
        {
            if (floatingTextPrefab != null && damageCanvas != null)
            {
                GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, damageCanvas);
                DamageText floatingTextComponent = floatingText.GetComponent<DamageText>();
                floatingTextComponent.SetText(damage.ToString(), Color.red);
            }
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


        private void DropCoins(Vector3 position)
        {
            for (int i = 0; i < numberOfCoinsToDrop; i++)
            {
                // Calculate a random position around the enemy within the drop radius
                Vector3 randomPosition = position + new Vector3(
                    UnityEngine.Random.Range(-dropRadius, dropRadius),
                    UnityEngine.Random.Range(-dropRadius, dropRadius),
                    0f);

                //Vector3 startPosition = randomPosition + dropOffset;

                // Instantiate the coin prefab at the calculated random position
                //GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity);


                //LeanTween.move(coin, randomPosition, dropDuration).setEase(LeanTweenType.easeOutBounce);
                //LeanTween.rotateZ(coin, 360, dropDuration).setEase(LeanTweenType.easeInOutCubic).setLoopClamp();

                GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity, MaterialsAndCoinsDropOff);

                Coin coinComponent = coin.GetComponent<Coin>();
                if (coinComponent != null)
                {
                    coinComponent.coinValue = CoinValueToDrop; // Set the coin value as needed
                }

                // Calculate the upwards position
                Vector3 curvedUpwardsPosition = position + new Vector3(
                Random.Range(-dropRadius, dropRadius), initialUpwardsDistance, 0);


                Vector3 finalPosition = curvedUpwardsPosition + new Vector3(0, -initialUpwardsDistance, 0);
                // Animate the coin moving up and to the side in a curve, then falling to the random position with a bounce
                LeanTween.move(coin, curvedUpwardsPosition, upwardsDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    LeanTween.move(coin, finalPosition, dropDuration).setEase(LeanTweenType.easeOutBounce);
                    LeanTween.rotateZ(coin, 360, dropDuration).setEase(LeanTweenType.easeInOutCubic).setLoopClamp();
                });

            }
        }

        private void DropHearts(Vector3 position)
        {
            for (int i = 0; i < numberOfHeartsToDrop; i++)
            {
                // Calculate a random position around the enemy within the drop radius
                Vector3 randomPosition = position + new Vector3(
                    UnityEngine.Random.Range(-dropRadius, dropRadius),
                    UnityEngine.Random.Range(-dropRadius, dropRadius),
                    0f);

                GameObject heart = Instantiate(heartPrefab, position, Quaternion.identity, MaterialsAndCoinsDropOff);

                Heart heartComponent = heart.GetComponent<Heart>();
                if (heartComponent != null)
                {
                    heartComponent.HeartValue = HeartValueToDrop; // Set the coin value as needed
                }

                // Calculate the upwards position
                Vector3 curvedUpwardsPosition = position + new Vector3(
                Random.Range(-dropRadius, dropRadius), initialUpwardsDistance, 0);


                Vector3 finalPosition = curvedUpwardsPosition + new Vector3(0, -initialUpwardsDistance, 0);
               
                LeanTween.move(heart, curvedUpwardsPosition, upwardsDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    LeanTween.move(heart, finalPosition, dropDuration).setEase(LeanTweenType.easeOutBounce);
                    LeanTween.rotateZ(heart, 360, dropDuration).setEase(LeanTweenType.easeInOutCubic).setLoopClamp();
                });

                
            }
        }


        private void DropMaterials(Vector3 position)
        {
            for (int i = 0; i < numberOfMaterialToDrop; i++)
            {
                // Calculate a random position around the enemy within the drop radius
                Vector3 randomPosition = position + new Vector3(
                    UnityEngine.Random.Range(-dropRadius, dropRadius),
                    UnityEngine.Random.Range(-dropRadius, dropRadius),
                    0f);

                GameObject material = Instantiate(MaterialPrefab, position, Quaternion.identity, MaterialsAndCoinsDropOff);

                Materials SWN = material.GetComponent<Materials>();
                if (SWN != null)
                {
                    SWN.MaterialValue = MaterialValueToDrop;
                    SWN.MaterialName = "Silicon Wafer";// Set the coin value as needed
                }

                // Calculate the upwards position
                Vector3 curvedUpwardsPosition = position + new Vector3(
                Random.Range(-dropRadius, dropRadius), initialUpwardsDistance, 0);


                Vector3 finalPosition = curvedUpwardsPosition + new Vector3(0, -initialUpwardsDistance, 0);

                LeanTween.move(material, curvedUpwardsPosition, upwardsDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    LeanTween.move(material, finalPosition, dropDuration).setEase(LeanTweenType.easeOutBounce);
                    LeanTween.rotateZ(material, 360, dropDuration).setEase(LeanTweenType.easeInOutCubic).setLoopClamp();
                });

                
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

        //public void DetectColliders()
        //{
        //    foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, AttackRange))
        //    {
        //        Health health;
        //        if (health = collider.GetComponent<Health>())
        //        {
        //            health.GetHit(Attack, transform.gameObject);
        //            ShowFloatingText(Attack);
        //            if (gameObject.layer != collider.gameObject.layer)
        //            {
        //                collider.GetComponent<Animator>().SetBool("Hit", true);
        //                StartCoroutine(HitRecovery());
        //            }
        //        }
        //    }
        //}
        public void DetectColliders()
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, AttackRange))
            {
                PlayerArmor armor = collider.GetComponent<PlayerArmor>();
                Health health = collider.GetComponent<Health>();

                if (armor != null && !armor.isEmpty)
                {
                    armor.GetHit(Attack, transform.gameObject);
                    ShowFloatingText(Attack);

                    if (armor.currentArmor <= 0)
                    {
                        armor.isEmpty = true;
                    }

                    if (gameObject.layer != collider.gameObject.layer)
                    {
                        collider.GetComponent<Animator>().SetBool("Hit", true);
                        StartCoroutine(HitRecovery());
                    }
                }
                else if (health != null)
                {
                    health.GetHit(Attack, transform.gameObject);
                    ShowFloatingText(Attack);

                    if (gameObject.layer != collider.gameObject.layer)
                    {
                        collider.GetComponent<Animator>().SetBool("Hit", true);
                        StartCoroutine(HitRecovery());
                    }
                }
            }
        }

        private IEnumerator HitRecovery()
        {
            isRecovering = true;
            yield return new WaitForSeconds(recoveryTime);
            isRecovering = false;
        }
        private IEnumerator HandleDeath()
        {

          
            if (enemyCollider.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.velocity = Vector2.zero;
            }

            

            yield return new WaitForSeconds(0.9f);
            DropCoins(enemyCollider.transform.position);
            DropHearts(enemyCollider.transform.position);
            DropMaterials(enemyCollider.transform.position);
            Destroy(gameObject);

            GameManager.instance.TempEnemyKilled += 1;


        }

    }
}
