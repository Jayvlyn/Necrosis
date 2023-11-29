using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : RigidBody2D
{
	playerData data;
	private float travel;
	public uint mass = 5;

	public void Init(uint mass, playerData data)
	{ 
		this.mass = mass;
		this.data = data;
	}
	public override void _Ready()
	{
		travel = data.bulletTravel;
        GetChild(1).GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        Timer timer = GetNode<Timer>("CollisionTimer");
		timer.Timeout += () => EnableCollision();
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		LinearVelocity *= travel;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		Vector2 deltaVelocity = new Vector2(LinearVelocity.X * (float)delta, LinearVelocity.Y * (float)delta);
		KinematicCollision2D collisionInfo = MoveAndCollide(deltaVelocity);

		// Bounce is in try-catch because it was producing error despite being functional, try-catch supresses the error
		try { if (collisionInfo != null) LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal()); }
		catch (Exception e) {Debug.WriteLine("Error in bullet bouncing: " + e.Message);}
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
