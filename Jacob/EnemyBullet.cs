using Godot;
using System;
using System.Diagnostics;

public partial class EnemyBullet : Bullet
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        base._Ready();
        travel = 0.995f;

        Timer despawnTimer = GetNode<Timer>("DespawnTimer");
        despawnTimer.Timeout += () => Despawn();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

    public async void _on_bullet_area_body_entered(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
			Mass playerMass = body.GetNode<Mass>("Mass");
			playerMass.TakeDamage(mass);
        }
    }
}
