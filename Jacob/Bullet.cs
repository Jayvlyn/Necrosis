using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : RigidBody2D
{
	CharacterBody2D player;
	playerData data;
	private float travel;
	public uint mass = 5;
	private float bulletDamage;
	public bool damages = true;
	public bool pickup = false;
	public float pickupRange;
	private float playerDistance;

	public void Init(uint mass, playerData data)
	{ 
		this.mass = mass;
		this.data = data;
		player = (CharacterBody2D)data.GetParent();
	}
	public override void _Ready()
	{
		travel = data.bulletTravel;
		bulletDamage = data.bulletDamage;
		pickupRange = data.massPickupRange;

        GetChild(1).GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        Timer timer = GetNode<Timer>("CollisionTimer");
		timer.Timeout += () => EnableCollision();
	}

    public override void _Process(double delta)
    {
        base._Process(delta);

		playerDistance = (player.GlobalPosition - GlobalPosition).Length();
		if (playerDistance < pickupRange && !damages) pickup = true;
		else pickup = false;

		LinearVelocity *= travel;

		if (pickup) 
		{
			MoveTowardsPlayer(delta);
        }

		if (LinearVelocity.Length() < 20 && damages) damages = false;
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

	private void MoveTowardsPlayer(double delta)
	{
        Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		LinearVelocity += direction * ((2000 - playerDistance) * (float)delta);
    }

    public async void _on_bullet_area_body_entered(Node2D body)
	{
		if (body.IsInGroup("Enemy") && damages)
		{
			Health enemyHealth = body.GetNode<Health>("Health");
			Enemy enemy = (Enemy)body;
            enemyHealth.Damage(bulletDamage * (1+(0.1f * mass)));
			if (enemyHealth.health <= 0 && !enemy.dead)
			{
				enemy.dead = true;
				body.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("death");
				body.GetNode<Timer>("DeathTimer").Start();

				await ToSignal(body.GetNode<Timer>("DeathTimer"), "timeout");
				body.QueueFree();
			}
		}

		if (body.IsInGroup("Player") && pickup)
		{
			body.GetNode<Mass>("Mass").GainMass(mass);
			//Debug.WriteLine("Mass: " + body.GetNode<Mass>("Mass").GetMass());
			QueueFree();

		}
	}
}
