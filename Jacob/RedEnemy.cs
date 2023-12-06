using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class RedEnemy : Enemy
{
    private Random random = new Random();
	private Vector2 moveDir;

	private Vector2 lastPos;

	private bool runAway;
    public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		//if(GetNode<Timer>("DeathTimer").TimeLeft != 0) Debug.WriteLine(GetNode<Timer>("DeathTimer").TimeLeft);

    }

	public override void _PhysicsProcess(double delta)
	{
		if (player != null && !dead)
		{
			LookAt(-player.GlobalPosition);
			if(!runAway)
			{

				if (random.NextDouble() < 0.01 || GlobalPosition == lastPos) 
				{
					float randomAngle = (float)random.NextDouble() * Mathf.Pi * 2;

					moveDir = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
					moveDir = moveDir.Normalized();
				}
			}
			else
			{
				moveDir = -(player.GlobalPosition - GlobalPosition).Normalized();

			}

			Velocity = moveDir * speed;
			lastPos = GlobalPosition;
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		base._PhysicsProcess(delta);
	}

	public override void Attack()
	{
		Mass playerMass = player.GetNode<Mass>("Mass");
		playerMass.TakeDamage((uint)damage);
		base.Attack();
	}

	public void _on_flee_range_body_entered(Node2D body)
	{
        if (body.IsInGroup("Player"))
        {
            runAway = true;
        }
    }

	public void _on_flee_range_body_exited(Node2D body)
	{
        if (body.IsInGroup("Player"))
        {
            runAway = false;
        }
    }
}
