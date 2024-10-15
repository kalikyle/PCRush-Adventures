using Assets.PixelHeroes.Scripts.CharacterScrips;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;
using Input = UnityEngine.Input;

namespace Assets.PixelHeroes.Scripts.ExampleScripts
{
    public class CharacterControls : MonoBehaviour
    {
        public Joystick movementJoystick;
        public Character Character;
        public float WalkSpeed = 1f;
        public int runSpeed = 2;
        public float pushForce = 5f;
        //private float manaRegenRate = 5f; // Amount of mana to regenerate per second
        private float manaDecrementRate = 10f; // Amount of mana to decrement per second while running
        private bool isRunning;
        private float currentMana;
        public bool isDead = false;
        public Button AttackBTN;
        //private float attackCooldownTimer = 0.0f;
        //private float AttackSpeed = 1.0f; // Set this to the desired attack speed

        public PlayerTeleport playerTeleport;
        
        public Animator _animator;
        private Vector2 _input;

        public Canvas NoInternet;

        public Rigidbody2D r2d;

        
        public Transform CircleOrigin;
        public float radius;

        public GameObject floatingTextPrefab; // Reference to the floating text prefab
        public Transform damageCanvas; // Reference to the Damage Canvas object public GameObject floatingTextPrefab; // Reference to the floating text prefab

