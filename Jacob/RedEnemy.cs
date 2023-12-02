using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class RedEnemy : Enemy
{

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public override void Attack()
    {
        Mass playerMass = player.GetNode<Mass>("Mass");
        playerMass.TakeDamage((uint)damage);
        base.Attack();
    }
}
