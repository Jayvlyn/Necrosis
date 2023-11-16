using Godot;
using System;


public partial class PlayerController : CharacterBody2D
{
	private float speed = 500.0f;

	private Vector2 velocity = Vector2.Zero;
	private Vector2 input = Vector2.Zero;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ProcessInput();
		MovePlayer(delta);
		LookAtMouse();
	}

	private void ProcessInput()
	{
		input = new Vector2();

		if (Input.IsActionPressed("up"))
		{
			input.Y -= 1;
		}
		if (Input.IsActionPressed("down"))
		{
			input.Y += 1;
		}
		if (Input.IsActionPressed("left"))
		{
			input.X -= 1;
		}
		if (Input.IsActionPressed("right"))
		{
			input.X += 1;
		}

		// Normalize input vector to prevent faster diagonal movement
		input = input.Normalized();
	}

	private void MovePlayer(double delta)
	{
		// Set velocity based on input and speed
		velocity = input * speed;
		// Update the player's position
		Position += velocity * (float)delta;
	}

	private void LookAtMouse()
	{
		Vector2 mousePos = GetGlobalMousePosition();

		// Angle between player character and mouse
		float angle = Mathf.Atan2(mousePos.Y - this.GlobalPosition.Y, mousePos.X - this.GlobalPosition.X);

		this.RotationDegrees = Mathf.RadToDeg(angle);
	}
}
