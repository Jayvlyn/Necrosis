using Godot;
using System;


public partial class PlayerController : CharacterBody2D
{
	[Export] public float speed = 500.0f;

	private Vector2 input = Vector2.Zero;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		// Rotates player to look at the mouse position
		LookAt(GetGlobalMousePosition());
		ProcessInput();
		Velocity = input * speed;
		MoveAndSlide();
	}

	private void ProcessInput()
	{
		input = Input.GetVector("left", "right", "up", "down");
		// Normalize input vector to prevent faster diagonal movement
		input = input.Normalized();
	}


	private void ProcessInput()
	{
		input = Input.GetVector("left", "right", "up", "down");

		// Normalize input vector to prevent faster diagonal movement
		input = input.Normalized();
	}



	private void _on_area_2d_area_entered(Area2D area)
	{
		// Replace with function body.
	}


	private void _on_area_2d_area_exited(Area2D area)
	{
		// Replace with function body.
	}
}
