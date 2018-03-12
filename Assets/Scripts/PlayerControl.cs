using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public float speed;
	public float boundX, boundXsmall;

	private Rigidbody2D rb2d;
	private Vector2 paddleScaleOriginal, paddleScaleCurrent;

	private void Start () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		paddleScaleOriginal = transform.localScale;
	}
	
	private void Update ()
	{
		paddleScaleCurrent = transform.localScale;
	}

	private void FixedUpdate () 
	{
		//Changes velocity based on input
		var paddleVelocity = rb2d.velocity;

		if (Input.GetKey (moveRight))
		{
			paddleVelocity.x = speed;
		}

		if (Input.GetKey (moveLeft))
		{
			paddleVelocity.x = -speed;
		}

		else if (!Input.anyKey)
		{
			paddleVelocity.x = 0;
		}

		rb2d.velocity = paddleVelocity;

		//restricts play area
		var paddlePosition = transform.position;

		if (paddleScaleOriginal == paddleScaleCurrent) 
		{
			if (paddlePosition.x > boundX) 
			{
				paddlePosition.x = boundX;
			}

			if (paddlePosition.x < -boundX) 
			{
				paddlePosition.x = -boundX;
			}

			transform.position = paddlePosition;
		}

		if (paddleScaleOriginal != paddleScaleCurrent) 
		{
			if (paddlePosition.x > boundXsmall) 
			{
				paddlePosition.x = boundXsmall;
			}

			if (paddlePosition.x < -boundXsmall) 
			{
				paddlePosition.x = -boundXsmall;
			}

			transform.position = paddlePosition;
		}
	}
}
