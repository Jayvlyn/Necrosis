using Godot;
using System;
using System.Diagnostics;

public abstract partial class Enemy : CharacterBody2D
{
    protected PlayerController player;
    protected Health health;

    [Export] public float speed = 250.0f;
    [Export] public float damage = 10.0f;
    [Export] public float attacksPerSecond = 2.0f;
    [Export] public int expValue = 10;

    public bool dead = false;
    protected float attackSpeed;
    protected float attackTimer;
    protected bool withinAttackRange = false;

    public override void _Ready()
    {
        player = (PlayerController)GetTree().Root.GetNode("PrototypeLevel").GetNode("PlayerController");
        health = (Health)GetChild(0);

        attackSpeed = 1 / attacksPerSecond;
        attackTimer = attackSpeed;
    }

    public override void _Process(double delta)
    {
        if (withinAttackRange && attackTimer <= 0 && !dead)
        {
            Attack();
        }
        else
        {
            attackTimer -= (float)delta;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player != null && !dead)
        {
            LookAt(player.GlobalPosition);
            Vector2 dir = (player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = dir * speed;
        }
        else
        {
            Velocity = Vector2.Zero;
        }

        MoveAndSlide();
    }

    public virtual void Attack()
    {
        attackTimer = attackSpeed;
    }

    public void OnAttackRangeEnter(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            withinAttackRange = true;
        }
    }

    public void OnAttackRangeExit(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            withinAttackRange = false;
            attackTimer = attackSpeed;
        }
    }
}