        private bool canAttack = true;
        private bool canMove = true;
        private bool canRun = true;
        //private bool moving = false;

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
            AttackBTN.onClick.AddListener(TriggerAttack);
            //StartCoroutine(ArmorRegenCoroutine(armor));
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            if (GameManager.instance.MinimapOpened == false)
            {
                if(GameManager.instance.LTA.HordeDone == false) { 

                    if (isDead == false)
                    {
                        if (!NoInternet.isActiveAndEnabled)
                        {
                            if (!DialogueManager.GetInstance().dialogueIsPlaying)
                            {
                                if (!GameManager.instance.PlayerDeskUI.activeSelf && !playerTeleport.BuildRoom.activeSelf && !IsSceneLoaded("PCRush CharacterEditor"))
                                {
                                    HandleMovement();

                                   
                                        // Handle attack logic if cursor is over your panel
                                    //HandleAttack();
                                    

                                    //HandleAttack();
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
                }
                else
                {
                    StopMovement();
                }
            }
            else
            {
                StopMovement();
            }

            HandlePlayerHealth();
            HandlePlayerMana();
            HandlePlayerArmor();
            //attackCooldownTimer -= Time.deltaTime; // Decrement the cooldown timer

            //playerstats2
            WalkSpeed = (float)GameManager.instance.PlayerTotalWalkSpeed;
            //AttackSpeed = (float)GameManager.instance.PlayerTotalAttackSpeed;
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
                canAttack = false;
                currentMana = 0;
            }
            else
            {
                canRun = true;
                canAttack = true;
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
        private void ShowFloatingText(string damage, Color color)
        {
            if (floatingTextPrefab != null && damageCanvas != null)
            {
                GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, damageCanvas);
                DamageText floatingTextComponent = floatingText.GetComponent<DamageText>();
               
                    floatingTextComponent.SetText(damage, color);

                
                
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
                    _animator.SetBool("Dead", true);
                    r2d.bodyType = RigidbodyType2D.Static;
                    triggerDied();
                }
            }
        }
        bool isDeadAnimate = false;
        public void triggerDied()
        {
           if(isDeadAnimate == false)
            {
                isDeadAnimate = true;
                GameManager.instance.LTA.YouDied();
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

            //// Handle mouse input for attack (PC)
            //if (UnityEngine.Input.GetMouseButtonDown(0))
            //{
            //    if (canAttack && IsCursorOverGameObject("AttackArea"))
            //    {
            //        TriggerAttack();
            //    }
            //}

            //// Handle touch input for attack (mobile)
            //foreach (Touch touch in Input.touches)
            //{
            //    // Check if the touch is not on the joystick area and is over the attack area
            //    if (touch.phase == TouchPhase.Began && !IsTouchOverJoystick(touch))
            //    {
            //        if (canAttack && IsCursorOverGameObject("AttackArea"))
            //        {
            //            TriggerAttack();
            //        }
            //    }
            //}
        }

        //private bool IsTouchOverJoystick(Touch touch)
        //{
        //    // Check if the touch position is over the joystick's area
        //    // You might use RectTransform to check the bounds of the joystick UI
        //    return RectTransformUtility.RectangleContainsScreenPoint(
        //        movementJoystick.GetComponent<RectTransform>(),
        //        touch.position,
        //        null
        //    );
        //}

        private void TriggerAttack()
        {
            // Play attack sound
            if (canAttack)
            {
                SoundManager.instance.PlayAttackSound();

                // Trigger random attack animation
                string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
                _animator.SetTrigger(randomAttackAnimation);

                // Detect colliders for attack hit detection
                DetectColliders();

                // Decrease mana for attack
                currentMana -= 2f;
            }
        }
        //private void HandleAttack()
        //{
        //    // Handle attack input
        //    if (UnityEngine.Input.GetMouseButtonDown(0))
        //    {

        //        if (canAttack)
        //        {
        //            if (IsCursorOverGameObject("AttackArea"))
        //            {
        //                SoundManager.instance.PlayAttackSound();

        //                // Choose a random attack animation
        //                string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
        //                // Set the selected animation trigger
        //                _animator.SetTrigger(randomAttackAnimation);

        //                // Detect colliders for attack hit detection
        //                DetectColliders();
        //                currentMana = currentMana - 2f;
        //                // Reset the cooldown timer
        //                //attackCooldownTimer = 1.0f / AttackSpeed;
        //            }
        //        }
        //    }
        //}
        //public void HandleJoyStickMovement()
        //{
        //    if (movementJoystick.Direction.y != 0) {

        //        r2d.velocity = new Vector2(movementJoystick.Direction.x * WalkSpeed, movementJoystick.Direction.y * WalkSpeed);

        //    }
        //    else
        //    {
        //        r2d.velocity = Vector2.zero;
        //    }
        //}
        //public void HandleMovement()
        //{
        //    // Check if movement is allowed
        //    if (canMove)
        //    {

        //        // Handle movement input
        //        _input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        //        _input.y = UnityEngine.Input.GetAxisRaw("Vertical");

        //        // Set animation parameters based on input
        //        if (_input != Vector2.zero)
        //        {
        //            // Play walking sound only if it's not already playing
        //            if (!SoundManager.instance.soundEffectSource.isPlaying)
        //            {
        //                SoundManager.instance.PlayWalkingSound();
        //            }

        //            _input.Normalize();

        //            // Turn the character based on input direction
        //            if (_input.x < 0) // If moving left
        //            {
        //                Turn(-1); // Turn left (face left)
        //            }
        //            else if (_input.x > 0) // If moving right
        //            {
        //                Turn(1); // Turn right (face right)
        //            }
        //            // Calculate velocity vector based on input and speed
        //            Vector2 velocity = _input * WalkSpeed;
        //            // Apply velocity to Rigidbody2D
        //            r2d.velocity = velocity;
        //            Move(_input);

        //            // Set movement animations
        //            _animator.SetBool("Idle", false);
        //            _animator.SetBool("Walking", true);
        //            _animator.SetBool("Running", false);


        //        }
        //        else
        //        {
        //            //if (SoundManager.instance.soundEffectSource.isPlaying)
        //            //{
        //            //    SoundManager.instance.soundEffectSource.Stop();
        //            //}

        //            r2d.velocity = Vector2.zero;
        //            _animator.SetBool("Idle", true);
        //            _animator.SetBool("Walking", false);
        //            _animator.SetBool("Running", false);
        //        }

        //        if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && _input != Vector2.zero)
        //        {

        //            if (canRun == true)
        //            {
        //                // Set running animation

        //                _animator.SetBool("Running", true);
        //                _animator.SetBool("Walking", false);

        //                // Calculate velocity vector based on input and running speed
        //                Vector2 velocity = _input * (WalkSpeed + runSpeed);
        //                // Apply velocity to Rigidbody2D
        //                r2d.velocity = velocity;

        //                isRunning = true;
        //                currentMana = currentMana - manaDecrementRate * Time.deltaTime;
        //                runSoundTimer -= Time.deltaTime;
        //                if (runSoundTimer <= 0f)
        //                {
        //                    SoundManager.instance.PlayRunSound(); // Play run sound
        //                    runSoundTimer = runSoundInterval; // Reset timer
        //                }
        //            }

        //        }
        //        else
        //        {

        //            isRunning = false;
        //        }

        //    }
        //}
        //public void HandleMovement()
        //{
        //    // Check if movement is allowed
        //    if (canMove)
        //    {
        //        Vector2 input = Vector2.zero;

        //        // Check joystick input
        //        if (movementJoystick.Direction != Vector2.zero)
        //        {
        //            input = movementJoystick.Direction;
        //        }
        //        else
        //        {
        //            // Check keyboard input
        //            input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        //            input.y = UnityEngine.Input.GetAxisRaw("Vertical");
        //        }

        //        if (input != Vector2.zero)
        //        {
        //            if (!SoundManager.instance.walkingSoundSource.isPlaying)
        //            {
        //                SoundManager.instance.PlayWalkingSound();
        //            }
        //            // Normalize input
        //            input.Normalize();

        //            // Turn the character based on input direction
        //            if (input.x < 0) // If moving left
        //            {
        //                Turn(-1); // Turn left (face left)
        //            }
        //            else if (input.x > 0) // If moving right
        //            {
        //                Turn(1); // Turn right (face right)
        //            }

        //            // Calculate velocity vector based on input and speed
        //            Vector2 velocity = input * WalkSpeed;

        //            // Apply velocity to Rigidbody2D
        //            r2d.velocity = velocity;
        //            Move(input);

        //            // Set movement animations
        //            _animator.SetBool("Idle", false);
        //            _animator.SetBool("Walking", true);
        //            _animator.SetBool("Running", false);
        //        }
        //        else
        //        {
        //            // Stop movement
        //            r2d.velocity = Vector2.zero;

        //            // Set idle animation
        //            _animator.SetBool("Idle", true);
        //            _animator.SetBool("Walking", false);
        //            _animator.SetBool("Running", false);

        //            // Stop walking sound if needed
        //            if (SoundManager.instance.walkingSoundSource.isPlaying)
        //            {
        //                SoundManager.instance.soundEffectSource.Stop();
        //            }
        //        }

        //        // Check if running
        //        if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && input != Vector2.zero)
        //        {

        //            if (canRun == true)
        //            {
        //                // Set running animation

        //                _animator.SetBool("Running", true);
        //                _animator.SetBool("Walking", false);

        //                // Calculate velocity vector based on input and running speed
        //                Vector2 velocity = input * (WalkSpeed + runSpeed);
        //                // Apply velocity to Rigidbody2D
        //                r2d.velocity = velocity;

        //                isRunning = true;
        //                currentMana = currentMana - manaDecrementRate * Time.deltaTime;
        //                runSoundTimer -= Time.deltaTime;
        //                if (runSoundTimer <= 0f)
        //                {
        //                    SoundManager.instance.PlayRunSound(); // Play run sound
        //                    runSoundTimer = runSoundInterval; // Reset timer
        //                }
        //            }

        //        }
        //        else
        //        {

        //            isRunning = false;
        //        }
        //    }
        //}

        public void HandleMovement()
        {
            // Check if movement is allowed
            if (canMove)
            {
                Vector2 input = Vector2.zero;

                // Handle joystick input (mobile)
                if (movementJoystick.Direction != Vector2.zero)
                {
                    input = movementJoystick.Direction;  // Joystick input
                }
                else
                {
                    // Handle keyboard input (for PC)
                    input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
                    input.y = UnityEngine.Input.GetAxisRaw("Vertical");
                }

                if (input != Vector2.zero)
                {
                    // Play walking sound if not already playing
                    if (!SoundManager.instance.walkingSoundSource.isPlaying)
                    {
                        SoundManager.instance.PlayWalkingSound();
                    }

                    // Normalize input for smooth movement
                    input.Normalize();

                    // Turn the character based on input direction
                    if (input.x < 0) // Moving left
                    {
                        Turn(-1); // Turn left (face left)
                    }
                    else if (input.x > 0) // Moving right
                    {
                        Turn(1); // Turn right (face right)
                    }

                    // Calculate and apply velocity based on input
                    Vector2 velocity = input * WalkSpeed;
                    r2d.velocity = velocity;

                    Move(input);

                    // Set movement animations
                    _animator.SetBool("Idle", false);
                    _animator.SetBool("Walking", true);
                    _animator.SetBool("Running", false);
                }
                else
                {
                    // Stop movement
                    r2d.velocity = Vector2.zero;

                    // Set idle animation
                    _animator.SetBool("Idle", true);
                    _animator.SetBool("Walking", false);
                    _animator.SetBool("Running", false);

                    // Stop walking sound
                    if (SoundManager.instance.walkingSoundSource.isPlaying)
                    {
                        SoundManager.instance.soundEffectSource.Stop();
                    }
                }

                // Running check (if LeftShift is pressed for PC, or joystick for mobile)
                if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && input != Vector2.zero)
                {
                    if (canRun == true)
                    {
                        // Set running animation
                        _animator.SetBool("Running", true);
                        _animator.SetBool("Walking", false);

                        // Calculate running speed
                        Vector2 velocity = input * (WalkSpeed + runSpeed);
                        r2d.velocity = velocity;

                        // Decrease mana when running
                        isRunning = true;
                        currentMana -= manaDecrementRate * Time.deltaTime;

                        // Play running sound with interval
                        runSoundTimer -= Time.deltaTime;
                        if (runSoundTimer <= 0f)
                        {
                            SoundManager.instance.PlayRunSound();
                            runSoundTimer = runSoundInterval; // Reset timer
                        }
                    }
                }
                else
                {
                    isRunning = false;
                }
            }
        }

        //bool IsCursorOverGameObject(string gameObjectName)
        //{
        //    PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        //    {
        //        position = UnityEngine.Input.mousePosition
        //    };

        //    List<RaycastResult> raycastResults = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        //    foreach (RaycastResult result in raycastResults)
        //    {
        //        if (result.gameObject.name == gameObjectName)
        //        {
        //            return true; // Cursor is over the GameObject or UI Panel
        //        }
        //    }

        //    return false; // Cursor is not over the specific GameObject
        //}

        private float runSoundTimer = 0f; // Timer to space out running sounds
        public float runSoundInterval = 0.3f; // Interval between running sounds
        private IEnumerator ManaRegenCoroutine()
        {
            while (true)
            {
                if (!isRunning && currentMana < (float)GameManager.instance.PlayerTotalMana)
                {
                    currentMana = (int)Mathf.Min((float)(currentMana + GameManager.instance.PlayerTotalManaRegen), (float)GameManager.instance.PlayerTotalMana);
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


            await Task.Delay(1000);
            // Reset animator parameters
            _animator.SetBool("Dead", false);
            _animator.SetBool("Idle", true); // Ensure Idle is set
            isDeadAnimate = false;

            
            //_animator.SetTrigger("Jab");
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
                        health.GetHit((int)GameManager.instance.PlayerTotalAttackDamage * 2, transform.gameObject);

                        if (gameObject.layer != collider.gameObject.layer)
                        {
                            ShowFloatingText("Critical Hit "+ (int)GameManager.instance.PlayerTotalAttackDamage * 2, Color.magenta);
                            collider.GetComponent<Animator>().SetBool("Hit", true);
                            

                        }
                    }
                    else
                    {
                        //attack
                        health.GetHit((int)GameManager.instance.PlayerTotalAttackDamage, transform.gameObject);

                        if (gameObject.layer != collider.gameObject.layer)
                        {
                            ShowFloatingText(GameManager.instance.PlayerTotalAttackDamage.ToString(), Color.yellow);
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