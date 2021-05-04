using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 10f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_DoubleJumpForce = 6f;                          // Amount of force added when the player double jumps.
																					//[Range(0, 1)] [SerializeField] private float m_airDragMultiplier = 0.7f;		// Reduce the movement speed in air when not giving input
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	private bool canMove = true;

	[SerializeField] float k_GroundedRadius; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	// Walking animation
	private Animator anim;
	private bool isWalking;
	private bool isRunning;
	//private float facingDir = Input.GetAxisRaw("Horizontal");

	// Jumping
	public int jumpLim = 2;
	private int noOfJumpsLeft;
	public bool canDoubleJump;
	public float variableJumpHeightMultiplier = 0.5f;
	private bool isJumping;

	// Wall Sliding
	public Transform m_WallCheck;
	private bool isTouchingWall;
	public float wallCheckDistance;
	private bool isWallSliding;
	public float wallSlideSpeed;
	private bool isHorKeyPressed;

	// Ledge Climbing
	public bool ledgeClimbActive;
	public Transform ledgeCheck;
	public Transform ledgeCheck2;
	private bool isTouchingLedge;
	private bool isActuallyTouchingLedge;
	private bool canClimbLedge = false;
	private bool ledgeDetected;
	private bool isHanging;
	private bool isClimbingLedge;
	private Vector2 ledgePosBot;
	private Vector2 ledgePos1;
	private Vector2 ledgePos2;
	public float ledgeClimbXoffset1 = 0f;
	public float ledgeClimbYoffset1 = 0f;
	public float ledgeClimbXoffset2 = 0f;
	public float ledgeClimbYoffset2 = 0f;

	// Dashing
	public bool isAllowedDash;
	private bool isDashing;
	public float dashTime;
	public float dashSpeed;
	public float distanceBetweenImages;
	public float dashCooldown;
	private float dashTimeLeft;
	private float lastImageXPos;
	private float lastDash = -100f;



	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	void Start()
	{
		anim = GetComponent<Animator>();
		noOfJumpsLeft = jumpLim;
	}


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();


		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}



	private void FixedUpdate()
	{
		checkIfWallSliding();
		isHorizontalPressed();
        if (ledgeClimbActive)
        {
			checkLedgeClimb();
		}
		updateAnimation();
		//checkLedgeClimb();

		bool wasGrounded = m_Grounded;
		//m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		//Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		//for (int i = 0; i < colliders.Length; i++)
		//{
		//	if (colliders[i].gameObject != gameObject)
		//	{
		//		m_Grounded = true;
		//		if (!wasGrounded)
		//			OnLandEvent.Invoke();
		//	}
		//	else
		//          {
		//		m_Grounded = false;
		//          }

		//}

		RaycastHit2D onGroundHit = Physics2D.Raycast(m_GroundCheck.position, Vector2.down, k_GroundedRadius, m_WhatIsGround);

		if (onGroundHit.collider == true)
		{
			m_Grounded = true;
		}
		else
		{
			m_Grounded = false;
		}

		


		// Wall Sliding
		
		isTouchingWall = Physics2D.Raycast(m_WallCheck.position, Vector2.right * transform.localScale, wallCheckDistance, m_WhatIsGround);
		
		

		// Ledge Climbing
		
		isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, Vector2.right * transform.localScale, wallCheckDistance, m_WhatIsGround);
		isActuallyTouchingLedge = Physics2D.Raycast(ledgeCheck2.position, Vector2.right * transform.localScale, wallCheckDistance, m_WhatIsGround);



		if (isActuallyTouchingLedge && !isTouchingLedge && !ledgeDetected)
		{
			ledgeDetected = true;
			ledgePosBot = ledgeCheck2.position;
		}
	}


	public void Move(float move, bool crouch, bool jump, bool dashButtonPressed)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			if (!isDashing)
			{
				// Move the character by finding the target velocity
				Vector2 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && canDoubleJump)
		{
			// Add a vertical force to the player.
			noOfJumpsLeft = jumpLim;
		}
		if (m_Grounded && !canDoubleJump)
		{
			if (jump)
			{
				m_Grounded = false;
				m_Rigidbody2D.velocity = (new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce));
			}
		}

		if (m_Grounded)
		{
			isJumping = false;
		}
		else
		{
			isJumping = true;
		}


		// Double Jump Section
		if (noOfJumpsLeft > 0 && canDoubleJump)
		{

			if (jump)
			{
				m_Grounded = false;
				if (noOfJumpsLeft == 1)
				{
					m_Rigidbody2D.velocity = (new Vector2(m_Rigidbody2D.velocity.x, m_DoubleJumpForce));
				}
				else
				{
					m_Rigidbody2D.velocity = (new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce));
				}
				noOfJumpsLeft--;
			}
		}

		// Dashing
		if (dashButtonPressed && isAllowedDash && !isDashing)
		{
			if (Time.time >= (lastDash + dashCooldown))
			{
				AttemptToDash();
			}


		}


		if (isDashing)
		{

			if (dashTimeLeft > 0)
			{
				canMove = false;
				m_Rigidbody2D.velocity = new Vector2((move * dashSpeed), m_Rigidbody2D.velocity.y);
				dashTimeLeft -= Time.deltaTime;

				//if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBetweenImages)
				//{
				//	PlayerAfterImagePool.Instance.GetFromPool();
				//	lastImageXPos = transform.position.x;
				//}
			}

			//if (dashTimeLeft <= 0 || isTouchingWall || isJumping)
			if (dashTimeLeft <= 0 || isTouchingWall)
				{
				canMove = true;
				isDashing = false;
			}

		}

		// Ledge Climb 
		


		// Wall Sliding Section
		if (isWallSliding)
		{
			if (m_Rigidbody2D.velocity.y < -wallSlideSpeed)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -wallSlideSpeed);
			}
		}
	}

	public void movementDir(float Dir, bool walk)
	{
		// Animation change
		if (Dir != 0)
		{
			isRunning = true;
			//if (walk == true)
			//         {
			//	isWalking = true;
			//	isRunning = false;
			//}
			//         else
			//         {
			//	isRunning = true;
			//	isWalking = false;
			//}
		}
		else
		{
			//isWalking = false;
			isRunning = false;
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void updateAnimation()
	{
		//anim.SetBool("isWalking", isWalking);
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isRunning", isRunning);
		anim.SetBool("isHanging", isHanging);
		anim.SetBool("isClimbingLedge", isClimbingLedge);
		anim.SetFloat("Velocity X", m_Rigidbody2D.velocity.x);
		anim.SetFloat("Velocity Y", m_Rigidbody2D.velocity.y);
	}

	private void isHorizontalPressed()
	{
		if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
		{
			isHorKeyPressed = true;
		}
		else
		{
			isHorKeyPressed = false;
		}

	}

	private void checkIfWallSliding()
	{
		if (isTouchingWall && !m_Grounded && m_Rigidbody2D.velocity.y < 0 && isHorKeyPressed && !canClimbLedge)
		{
			isWallSliding = true;
		}
		else
		{
			isWallSliding = false;
		}
	}


	private void checkLedgeClimb()
	{
		if (ledgeDetected && !canClimbLedge && Input.GetButton("Interact") && isJumping)
		{
			canClimbLedge = true;

			if (m_FacingRight)
			{
				ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXoffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset1);
				ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXoffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset2);
			}
			else
			{
				ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x + wallCheckDistance) - ledgeClimbXoffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset1);
				ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x + wallCheckDistance) - ledgeClimbXoffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset2);

			}


			canMove = false;
		}

        if (canClimbLedge)
        {
			if ( Input.GetButton("Jump"))
            {
				isHanging = false;
				isClimbingLedge = true;
            }
			transform.position = ledgePos1;
			isClimbingLedge = false;
			isHanging = true;
        }

	}

	public void FinishLedgeClimb()
    {
		canClimbLedge = true;
		transform.position = ledgePos2;
		canMove = true;
		ledgeDetected = false;
		isClimbingLedge = false;
		isHanging = false;
    }

	// Dashing
	private void AttemptToDash()
	{
		isDashing = true;
		dashTimeLeft = dashTime;
		lastDash = Time.time;

		//PlayerAfterImagePool.Instance.GetFromPool();
		//lastImageXPos = transform.position.x;

	}



}