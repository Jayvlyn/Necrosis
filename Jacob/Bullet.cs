using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : RigidBody2D
{
	public int mass = 5;

	public override void _Ready()
	{
		//Timer timer = GetNode<Timer>("DestroyTimer");
		// Once timer on bullet runs out, it destroys itself
		//timer.Timeout += () => QueueFree();
	}

	public void _on_area_2d_body_entered(Node2D body)
	{
		if (body.IsInGroup("Enemy"))
		{
			body.GetNode<Health>("Health").Damage(mass);
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
