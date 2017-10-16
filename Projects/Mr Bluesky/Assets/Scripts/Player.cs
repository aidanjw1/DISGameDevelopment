/// <summary>
/// Script controls playermovement and player actions
/// </summary>
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
	//player movement
	[Tooltip("player left to right speed")]
	public float speed;
	[Tooltip("velcority player jumps up with")]
	public float jumpVelocity;
	[Tooltip("value of gravity that effects player")]
	public float gravity;
	[Tooltip("keeps track of number of player lives left")]
	public static int livesLeft;

	//check for grounding
	[Tooltip("width of ray casts")]
	public float rayCastWidth;
	[Tooltip("number of grounding raycasts")]
	public int numberOfRaycasts;
	[Tooltip("bool the checks if player is grounded")]
	public bool isGrounded = false;
	[Tooltip("ground tag used in grounding")]
	Transform tagGround;

	//animation
	[Tooltip("used for player animation")]
	private bool facingRight;
	[Tooltip("player flashes red when hit")]
	public int NumBlinks;
	[Tooltip("manages blinking red")]
	public float BlinkTime;
	[Tooltip("color when not blinking")]
	private Color NormalColor;
	[Tooltip("initializes player animation")]
	private Animator myAnimator; 


	//miscellanious
	[Tooltip("Initializes player rigidbody")]
	Rigidbody2D rigidBody;
	[Tooltip("layers that will trigger grounding raycast")]
	public LayerMask playerMask;
	[Tooltip("audio file for player death")]
	public AudioSource death;
	[Tooltip("player position used during player respawn")]
	public Vector3 currentPlayerPosition;


	void Start () {
		//is true when character is facing right, meaning x axis scale is set to 1
		facingRight = true; 
		rigidBody = GetComponent<Rigidbody2D>();
		tagGround = GameObject.Find (this.name + "tag_ground").transform;
		myAnimator = GetComponent<Animator> ();
		currentPlayerPosition = this.transform.position;
		NormalColor = this.gameObject.GetComponent<Renderer> ().material.color;

	}

	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		CheckGrounding ();
		HandleMovement (horizontal);
		Flip (horizontal);
	}


	/// <summary>
	/// Jump the specified velocity.
	/// </summary>
	/// <param name="velocity">Velocity.</param>
	Vector2 Jump(Vector2 velocity) {
		velocity.y = jumpVelocity;
		isGrounded = false;
		return velocity;
	}


	/// <summary>
	/// method handles movement involving on the x plane and calls the jump function
	/// changes states of animator for running and jumping
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	private void HandleMovement (float horizontal) {
		Vector2 velocity = rigidBody.velocity;
		velocity.x = horizontal * speed;

		if (isGrounded == false) {
			myAnimator.SetInteger ("State", 3);
		} else
			myAnimator.SetInteger ("State", 0);
		
		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));

		//checks for grounding then call jump  method
		if (Input.GetButtonDown ("Jump") && isGrounded) {
			velocity = Jump (velocity);
		}
		velocity.y += -gravity * Time.deltaTime;
		rigidBody.velocity = velocity;
	}


	/// <summary>
	/// handles which direction the character is facing, changes player animation
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	private void Flip(float horizontal) {
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;

			Vector3 playerScale = transform.localScale;
			playerScale.x *= -1; //invert the scale
			transform.localScale = playerScale;
		}
	}


	/// <summary>
	/// Checks the grounding. Function based on Benno's UpdateGrounding() function in his platformer2D Example and the raycast lab
	/// </summary>
	void CheckGrounding() {
		//set up center of player and raycast start position
		Vector2 groundCheckCenter = new Vector2 (transform.position.x + 0.6f, transform.position.y - 2f);
		Vector2 groundCheckStart = groundCheckCenter + Vector2.left * rayCastWidth * 1f;

		//evenly disperses raycasts 
		for (int i = 0; i < numberOfRaycasts; i++) {
			if (Physics2D.Linecast (groundCheckStart, tagGround.position, playerMask)) {
				isGrounded = true;
				return;
			}

			//updates raycast position
			groundCheckStart += Vector2.right * (1.2f / (numberOfRaycasts - 1.0f)) * rayCastWidth;
		}
			
		isGrounded = false;
	}

	/// <summary>
	/// Handles character death and respawns player at last known position
	/// is accessed by checkpoint script to update spawn location
	/// updates UI heart manager
	/// </summary>
	/// <param name="Respawn">If set to <c>true</c> respawn.</param>
	public void Die(bool Respawn = false) {
		death.Play ();
		GameObject.FindObjectOfType<LifeManager> ().GetComponent<LifeManager> ().updateHearts ();
		if (Respawn) {
			transform.position = currentPlayerPosition;
		}
		else {
			StartCoroutine (PlayerBlink ());
		}
	}

	/// <summary>
	/// player blinks red when hit with projectile or runs into laser
	/// </summary>
	/// <returns>The blink.</returns>
	private IEnumerator PlayerBlink() {
		Renderer ren = this.GetComponent<Renderer> ();
		for (int i = 0; i < NumBlinks; i++) {
			ren.material.color = Color.red;
			yield return new WaitForSeconds (BlinkTime);

			ren.material.color = NormalColor;
			yield return new WaitForSeconds (BlinkTime);
		}
	}
}