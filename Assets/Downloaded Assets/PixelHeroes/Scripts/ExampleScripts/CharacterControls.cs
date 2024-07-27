using Assets.PixelHeroes.Scripts.CharacterScrips;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

namespace Assets.PixelHeroes.Scripts.ExampleScripts
{
    public class CharacterControls : MonoBehaviour
    {

        public Character Character;
        public float WalkSpeed = 1f;
        public int runSpeed = 2;
        public float pushForce = 5f;
        private float manaRegenRate = 5f; // Amount of mana to regenerate per second
        private float manaDecrementRate = 10f; // Amount of mana to decrement per second while running
        private bool isRunning;
        private float currentMana;
        public bool isDead = false;
        private float attackCooldownTimer = 0.0f;
        private float AttackSpeed = 1.0f; // Set this to the desired attack speed

        public PlayerTeleport playerTeleport;
        
        public Animator _animator;
        private Vector2 _input;

        public Canvas NoInternet;

        public Rigidbody2D r2d;

        
        public Transform CircleOrigin;
        public float radius;

        public GameObject floatingTextPrefab; // Reference to the floating text prefab
        public Transform damageCanvas; // Reference to the Damage Canvas object public GameObject floatingTextPrefab; // Reference to the floating text prefab
        

        private bool canMove = true;
        private bool canRun = true;
        private bool moving = false;

        private string[] attackAnimations = { "Slash", "Attack", "Jab"};

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            r2d = GetComponent<Rigidbody2D>();
        }

        private async void Start()
        {
            
            Health health;
            health = GetComponent<Health>();
            PlayerArmor armor = GetComponent<PlayerArmor>();

            //playerstats1
            await Task.Delay(6000);
            health.currentHealth = (int)GameManager.instance.PlayerTotalHealth;
            armor.currentArmor = (int)GameManager.instance.PlayerTotalArmor;
            currentMana = (float)GameManager.instance.PlayerTotalMana;
            
            StartCoroutine(ManaRegenCoroutine());
            StartCoroutine(HealthRegenCoroutine(health));
            //StartCoroutine(ArmorRegenCoroutine(armor));
        }

        private void Update()
        {
            if (isDead == false)
            {
                if (!NoInternet.isActiveAndEnabled)
                {
                    if (!DialogueManager.GetInstance().dialogueIsPlaying)
                    {
                        if (!playerTeleport.DeskPanel.activeSelf && !playerTeleport.BuildRoom.activeSelf && !IsSceneLoaded("PCRush CharacterEditor"))
                        {
                            HandleMovement();
                            HandleAttack();
                        }
                        else
                        {
                            StopMovement();
                        }
                    }
                    else
                    {
                        ResetMovement();
                    }
                }
            }

            HandlePlayerHealth();
            HandlePlayerMana();
            HandlePlayerArmor();
            attackCooldownTimer -= Time.deltaTime; // Decrement the cooldown timer

            //playerstats2
            WalkSpeed = (float)GameManager.instance.PlayerTotalWalkSpeed;
            AttackSpeed = (float)GameManager.instance.PlayerTotalAttackSpeed;
        }

        private void HandlePlayerMana()
        {
            GameManager.instance.manaSlider.maxValue = (float)GameManager.instance.PlayerTotalMana;
            GameManager.instance.manaSlider.value = currentMana;
            GameManager.instance.manaText.text = currentMana.ToString("F0") + " MP" ;

            GameManager.instance.StatsmanaSlider.maxValue = (float)GameManager.instance.PlayerTotalMana;
            GameManager.instance.StatsmanaSlider.value = currentMana;
            GameManager.instance.StatsmanaText.text = currentMana.ToString("F0") + " /" + GameManager.instance.PlayerTotalMana.ToString();

            if (currentMana <= 0)
            {
                canRun = false;
            }
            else
            {
                canRun = true;
            }
        }

        public bool IsSceneLoaded(string sceneName)
        {
            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(i);

                // Check if the loaded scene's name matches the target scene name
                if (loadedScene.name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }
        private void ShowFloatingText(string damage)
        {
            if (floatingTextPrefab != null && damageCanvas != null)
            {
                GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, damageCanvas);
                DamageText floatingTextComponent = floatingText.GetComponent<DamageText>();
                floatingTextComponent.SetText(damage, Color.yellow);
            }
        }

      

