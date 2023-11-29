using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : RigidBody2D
{
	public uint mass = 5;

	public override void _Ready()
	{
        GetChild(1).GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        Timer timer = GetNode<Timer>("CollisionTimer");
		timer.Timeout += () => EnableCollision();
	}

	public void EnableCollision()
	{
		GetChild(1).GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	public async void _on_area_2d_body_entered(Node2D body)
	{
		if (body.IsInGroup("Enemy"))
		{
			body.GetNode<Health>("Health").Damage(mass);
			if (body.GetNode<Health>("Health").health == 0)
			{
				body.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("death");
				body.GetNode<Timer>("DeathTimer").Start();

				await ToSignal(body.GetNode<Timer>("DeathTimer"), "timeout");
				body.QueueFree();
			}
			
		}

		if (body.IsInGroup("Player"))
		{
			body.GetNode<Mass>("Mass").GainMass(mass);
			Debug.WriteLine("Mass: " + body.GetNode<Mass>("Mass").GetMass());
			//Debug.WriteLine(body.GetNode<CharacterBody2D>("PlayerController"));
			//body.GetNode<CharacterBody2D>("PlayerController").kills++;
			QueueFree();

		}
	}
}
