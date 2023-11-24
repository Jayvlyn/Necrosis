using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class WhiteEnemy : CharacterBody2D
{
	PlayerController player;

	[Export] float speed = 250.0f;
	[Export] float damage = 10.0f;
	[Export] float attacksPerSecond = 2.0f;

	float attackSpeed;
	float attackTimer;
	bool withinAttackRange = false;

	public override void _Ready()
	{
		player = (PlayerController)GetTree().Root.GetNode("PrototypeLevel").GetNode("PlayerController");

		attackSpeed = 1 / attacksPerSecond;
		attackTimer = attackSpeed;
	}

	public override void _Process(double delta)
	{
		if (withinAttackRange && attackTimer <= 0)
		{
			Attack();
			attackTimer = attackSpeed;
		}
		else
		{
			attackTimer -= (float)delta;
		}

		
	}

	public override void _PhysicsProcess(double delta)
	{
		if(player != null)
		{
			LookAt(player.GlobalPosition);
			Vector2 dir = (player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = dir * speed;
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		MoveAndSlide();
	}

	public void Attack()
	{
		Mass playerMass= player.GetNode<Mass>("Mass");
		playerMass.LoseMass((int)damage);
	}

	public void OnAttackRangeEnter(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			withinAttackRange = true;
		}
	}

	public void OnAttackRangeExit(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			withinAttackRange = false;
			attackTimer = attackSpeed;
		}
	}
}
