using Godot;
using System;

public partial class playerData : Node
{
    // Player class
    public enum Class
    {
        Tanker,
        Soldier,
        Sprinter
    }

    [Export] public Class playerClass;


    // Player attributes
    [Export] public float moveSpeed = 600.0f;
    [Export] public uint maxMass = 100;
    [Export] public float bulletSpeed = 600.0f;
    [Export] public float bulletsPerSecond = 5.0f;
    [Export] public float bulletDamage = 30.0f;
    [Export] public uint massPerBullet = 5;
    

    // Stats
    public int kills = 0;


    // Leveling
    [Export] public int levelScaling = 2; // how large the expNeeded increases per level
    [Export] public int level1Cost = 100;
    public int level = 1;
    public uint experience = 0;

    public void GainExp(uint amount)
    {
        double expNeeded = level1Cost * Math.Pow(level, levelScaling); // multiplies a base level cost by an exponentially growing level scaling
        if (experience + amount > expNeeded)
        { 
            LevelUp();
        }
        else
        {
            experience += amount;
        }
    }

    public void LevelUp()
    {
        experience = 0; // Reset experience on level up
        level++;
    }
}
