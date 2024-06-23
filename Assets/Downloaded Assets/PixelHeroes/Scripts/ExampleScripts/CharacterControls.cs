using Assets.PixelHeroes.Scripts.CharacterScrips;
using System.Collections;
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
        //    public Character Character;
        //    //public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html
        //    public float RunSpeed = 1f;
        //    public float JumpSpeed = 3f;
        //    public float CrawlSpeed = 0.25f;
        //    public float Gravity = -0.2f;
        //    public ParticleSystem MoveDust;
        //    public ParticleSystem JumpDust;

        //    private Vector3 _motion = Vector3.zero;
        //    private int _inputX, _inputY;
        //    private float _activityTime;

        //    public void Start()
        //    {
        //        Character.SetState(AnimationState.Idle);
        //    }

        //    public void Update()
        //    {
        //        if (Input.GetKeyDown(KeyCode.A)) Character.Animator.SetTrigger("Attack");
        //        else if (Input.GetKeyDown(KeyCode.J)) Character.Animator.SetTrigger("Jab");
        //        else if (Input.GetKeyDown(KeyCode.P)) Character.Animator.SetTrigger("Push");
        //        else if (Input.GetKeyDown(KeyCode.H)) Character.Animator.SetTrigger("Hit");
        //        else if (Input.GetKeyDown(KeyCode.I)) { Character.SetState(AnimationState.Idle); _activityTime = 0; }
        //        else if (Input.GetKeyDown(KeyCode.R)) { Character.SetState(AnimationState.Ready); _activityTime = Time.time; }
        //        else if (Input.GetKeyDown(KeyCode.B)) Character.SetState(AnimationState.Blocking);
        //        else if (Input.GetKeyUp(KeyCode.B)) Character.SetState(AnimationState.Ready);
        //        else if (Input.GetKeyDown(KeyCode.D)) Character.SetState(AnimationState.Dead);

        //        // Builder characters only.
        //        else if (Input.GetKeyDown(KeyCode.S)) Character.Animator.SetTrigger("Slash");
        //        else if (Input.GetKeyDown(KeyCode.O)) Character.Animator.SetTrigger("Shot");
        //        else if (Input.GetKeyDown(KeyCode.F)) Character.Animator.SetTrigger("Fire1H");
        //        else if (Input.GetKeyDown(KeyCode.E)) Character.Animator.SetTrigger("Fire2H");
        //        else if (Input.GetKeyDown(KeyCode.C)) Character.SetState(AnimationState.Climbing);
        //        else if (Input.GetKeyUp(KeyCode.C)) Character.SetState(AnimationState.Ready);
        //        else if (Input.GetKeyUp(KeyCode.L)) Character.Blink();

        //        if (Controller.isGrounded)
        //        {
        //            if (Input.GetKeyDown(KeyCode.DownArrow))
        //            {
        //                GetDown();
        //            }
        //            else if (Input.GetKeyUp(KeyCode.DownArrow))
        //            {
        //                GetUp();
        //            }
        //        }

        //        if (Input.GetKey(KeyCode.LeftArrow))
        //        {
        //            _inputX = -1;
        //        }
        //        else if (Input.GetKey(KeyCode.RightArrow))
        //        {
        //            _inputX = 1;
        //        }

        //        if (Input.GetKeyDown(KeyCode.UpArrow))
        //        {
        //            _inputY = 1;

        //            if (Controller.isGrounded)
        //            {
        //                JumpDust.Play(true);
        //            }
        //        }
        //    }

        //    public void FixedUpdate()
        //    {
        //        Move();
        //    }

        //    private void Move()
        //    {
        //        if (Time.frameCount <= 1)
        //        {
        //            Controller.Move(new Vector3(0, Gravity) * Time.fixedDeltaTime);
        //            return;
        //        }

        //        var state = Character.GetState();

        //        if (state == AnimationState.Dead)
        //        {
        //            if (_inputX == 0) return;

        //            Character.SetState(AnimationState.Running);
        //        }

        //        if (_inputX != 0)
        //        {
        //            Turn(_inputX);
        //        }

        //        if (Controller.isGrounded)
        //        {
        //            if (state == AnimationState.Jumping)
        //            {
        //                if (Input.GetKey(KeyCode.DownArrow))
        //                {
        //                    GetDown();
        //                }
        //                else
        //                {
        //                    Character.Animator.SetTrigger("Landed");
        //                    Character.SetState(AnimationState.Ready);
        //                    JumpDust.Play(true);
        //                }
        //            }

        //            _motion = state == AnimationState.Crawling
        //                ? new Vector3(CrawlSpeed * _inputX, 0)
        //                : new Vector3(RunSpeed * _inputX, JumpSpeed * _inputY);

        //            if (_inputX != 0 || _inputY != 0)
        //            {
        //                if (_inputY > 0)
        //                {
        //                    Character.SetState(AnimationState.Jumping);
        //                }
        //                else
        //                {
        //                    switch (state)
        //                    {
        //                        case AnimationState.Idle:
        //                        case AnimationState.Ready:
        //                            Character.SetState(AnimationState.Running);
        //                            break;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                switch (state)
        //                {
        //                    case AnimationState.Crawling:
        //                    case AnimationState.Climbing:
        //                    case AnimationState.Blocking:
        //                        break;
        //                    default:
        //                        var targetState = Time.time - _activityTime > 5 ? AnimationState.Idle : AnimationState.Ready;

        //                        if (state != targetState)
        //                        {
        //                            Character.SetState(targetState);
        //                        }

        //                        break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            _motion = new Vector3(RunSpeed * _inputX, _motion.y);
        //            Character.SetState(AnimationState.Jumping);
        //        }

        //        _motion.y += Gravity;

        //        Controller.Move(_motion * Time.fixedDeltaTime);

        //        Character.Animator.SetBool("Grounded", Controller.isGrounded);
        //        Character.Animator.SetBool("Moving", Controller.isGrounded && _inputX != 0);
        //        Character.Animator.SetBool("Falling", !Controller.isGrounded && Controller.velocity.y < 0);

        //        if (_inputX != 0 && _inputY != 0 || Character.Animator.GetBool("Action"))
        //        {
        //            _activityTime = Time.time;
        //        }

        //        _inputX = _inputY = 0;

        //        if (Controller.isGrounded && !Mathf.Approximately(Controller.velocity.x, 0))
        //        {
        //            var velocity = MoveDust.velocityOverLifetime;

        //            velocity.xMultiplier = 0.2f * -Mathf.Sign(Controller.velocity.x);

        //            if (!MoveDust.isPlaying)
        //            {
        //                MoveDust.Play();
        //            }
        //        }
        //        else
        //        {
        //            MoveDust.Stop();
        //        }
        //    }

        //    private void Turn(int direction)
        //    {
        //        var scale = Character.transform.localScale;

        //        scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

        //        Character.transform.localScale = scale;
        //    }

        //    private void GetDown()
        //    {
        //        Character.Animator.SetTrigger("GetDown");
        //        Character.CharacterController.center = new Vector3(0, 0.06f);
        //        Character.CharacterController.height = 0.08f;
        //    }

        //    private void GetUp()
        //    {
        //        Character.Animator.SetTrigger("GetUp");
        //        Character.CharacterController.center = new Vector3(0, 0.08f);
        //        Character.CharacterController.height = 0.16f;
        //    }
        //}
        public Character Character;
        public float WalkSpeed = 1f;
        public int runSpeed = 2;
        public float pushForce = 5f;
        public int attack = 1;
        public Slider healthSlider;
        public TMP_Text hitPoints;
        public bool isDead = false;

        public PlayerTeleport playerTeleport;

        public Animator _animator;
        private Vector2 _input;

        public Canvas NoInternet;

        public Rigidbody2D r2d;

        //public GameObject gameObjects;
        public Transform CircleOrigin;
        public float radius;

        public GameObject floatingTextPrefab; // Reference to the floating text prefab
        public Transform damageCanvas; // Reference to the Damage Canvas object public GameObject floatingTextPrefab; // Reference to the floating text prefab
        //public Health health;

        private bool canMove = true;
        private bool moving = false;

        private string[] attackAnimations = { "Slash", "Attack", "Jab"};

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            Health health;
            health = GetComponent<Health>();
            health.maxHealth = GameManager.instance.PlayerHealth;
            health.currentHealth = GameManager.instance.PlayerHealth;
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
        private void ShowFloatingText(int damage)
        {
            if (floatingTextPrefab != null && damageCanvas != null)
            {
                GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, damageCanvas);
                DamageText floatingTextComponent = floatingText.GetComponent<DamageText>();
                floatingTextComponent.SetText(damage.ToString(), Color.yellow);
            }
        }

        //private void Update()
        //{
        //    if (!NoInternet.isActiveAndEnabled)
        //    {
        //        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        //        {
        //            if (!playerTeleport.DeskPanel.activeSelf && !playerTeleport.BuildRoom.activeSelf && !IsSceneLoaded("PCRush CharacterEditor"))
        //            {
        //                // Check if movement is allowed
        //                if (canMove)
        //                {
        //                    // Handle movement input
        //                    _input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        //                    _input.y = UnityEngine.Input.GetAxisRaw("Vertical");

        //                    // Set animation parameters based on input
        //                    if (_input != Vector2.zero)
        //                    {
        //                        _input.Normalize();

        //                        // Turn the character based on input direction
        //                        if (_input.x < 0) // If moving left
        //                        {
        //                            Turn(-1); // Turn left (face left)
        //                        }
        //                        else if (_input.x > 0) // If moving right
        //                        {
        //                            Turn(1); // Turn right (face right)
        //                        }
        //                        // Calculate velocity vector based on input and speed
        //                        Vector2 velocity = _input * WalkSpeed;
        //                        // Apply velocity to Rigidbody2D
        //                        r2d.velocity = velocity;
        //                        Move(_input);

        //                        // Stop the attack animation if the player is moving
        //                        foreach (string attackAnimation in attackAnimations)
        //                        {
        //                            _animator.SetBool(attackAnimation, false);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        r2d.velocity = Vector2.zero;
        //                        _animator.SetBool("Idle", true);
        //                        _animator.SetBool("Walking", false);
        //                        _animator.SetBool("Running", false);
        //                        moving = false;
        //                    }

        //                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && moving)
        //                    {
        //                        // Set running animation
        //                        _animator.SetBool("Running", true);
        //                        _animator.SetBool("Walking", false);

        //                        // Calculate velocity vector based on input and running speed
        //                        Vector2 velocity = _input * (WalkSpeed + runSpeed);
        //                        // Apply velocity to Rigidbody2D
        //                        r2d.velocity = velocity;
        //                    }
        //                }

        //                // Handle attack input
        //                if (UnityEngine.Input.GetMouseButtonDown(0))
        //                {
        //                    // Stop the attack animation if the player is moving
        //                    if (_input != Vector2.zero)
        //                    {
        //                        foreach (string attackAnimation in attackAnimations)
        //                        {
        //                            _animator.SetBool(attackAnimation, false);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Choose a random attack animation
        //                        string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
        //                        // Set the selected animation
        //                        _animator.SetBool(randomAttackAnimation, true);
        //                    }
        //                    DetectColliders();
        //                }
        //            }
        //            else
        //            {
        //                // Stop movement and animations if DeskPanel is active
        //                r2d.velocity = Vector2.zero;
        //                _animator.SetBool("Idle", true);
        //                _animator.SetBool("Walking", false);
        //                _animator.SetBool("Running", false);
        //                moving = false;
        //            }
        //        }
        //        else
        //        {
        //            ResetMovement();
        //        }

        //    }

        //}
        private void Update()
        {
            if(isDead == false)
            {
                if (!NoInternet.isActiveAndEnabled)
                {
                    if (!DialogueManager.GetInstance().dialogueIsPlaying)
                    {
                        if (!playerTeleport.DeskPanel.activeSelf && !playerTeleport.BuildRoom.activeSelf && !IsSceneLoaded("PCRush CharacterEditor"))
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
                                    // Set running animation
                                    _animator.SetBool("Running", true);
                                    _animator.SetBool("Walking", false);

                                    // Calculate velocity vector based on input and running speed
                                    Vector2 velocity = _input * (WalkSpeed + runSpeed);
                                    // Apply velocity to Rigidbody2D
                                    r2d.velocity = velocity;
                                }
                            }

                            // Handle attack input
                            if (UnityEngine.Input.GetMouseButtonDown(0))
                            {
                                // Choose a random attack animation
                                string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
                                // Set the selected animation trigger
                                _animator.SetTrigger(randomAttackAnimation);

                                // Detect colliders for attack hit detection
                                DetectColliders();
                            }
                        }
                        else
                        {
                            // Stop movement and animations if DeskPanel is active
                            r2d.velocity = Vector2.zero;
                            _animator.SetBool("Idle", true);
                            _animator.SetBool("Walking", false);
                            _animator.SetBool("Running", false);
                        }
                    }
                    else
                    {
                        ResetMovement();
                    }
                }
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
                    //enemyCollider.isTrigger = true;
                    _animator.SetBool("Dead", true);
                    StartCoroutine(HandleDeath());

                }
            }


        }

        private IEnumerator HandleDeath()
        {
            yield return new WaitForSeconds(3f); // Wait for 5 seconds

            // Teleport player to respawn point
            Transform respawnPoint = GetRespawnPoint(); // Implement this method to get the respawn point
            if (respawnPoint != null)
            {
                transform.position = respawnPoint.position;
            }

            // Reset the player's state
            isDead = false;
            Health health = GetComponent<Health>();
            if (health != null)
            {
                health.isDead = false;
                health.currentHealth = health.maxHealth; // Restore health
            }

            // Reset animator parameters
            _animator.ResetTrigger("Dead");
            _animator.SetBool("Idle", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
        }

        public Transform respawnPoint;

        private Transform GetRespawnPoint()
        {
            return respawnPoint;
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
        private void Updates()
        {
            if (!playerTeleport.DeskPanel.activeSelf)
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
                    }
                    else
                    {
                        r2d.velocity = Vector2.zero;
                        _animator.SetBool("Idle", true);
                        _animator.SetBool("Walking", false);
                        _animator.SetBool("Running", false) ;
                        moving = false;
                    }

                    // Handle attack input
                    if (UnityEngine.Input.GetMouseButtonDown(0))
                    {
                        
                        //_animator.SetBool("Slash", true);
                        // Choose a random attack animation
                        string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
                        // Set the selected animation
                        _animator.SetBool(randomAttackAnimation, true);
                    }

                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && moving)
                    {
                        // Set running animation
                        _animator.SetBool("Running", true);
                        _animator.SetBool("Walking", false);

                        // Calculate velocity vector based on input and running speed
                        Vector2 velocity = _input * (WalkSpeed + runSpeed);
                        // Apply velocity to Rigidbody2D
                        r2d.velocity = velocity;
                    }
                }
            }
            else
            {
                // Stop movement and animations if DeskPanel is active
                r2d.velocity = Vector2.zero;
                _animator.SetBool("Idle",true);
                _animator.SetBool("Walking", false);
                _animator.SetBool("Running", false); ;
                moving = false;
            }
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
                    //attack
                    health.GetHit(attack, transform.gameObject);

                    if (gameObject.layer != collider.gameObject.layer)
                    {
                        ShowFloatingText(attack);
                        collider.GetComponent<Animator>().SetBool("Hit", true);
                       
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