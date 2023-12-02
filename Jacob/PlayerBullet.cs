using Godot;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

public partial class PlayerBullet : Bullet
{
    public bool pickup = false;
    public float pickupRange;

    public override void _Ready()
	{
		base._Ready();
        travel = data.bulletTravel;
        bulletDamage = data.bulletDamage;
        pickupRange = data.massPickupRange;
    }

	public override void _Process(double delta)
	{
        base._Process(delta);

        playerDistance = (player.GlobalPosition - GlobalPosition).Length();
        if (playerDistance < pickupRange && !damages) pickup = true;
        else pickup = false;

        if (pickup) MoveTowardsPlayer(delta);
    }

    public async void _on_bullet_area_body_entered(Node2D body)
    {
        if (body.IsInGroup("Enemy") && damages)
        {
            Health enemyHealth = body.GetNode<Health>("Health");
            Enemy enemy = (Enemy)body;
            enemyHealth.Damage(bulletDamage * (1 + (0.1f * mass)));
            if (enemyHealth.health <= 0 && !enemy.dead)
            {
                enemy.dead = true;
                data.GainExp(enemy.expValue);
                //Debug.WriteLine("exp: " + data.experience);
                //Debug.WriteLine(data.level);
                body.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("death");
                if(!body.GetNode<Timer>("DeathTimer").IsStopped())body.GetNode<Timer>("DeathTimer").Start();

                await ToSignal(body.GetNode<Timer>("DeathTimer"), "timeout");
                body.QueueFree();
            }
        }

        if (body.IsInGroup("Player"))
        {
            body.GetNode<Mass>("Mass").GainMass(mass);
            QueueFree();
        }
    }
}
