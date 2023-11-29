using Godot;
using System;

public partial class playerData : Node
{
    public enum Class
    {
        Tanker,
        Soldier,
        Sprinter
    }

    [Export] public Class playerClass;
    [Export] public float moveSpeed = 600.0f;
    [Export] public int maxMass = 100;
    [Export] public float bulletSpeed = 600.0f;
    [Export] public float bulletsPerSecond = 5.0f;
    [Export] public float bulletDamage = 30.0f;
    [Export] public int massPerBullet = 5;

    public int kills = 0;
    public int experience = 0;
}
