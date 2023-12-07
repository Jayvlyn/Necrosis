using Godot;
using System;
using System.Diagnostics;

public partial class playerData : Node
{
    [Export] public Global.Class playerClass;
    [Export] public Texture2D soldierSprite;
    [Export] public Texture2D tankerSprite;
    [Export] public Texture2D sprinterSprite;

    [Export] public SpriteFrames soldierAnim;
    [Export] public SpriteFrames tankerAnim;
    [Export] public SpriteFrames sprinterAnim;

    [Export] public Resource soldierCursor;
    [Export] public Resource tankerCursor;
    [Export] public Resource sprinterCursor;

    private PlayerController playerController;
    private Mass playerMass;

    // Player attributes
    [Export(PropertyHint.Range, "0,2000")] public float moveSpeed = 600.0f;
    [Export(PropertyHint.Range, "0,10000")] public uint maxMass = 100;
    [Export(PropertyHint.Range, "0,10000")] public float bulletSpeed = 600.0f;
    [Export(PropertyHint.Range, "0.001,60")] public float bulletsPerSecond = 5.0f;
    [Export(PropertyHint.Range, "0,1000")] public float bulletDamage = 5.0f;
    [Export(PropertyHint.Range, "1,1000")] public uint massPerBullet = 5;
    [Export(PropertyHint.Range, "0.9,1")] public float bulletTravel = 0.95f;
    [Export(PropertyHint.Range, "100,1000")] public float massPickupRange = 150f;

    public bool dead = false;

    // Stats
    public int kills = 0;
    public int deaths = 0;

    // Leveling
    [Export(PropertyHint.Range, "1,50")] public uint levelScaling = 2; // how large the expNeeded increases per level
    [Export(PropertyHint.Range, "1,1000")] public uint level1Cost = 100;
    public uint level = 1;
    public uint experience = 0;

    public override void _Ready()
    {
        playerController = (PlayerController)GetParent();
        playerMass = (Mass)GetParent().GetNode<Mass>("Mass");

        playerClass = Global.GetInstance().selectedClass;

        Sprite2D playerSprite = GetParent().GetNode<Sprite2D>("Sprite2D");
        AnimatedSprite2D playerAnim = GetParent().GetNode<AnimatedSprite2D>("Sprite");
        switch(playerClass)
        {
            case Global.Class.Sprinter:
                playerSprite.Texture = (Texture2D)sprinterSprite;
                playerAnim.SpriteFrames = sprinterAnim;
                Input.SetCustomMouseCursor(sprinterCursor, Input.CursorShape.Arrow, new Vector2(32, 32));
                moveSpeed = 700;
                maxMass = 70;
                bulletSpeed = 700;
                bulletsPerSecond = 7;
                break;
            case Global.Class.Tanker:
                playerSprite.Texture = (Texture2D)tankerSprite;
                playerAnim.SpriteFrames = tankerAnim;
                Input.SetCustomMouseCursor(tankerCursor, Input.CursorShape.Arrow, new Vector2(32, 32));
                moveSpeed = 400;
                maxMass = 100;
                bulletSpeed = 500;
                bulletsPerSecond = 5;
                break;
            case Global.Class.Soldier:
                playerSprite.Texture = (Texture2D)soldierSprite;
                playerAnim.SpriteFrames = soldierAnim;
                Input.SetCustomMouseCursor(soldierCursor, Input.CursorShape.Arrow, new Vector2(32, 32));
                moveSpeed = 600;
                maxMass = 100;
                bulletSpeed = 600;
                bulletsPerSecond = 6;
                break;
        }
    }

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

    public void UpdateData()
    {
        playerController.UpdateData();
        playerMass.UpdateData();
        // Mass pickup range & bullet dmg grabbed from playerData when they are spawned, nothing to update
    }
}