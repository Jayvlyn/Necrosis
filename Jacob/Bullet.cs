using Godot;
using System;

public partial class Bullet : RigidBody2D
{
    [Export] public float damage = 10.0f;

    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("DestroyTimer");
        // Once timer on bullet runs out, it destroys itself
        timer.Timeout += () => QueueFree();
    }

    public void OnBodyEntered(Node2D body)
    {
        if(body.IsInGroup("Enemy"))
        {
            body.GetNode<Health>("Health").Damage(damage);
        }

        QueueFree(); // Destroys bullet on first collision
    }
}
