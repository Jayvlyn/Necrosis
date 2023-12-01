using Godot;
using System;

public partial class EnemyBullet : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
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

        }
    }
}
