using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour 
{
	public float maxSpeed;
	public GameObject Paddle;
	public int scorePlayer = 0;
	public int lifePlayer = 3;
	public Text debugText, scoreText, lifeText, endgameText;
	public Transform pauseMenu, endgameMenu, Bricks;
	public GameObject[] BrickRed, BrickOrange, BrickYellow, BrickGreen;

	private Rigidbody2D rb2d;
	private Vector2 ballVelocity, paddleScaleOriginal, paddleScaleCurrent;

	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		Invoke ("ballStart", 2.0f);
		paddleScaleOriginal = Paddle.transform.localScale;
		debugText.text = "";
	}

	void FixedUpdate () 
	{
		rb2d.velocity = Vector2.ClampMagnitude (rb2d.velocity, maxSpeed);
		paddleScaleCurrent = Paddle.transform.localScale;
	}

	void Update ()
	{
		scoreText.text = "Score: " + scorePlayer.ToString ();
		lifeText.text = "Lives: " + lifePlayer.ToString ();

		if (lifePlayer == 0) 
		{
			endgameMenu.gameObject.SetActive (true);
			Time.timeScale = 0.0f;
			endgameText.text = "Game Over!";
		}

		if (scorePlayer == 224) 
		{
			endgameMenu.gameObject.SetActive (true);
			Time.timeScale = 0.0f;
			endgameText.text = "Victory!";
		}
	}

	//function to delay
	IEnumerator Delay (float time)
	{
		yield return new WaitForSeconds (time);
	}

	//function to initiate ball movement
	public void ballStart()
	{
		StartCoroutine (Delay(3));

		float initialForce = Random.Range (-100, 100);
		rb2d.AddForce (new Vector2 (initialForce, 200));
	}

	//function to reset ball position and speed
	public void ballReset ()
	{
		ballVelocity = Vector2.zero;
		rb2d.velocity = ballVelocity;
		transform.position = new Vector2 (0, -3);
		growPaddle ();
		Invoke ("ballStart", 3.0f);
	}

	public void gameRestart ()
	{
		scorePlayer = 0;
		lifePlayer = 3;
		Paddle.transform.position = new Vector2 (0, -4);
		ballReset ();
		brickReset ();
		pauseMenu.gameObject.SetActive (false);
		endgameMenu.gameObject.SetActive (false);
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

	//sets paddle size to half if it is at full size
	private void shrinkPaddle ()
	{
		if (paddleScaleCurrent == paddleScaleOriginal)
			{
				Paddle.transform.localScale = new Vector2 (0.3f, 0.4f);
			}
	}

	//sets paddle size to full it is at half size
	private void growPaddle()
	{
		if (paddleScaleCurrent != paddleScaleOriginal) 
		{
			Paddle.transform.localScale = new Vector2 (0.6f, 0.4f);
		}
	}

	//function to reset all bricks
	private void brickReset ()
	{
		for (int j = 0; j < BrickRed.Length; j++) 
		{
			BrickRed [j].SetActive (true);
		}

		for (int j = 0; j < BrickOrange.Length; j++) 
		{
			BrickOrange [j].SetActive (true);
		}

		for (int j = 0; j < BrickYellow.Length; j++) 
		{
			BrickYellow [j].SetActive (true);
		}

		for (int j = 0; j < BrickGreen.Length; j++) 
		{
			BrickGreen [j].SetActive (true);
		}
	}

	//collision detection
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.collider.CompareTag ("Player")) 
		{
			ballVelocity.x = (rb2d.velocity.x/2.0f) + (other.collider.attachedRigidbody.velocity.x/4.0f);
			ballVelocity.y = rb2d.velocity.y * 1.1f;
			rb2d.velocity = ballVelocity;
		}

		else if (other.gameObject.CompareTag("Green Brick"))
		{
			other.gameObject.SetActive(false);
			scorePlayer = scorePlayer + 1;
		}

		else if (other.gameObject.CompareTag("Yellow Brick"))
		{
			other.gameObject.SetActive(false);
			scorePlayer = scorePlayer + 3;
		}

		else if (other.gameObject.CompareTag("Orange Brick"))
		{
			other.gameObject.SetActive(false);
			scorePlayer = scorePlayer + 5;
		}

		else if (other.gameObject.CompareTag("Red Brick"))
		{
			other.gameObject.SetActive(false);
			scorePlayer = scorePlayer + 7;
			shrinkPaddle ();
		}

		else if (other.gameObject.CompareTag("Top Wall"))
		{
			shrinkPaddle ();
		}
		
		else if (other.gameObject.CompareTag("Bottom Wall"))
		{
			ballReset ();
			lifePlayer = lifePlayer - 1;
		}

	}
}
