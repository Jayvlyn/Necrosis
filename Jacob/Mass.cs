using Godot;
using System;
using System.Diagnostics;
using System.Net;

public partial class Mass : Node2D // Mass acts as a 3-in-1 to represent the health, size, and ammo for the player
{
	[Export] public int maxMass = 100;
    private int currentMass;

    // Shooting mass
    [Export] PackedScene bulletScene;
    [Export] float bulletSpeed = 600.0f;
    [Export] float bulletsPerSecond = 5.0f;
    [Export] float bulletDamage = 30.0f;
    [Export] int massPerBullet = 5;

    float fireRate;
    float fireTimer;

    public override void _Ready()
	{
        currentMass = maxMass;
        fireRate = 1 / bulletsPerSecond;
    }

	public override void _Process(double delta)
	{
		ProcessShooting(delta);
    }

    public void ProcessShooting(double delta)
    {
        if (Input.IsActionPressed("shoot") && fireTimer <= 0 && CanShootMass())
        {
            // Start fireTimer, this disables ability to shoot again until fire timer reaches 0 again
            fireTimer = fireRate;

            // Deduct mass
            LoseMass(massPerBullet);

            // Create and fire bullet
            RigidBody2D bullet = bulletScene.Instantiate<RigidBody2D>();

            bullet.Rotation = GlobalRotation;
            bullet.GlobalPosition = GlobalPosition;
            bullet.LinearVelocity = bullet.Transform.X * bulletSpeed;
            //Bullet b = (Bullet)bullet.GetNode("Bullet");
            //b.mass = massPerBullet;

            GetTree().Root.AddChild(bullet);
        }
        else if (fireTimer > 0)
        {
            fireTimer -= (float)delta;
        }
    }

    public bool CanShootMass()
    {
        return (currentMass - massPerBullet) > 0;
    }

    public void TakeDamage(int damage)
    {
        LoseMass(damage);

        // spit out mass that you can pick back up
    }

    public void LoseMass(int amount)
    {
        currentMass -= amount;

        // do size reduction

    }

    public void GainMass(int amount)
    {
        currentMass += amount;
    }

}
