using Assets.PixelHeroes.Scripts.CharacterScrips;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public PlayerTeleport playerTeleport;

        private Animator _animator;
        private Vector2 _input;
      


        public Rigidbody2D r2d;

        private bool canMove = true;
        private bool moving = false;

        private string[] attackAnimations = { "Slash", "Attack", "Jab" , "Shot"};

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (!playerTeleport.DeskPanel.activeSelf && !playerTeleport.BuildRoom.activeSelf)
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

                        // Stop the attack animation if the player is moving
                        foreach (string attackAnimation in attackAnimations)
                        {
                            _animator.SetBool(attackAnimation, false);
                        }
                    }
                    else
                    {
                        r2d.velocity = Vector2.zero;
                        _animator.SetBool("Idle", true);
                        _animator.SetBool("Walking", false);
                        _animator.SetBool("Running", false);
                        moving = false;
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

                // Handle attack input
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    // Stop the attack animation if the player is moving
                    if (_input != Vector2.zero)
                    {
                        foreach (string attackAnimation in attackAnimations)
                        {
                            _animator.SetBool(attackAnimation, false);
                        }
                    }
                    else
                    {
                        // Choose a random attack animation
                        string randomAttackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
                        // Set the selected animation
                        _animator.SetBool(randomAttackAnimation, true);
                    }
                }
            }
            else
            {
                // Stop movement and animations if DeskPanel is active
                r2d.velocity = Vector2.zero;
                _animator.SetBool("Idle", true);
                _animator.SetBool("Walking", false);
                _animator.SetBool("Running", false);
                moving = false;
            }
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
        private void Turn(int direction)
        {
            // Get the current scale of the character
            Vector3 scale = Character.transform.localScale;

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
            Character.transform.localScale = scale;
        }
       
    }
}