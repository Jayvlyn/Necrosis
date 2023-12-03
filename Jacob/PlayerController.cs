using Godot;
using System;
using System.Diagnostics;

public partial class PlayerController : CharacterBody2D
{
	playerData data;
	private float speed;


	private Vector2 input = Vector2.Zero;


	public override void _Ready()
	{
		data = (playerData)GetChild(0); // playerData should be at 0
		speed = data.moveSpeed;

	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
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


}
