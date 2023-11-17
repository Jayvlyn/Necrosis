using Godot;
using System;

public partial class Gun : Node2D
{
	[Export] PackedScene bulletScene;
	[Export] float bulletSpeed = 600.0f;
	[Export] float bulletsPerSecond = 5.0f;
	[Export] float bulletDamage = 30.0f;

	float fireRate;
	float fireTimer = 0.0f;

    public override void _Ready()
    {
        fireRate = 1 / bulletsPerSecond;
    }

    public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed("shoot") && fireTimer > fireRate)
        {
            RigidBody2D bullet = bulletScene.Instantiate<RigidBody2D>();

            bullet.Rotation = GlobalRotation;
            bullet.GlobalPosition = GlobalPosition;
            bullet.LinearVelocity = bullet.Transform.X * bulletSpeed;

            GetTree().Root.AddChild(bullet);
        }
        else
        {
            fireTimer += (float)delta;
        }
    }
}
