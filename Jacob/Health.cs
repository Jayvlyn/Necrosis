using Godot;
using System;

public partial class Health : Node2D
{
    [Export] AudioStreamPlayer2D hurtSound;
    [Export] public float maxHealth = 100f;
	public float health;

	public override void _Ready()
	{
		health = maxHealth;
	}

	public void Damage(float damage)
	{
		hurtSound.Play();
		health -= damage;
	}
}
