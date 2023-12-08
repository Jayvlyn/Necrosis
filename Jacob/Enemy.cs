using Godot;
using System;
using System.Diagnostics;

public abstract partial class Enemy : CharacterBody2D
{
    protected PlayerController player;
    protected playerData pd;
    protected Health health;

    [Export] AudioStreamPlayer2D deathSound;

    [Export] public float speed = 250.0f;
    [Export] public float damage = 10.0f;
    [Export] public float attacksPerSecond = 2.0f;
    [Export] public uint expValue = 10;

    public bool dead = false;
    protected float attackSpeed;
    protected float attackTimer;
    protected bool withinAttackRange = false;

    public override void _Ready()
    {
        player = (PlayerController)GetTree().Root.GetNode("PrototypeLevel").GetNode("PlayerController");
        pd = (playerData)player.GetChild(0);
        health = (Health)GetChild(0);

        attackSpeed = 1 / attacksPerSecond;
        attackTimer = attackSpeed;
    }

    public override void _Process(double delta)
    {
        if (!pd.dead)
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
    }

    public override void _PhysicsProcess(double delta)
    {
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

    public async void OnDeath()
    {
        deathSound.Play();
        dead = true;
        
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("death");
        //if(!body.GetNode<Timer>("DeathTimer").IsStopped())body.GetNode<Timer>("DeathTimer").Start();

        await ToSignal(GetTree().CreateTimer(0.6), "timeout"); //creates a timer and waits for its signal
        QueueFree();
    }
}
