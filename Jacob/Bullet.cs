using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : RigidBody2D
{
	protected CharacterBody2D player;
	protected playerData data;
	protected float travel;
	protected float bulletDamage;
	protected float playerDistance;

	public uint mass = 5;
	public bool damages = true;

	public void Init(uint mass, playerData data = null)
	{ 
		this.mass = mass;
		if (data != null)
		{
			this.data = data;
			player = (CharacterBody2D)data.GetParent();
		}
	}

    public override void _Ready()
	{
        GetChild(1).GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        Timer timer = GetNode<Timer>("CollisionTimer");
		timer.Timeout += () => EnableCollision();
	}

    public override void _Process(double delta)
    {
        base._Process(delta);

		LinearVelocity *= travel;

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

	protected void MoveTowardsPlayer(double delta)
	{
        Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		LinearVelocity += direction * ((2000 - playerDistance) * (float)delta);
    }
}