        private void HandlePlayerHealth()
        {
            Health health;
            if (health = this.GetComponent<Health>())
            {

                health.maxHealth = (int)GameManager.instance.PlayerTotalHealth;

                GameManager.instance.healthSlider.maxValue = health.maxHealth;
                GameManager.instance.healthSlider.value = health.currentHealth;
                GameManager.instance.HealthhitPoints.text = health.currentHealth.ToString() + " HP";

                GameManager.instance.StatsPlayerHealthSlider.maxValue = health.maxHealth;
                GameManager.instance.StatsPlayerHealthSlider.value = health.currentHealth;
                GameManager.instance.StatsCurrentHealthText.text = health.currentHealth.ToString() + " / " + health.maxHealth.ToString();



                if (health.isDead || health.currentHealth == 0)
                {
                    isDead = true;
                    //enemyCollider.isTrigger = true;
                    
                    r2d.bodyType = RigidbodyType2D.Static;
                    _animator.SetBool("Dead", true);
                    triggerDied();
                }
            }
        }
        bool isDeadAnimate = false;
        public void triggerDied()
        {
           if(isDeadAnimate == false)
            {
                GameManager.instance.LTA.YouDied();
                isDeadAnimate = true;
                HandleDeath();
            }
        }

        private void HandlePlayerArmor()
        {
            PlayerArmor Armor;
            if (Armor = this.GetComponent<PlayerArmor>())
            {

                Armor.maxArmor = (int)GameManager.instance.PlayerTotalArmor;

                GameManager.instance.ArmorSlider.maxValue = Armor.maxArmor;
                GameManager.instance.ArmorSlider.value = Armor.currentArmor;
                GameManager.instance.armorText.text = Armor.currentArmor.ToString() + " AP";

                GameManager.instance.StatsArmorSlider.maxValue = Armor.maxArmor;
                GameManager.instance.StatsArmorSlider.value = Armor.currentArmor;
                GameManager.instance.StatsarmorText.text = Armor.currentArmor.ToString() + " / " + Armor.maxArmor.ToString();

            }
        }

