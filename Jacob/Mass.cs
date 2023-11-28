using Godot;
using System;
using System.Diagnostics;
using System.Net;

public partial class Mass : Node2D // Mass acts as a 3-in-1 to represent the health, size, and ammo for the player
{
    playerData data;

    [Export] public CharacterBody2D player;
    private int maxMass;
    private int currentMass;

    // Shooting mass
    [Export] PackedScene bulletScene;
    private float bulletSpeed;
    private float bulletsPerSecond;
    private float bulletDamage;
    private int massPerBullet;

    float fireRate;
    float fireTimer;

    public override void _Ready()
	{
        data = (playerData)GetParent().GetChild(0);
        maxMass = data.maxMass;
        bulletDamage = data.bulletDamage;
        bulletSpeed = data.bulletSpeed;
        bulletsPerSecond = data.bulletsPerSecond;
        massPerBullet = data.massPerBullet;

        currentMass = maxMass;
        fireRate = 1 / bulletsPerSecond;
    }

	public override void _Process(double delta)
	{
		ProcessShooting(delta);
        Debug.WriteLine(currentMass);
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
        UpdateSize();
    }

    public void GainMass(int amount)
    {
        if(currentMass + amount <= maxMass)
        {
            currentMass += amount;
        }
        else
        {
            currentMass = maxMass;
        }
        UpdateSize();
    }

    public int GetMass() { return currentMass; }

    public void UpdateSize()
    {
        float scaleFactor = currentMass / (float)maxMass;
        Vector2 newScale = new Vector2(scaleFactor, scaleFactor);
        player.Scale = newScale;
    }

}
