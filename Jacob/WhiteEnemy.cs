using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class WhiteEnemy : Enemy
{
    [Export] PackedScene bulletScene;
    [Export] public float bulletSpeed = 300.0f;
    private Node2D bulletSpawn;

    public override void _Ready()
	{
		base._Ready();
        bulletSpawn = (Node2D)GetChild(1);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
        if(player != null && !dead)
        {
            LookAt(player.GlobalPosition);
            if(!withinAttackRange)
            {
                Vector2 dir = (player.GlobalPosition - GlobalPosition).Normalized();
                Velocity = dir * speed;
            }
            else
            {
                Velocity = Vector2.Zero;
            }
        }
        else
        {
            Velocity = Vector2.Zero;
        }

        base._PhysicsProcess(delta);
	}

    public override void Attack()
    {
        // Create and fire bullet
        Bullet bullet = bulletScene.Instantiate<Bullet>();
        bullet.Init((uint)damage);

        bullet.Rotation = GlobalRotation;
        bullet.GlobalPosition = bulletSpawn.GlobalPosition;
        bullet.LinearVelocity = bullet.Transform.X * bulletSpeed;

        GetTree().Root.AddChild(bullet);


        base.Attack();
    }
}
