using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class RedEnemy : Enemy
{

	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		//if(GetNode<Timer>("DeathTimer").TimeLeft != 0) Debug.WriteLine(GetNode<Timer>("DeathTimer").TimeLeft);

    }

	public override void _PhysicsProcess(double delta)
	{
		if (player != null && !dead)
		{
			LookAt(-player.GlobalPosition);
			Vector2 dir = (player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = -dir * speed;
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		base._PhysicsProcess(delta);
	}

	public override void Attack()
	{
		Mass playerMass = player.GetNode<Mass>("Mass");
		playerMass.TakeDamage((uint)damage);
		base.Attack();
	}
}
