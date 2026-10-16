using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 60.0f;
	public const float JumpVelocity = -240.0f;

	private AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("left", "right");
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (IsOnFloor())
		{
			if (direction > 0)
			{
				animatedSprite.FlipH = false;
				animatedSprite.Play("walk");
			}
			else if (direction < 0)
			{
				animatedSprite.FlipH = true;
				animatedSprite.Play("walk");
			}
			else
			{
				animatedSprite.Play("idle");
			}
		}
		else
		{
			animatedSprite.Play("jump");
			if (direction > 0)
			{
				animatedSprite.FlipH = false;
			}
			else if (direction < 0)
			{
				animatedSprite.FlipH = true;
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
