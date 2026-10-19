using Godot;
using System;

public partial class Player : CharacterBody2D
{

	private enum PlayerState
	{
		Idle,
		Walking,
		Jumping,
	}

	private PlayerState state = PlayerState.Idle;

	public const float Speed = 60.0f;
	public const float JumpVelocity = -240.0f;

	private AnimatedSprite2D animatedSprite;
	private Vector2 velocity;
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		GD.Print(state);

		switch (state)
		{
			case PlayerState.Idle:
				idleState();
				break;
			case PlayerState.Walking:
				walkingState();
				break;
			case PlayerState.Jumping:
				jumpState();
				break;
		}

		// // Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}


		// if (IsOnFloor())
		// {
		// 	if (direction > 0)
		// 	{
		// 		animatedSprite.FlipH = false;
		// 		animatedSprite.Play("walk");
		// 	}
		// 	else if (direction < 0)
		// 	{
		// 		animatedSprite.FlipH = true;
		// 		animatedSprite.Play("walk");
		// 	}
		// 	else
		// 	{
		// 		animatedSprite.Play("idle");
		// 	}
		// }
		// else
		// {
		// 	animatedSprite.Play("jump");
		// 	if (direction > 0)
		// 	{
		// 		animatedSprite.FlipH = false;
		// 	}
		// 	else if (direction < 0)
		// 	{
		// 		animatedSprite.FlipH = true;
		// 	}
		// }

		Velocity = velocity;
		MoveAndSlide();
	}

	private void gotJumpState()
	{
		state = PlayerState.Jumping;
		animatedSprite.Play("jump");
	}

	private void gotWalkingState()
	{
		state = PlayerState.Walking;
		animatedSprite.Play("walk");
	}

	private void gotIdleState()
	{
		state = PlayerState.Idle;
		animatedSprite.Play("idle");
	}

	private void idleState()
	{
		move();
		if(velocity.X != 0)
		{
			gotWalkingState();
		}
	}

	private void walkingState()
	{
		move();
		if (velocity.X == 0)
		{
			gotIdleState();
		}
	}

	private void jumpState()
	{
		move();
	}

	private void move()
	{
		float direction = Input.GetAxis("left", "right");

		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (direction > 0)
		{
			animatedSprite.FlipH = false;
		}
		else if (direction < 0)
		{
			animatedSprite.FlipH = true;
		}
	}
}