        private void StopMovement()
        {
            // Stop movement and animations if DeskPanel is active
            r2d.velocity = Vector2.zero;
            _animator.SetBool("Idle", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
        }

        private void HandleAttack()
        {
            // Handle attack input
            if (UnityEngine.Input.GetMouseButtonDown(0) && attackCooldownTimer <= 0)
            {
                // Choose a random attack animation
                string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
                // Set the selected animation trigger
                _animator.SetTrigger(randomAttackAnimation);

                // Detect colliders for attack hit detection
                DetectColliders();

                // Reset the cooldown timer
                attackCooldownTimer = 1.0f / AttackSpeed;
            }
        }

        public void HandleMovement()
        {
            // Check if movement is allowed
            if (canMove)
            {
                // Handle movement input
                _input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
                _input.y = UnityEngine.Input.GetAxisRaw("Vertical");

                // Set animation parameters based on input
                if (_input != Vector2.zero)
                {
                    _input.Normalize();

                    // Turn the character based on input direction
                    if (_input.x < 0) // If moving left
                    {
                        Turn(-1); // Turn left (face left)
                    }
                    else if (_input.x > 0) // If moving right
                    {
                        Turn(1); // Turn right (face right)
                    }
                    // Calculate velocity vector based on input and speed
                    Vector2 velocity = _input * WalkSpeed;
                    // Apply velocity to Rigidbody2D
                    r2d.velocity = velocity;
                    Move(_input);

                    // Set movement animations
                    _animator.SetBool("Idle", false);
                    _animator.SetBool("Walking", true);
                    _animator.SetBool("Running", false);

                }
                else
                {
                    r2d.velocity = Vector2.zero;
                    _animator.SetBool("Idle", true);
                    _animator.SetBool("Walking", false);
                    _animator.SetBool("Running", false);
                }

                if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && _input != Vector2.zero)
                {
                    if(canRun == true)
                    {
                        // Set running animation
                        _animator.SetBool("Running", true);
                        _animator.SetBool("Walking", false);

                        // Calculate velocity vector based on input and running speed
                        Vector2 velocity = _input * (WalkSpeed + runSpeed);
                        // Apply velocity to Rigidbody2D
                        r2d.velocity = velocity;

                        isRunning = true;
                        currentMana = currentMana - manaDecrementRate * Time.deltaTime;
                    }
                   
                }
                else
                {
                    isRunning = false;
                }

            }
        }
        private IEnumerator ManaRegenCoroutine()
        {
            while (true)
            {
                if (!isRunning && currentMana < (float)GameManager.instance.PlayerTotalMana)
                {
                    currentMana = Mathf.Min(currentMana + manaRegenRate, (float)GameManager.instance.PlayerTotalMana);
                }
                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator HealthRegenCoroutine(Health health)
        {
            while (true)
            {
                if (GameManager.instance.InHomeWorld == true)
                {

                    if (health.currentHealth < GameManager.instance.PlayerTotalHealth)
                    {
                        health.currentHealth = (int)Mathf.Min((float)(health.currentHealth + GameManager.instance.PlayerTotalHealthRegen), (float)GameManager.instance.PlayerTotalHealth);
                    }

                }
                    yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator ArmorRegenCoroutine(PlayerArmor armor)
        {
            while (true)
            {
                if (GameManager.instance.InHomeWorld == true)
                {

                    if (armor.currentArmor < GameManager.instance.PlayerTotalArmor)
                    {
                        armor.currentArmor = (int)Mathf.Min((float)(armor.currentArmor + 5), (float)GameManager.instance.PlayerTotalArmor);
                    }

                }
                yield return new WaitForSeconds(1f);
            }
        }

        public GameObject RespawnPoint;
        public Transform GetRespawnPoint()
        {
            return RespawnPoint.transform;
        }

        private async void HandleDeath()
        {
            GameManager.instance.Hordemanager.StopCurrentHorde();
            await Task.Delay(5000);

            GameManager.instance.LTA.OpenTeleAnim();
            //// Teleport player to respawn point
            Transform respawnPoint = GetRespawnPoint(); // Implement this method to get the respawn point
            if (respawnPoint != null)
            {
                transform.position = respawnPoint.position;
            }

           

            playerTeleport.BackToTheHomeWorld();

            // Reset the player's state
            isDead = false;
            r2d.bodyType = RigidbodyType2D.Dynamic;
            Health health = GetComponent<Health>();
            if (health != null)
            {
                health.isDead = false;
                health.currentHealth = health.maxHealth; // Restore health
            }

            PlayerArmor armor = GetComponent<PlayerArmor>();
            if (armor != null)
            {
                armor.isEmpty = false;
                armor.currentArmor = armor.maxArmor; // Restore armor
            }

            //Reset animator parameters
            _animator.ResetTrigger("Dead");
           
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
            isDeadAnimate = false;
            HandleAttack();
        }

        public void ResetMovement()
        {
            r2d.velocity = Vector2.zero;
            _animator.SetBool("Idle", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
            foreach (string attackAnimation in attackAnimations)
            {
                _animator.SetBool(attackAnimation, false);
            }
            _input = Vector2.zero;
            moving = false;
        }
        
        private void Move(Vector2 movement)
        {
            // Calculate velocity vector based on input and speed
            Vector2 velocity = movement * WalkSpeed;

            // Apply velocity to Rigidbody2D
            r2d.velocity = velocity;

            _animator.SetBool("Idle", false);
            _animator.SetBool("Walking", true);
            _animator.SetBool("Running", false); ;
            moving = true;

        }

        public Transform CharacterBody;
        private void Turn(int direction)
        {
            // Get the current scale of the character
            Vector3 scale = CharacterBody.localScale;

            // Set the scale's x component based on the direction
            if (direction > 0)
            {
                // Facing right
                scale.x = Mathf.Abs(scale.x); // Ensure positive value
            }
            else if (direction < 0)
            {
                // Facing left
                scale.x = -Mathf.Abs(scale.x); // Ensure negative value
            }

            // Apply the new scale to the character
            CharacterBody.localScale = scale;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 position = CircleOrigin == null ? Vector3.zero : CircleOrigin.position;
            Gizmos.DrawWireSphere(position, radius);
        }

        public void DetectColliders()
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(CircleOrigin.position,radius))
            {
                //Debug.LogError(collider.name);
                Health health;
                if(health = collider.GetComponent<Health>())
                {

                    // Calculate damage with critical chance
                    bool isCritical = Random.Range(0, 100) < GameManager.instance.PlayerTotalCriticalChance;
                    if (isCritical)
                    {
                        health.GetHit((int)GameManager.instance.PlayerTotalCriticalHit, transform.gameObject);

                        if (gameObject.layer != collider.gameObject.layer)
                        {
                            ShowFloatingText("Critical Hit "+ GameManager.instance.PlayerTotalCriticalHit);
                            collider.GetComponent<Animator>().SetBool("Hit", true);

                        }
                    }
                    else
                    {
                        //attack
                        health.GetHit((int)GameManager.instance.PlayerTotalAttackDamage, transform.gameObject);

                        if (gameObject.layer != collider.gameObject.layer)
                        {
                            ShowFloatingText(GameManager.instance.PlayerTotalAttackDamage.ToString());
                            collider.GetComponent<Animator>().SetBool("Hit", true);

                        }
                    }

                   

                    Vector2 pushDirection = (collider.transform.position - transform.position).normalized;

                    // Apply push force to the collider
                    Rigidbody2D colliderRigidbody = collider.GetComponent<Rigidbody2D>();
                    if (colliderRigidbody != null)
                    {
                        // Adjust this value as needed
                        colliderRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                    }

                }

                
            }
        }

    }

   
}