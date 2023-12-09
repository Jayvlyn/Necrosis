using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

    private CanvasLayer ui;
    private Panel upgradePanel;

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
    private ProgressBar bloodBar;
    public double expNeeded;
    public uint level = 1;
    public uint experience = 0;
    private bool increaseExpBar = false;
    private int expGained = 0;
    private float gainTimer = 0;

    // Skill Tree
    List<string> tier1Upgrades;
    List<string> tier2Upgrades;
    List<string> tier3Upgrades;
    List<string> tier4Upgrades;
    List<string> tier5Upgrades;
    List<string> upgradeChoices;
    List<string> leftTreeUpgrades;
    List<string> rightTreeUpgrades;
    int lastUpgrade;

    [Export] PackedScene biggerBodyT1Upgrade;

    public override void _Ready()
    {
        playerController = (PlayerController)GetParent();
        playerMass = (Mass)GetParent().GetNode<Mass>("Mass");

        ui = GetParent().GetNode<CanvasLayer>("UI");
        upgradePanel = ui.GetNode<Panel>("UpgradePanel");
        upgradePanel.Hide();
        bloodBar = ui.GetNode<ProgressBar>("BloodBar");

        expNeeded = level1Cost * Math.Pow(level, levelScaling);

        playerClass = Global.GetInstance().selectedClass;

        Sprite2D playerSprite = GetParent().GetNode<Sprite2D>("Sprite2D");
        AnimatedSprite2D playerAnim = GetParent().GetNode<AnimatedSprite2D>("Sprite");
        switch (playerClass)
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

        // Upgrade set
        // |Tier 1|
        tier1Upgrades.Add("BiggerBody");
        tier1Upgrades.Add("BiggerBullets");
        tier1Upgrades.Add("FartherBullets");
        tier1Upgrades.Add("SpeedingBullet");
        tier1Upgrades.Add("StrongerBullets");
        tier1Upgrades.Add("Zippy");

        // |Tier 2|
        tier2Upgrades.Add("ArmorPiercing");
        tier2Upgrades.Add("ArmorUp");
        tier2Upgrades.Add("BetterFireRate");
        tier2Upgrades.Add("BiggerBody");
        tier2Upgrades.Add("BiggerBullets");
        tier2Upgrades.Add("Energized");
        tier2Upgrades.Add("FartherBullets");
        tier2Upgrades.Add("Rage");
        tier2Upgrades.Add("Sniper");
        tier2Upgrades.Add("Steroids");
        tier2Upgrades.Add("StrongerBullets");
        tier2Upgrades.Add("Zippy");

        // |Tier 3|
        tier3Upgrades.Add("ArmorPiercing");
        tier3Upgrades.Add("ArmorUp");
        tier3Upgrades.Add("BetterFireRate");
        tier3Upgrades.Add("Energized");
        tier3Upgrades.Add("Lightspeed");
        tier3Upgrades.Add("MassiveBullets");
        tier3Upgrades.Add("MassiveGrowth");
        tier3Upgrades.Add("Rage");
        tier3Upgrades.Add("Sniper");
        tier3Upgrades.Add("Steroids");
        tier3Upgrades.Add("Streamlined");
        tier3Upgrades.Add("Trained");

        // |Tier 4|
        tier4Upgrades.Add("Careful");
        tier4Upgrades.Add("CQC");
        tier4Upgrades.Add("HeavyBullets");
        tier4Upgrades.Add("HollowPoints");
        tier4Upgrades.Add("Lightspeed");
        tier4Upgrades.Add("MassiveBullets");
        tier4Upgrades.Add("MassiveGrowth");
        tier4Upgrades.Add("Patience");
        tier4Upgrades.Add("Quicker");
        tier4Upgrades.Add("RapidFire");
        tier4Upgrades.Add("Streamlined");
        tier4Upgrades.Add("Trained");

        // |Tier 5|
        tier5Upgrades.Add("BehemothBullets");
        tier5Upgrades.Add("BulkUp");
        tier5Upgrades.Add("Careful");
        tier5Upgrades.Add("Chunky");
        tier5Upgrades.Add("CQC");
        tier5Upgrades.Add("HEATAmmo");
        tier5Upgrades.Add("HeavyBullets");
        tier5Upgrades.Add("HollowPoints");
        tier5Upgrades.Add("Patience");
        tier5Upgrades.Add("Quicker");
        tier5Upgrades.Add("RapidFire");
        tier5Upgrades.Add("Spray&Pray");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (increaseExpBar)
        {
            bloodBar.Value = Mathf.Lerp((experience - expGained) / expNeeded, experience / expNeeded, gainTimer);

            gainTimer += (float)delta;
            if (gainTimer > 1)
            {
                increaseExpBar = false;
                gainTimer = 0;
            }
        }
    }

    public void GainExp(uint amount)
    {
        expNeeded = level1Cost * Math.Pow(level, levelScaling); // multiplies a base level cost by an exponentially growing level scaling
        if (experience + amount > expNeeded)
        {
            LevelUp();
            bloodBar.Value = 0;
        }
        else
        {
            experience += amount;

            if (increaseExpBar) // in the middle of increasing already
            {
                expGained += (int)amount;
            }
            else
            {
                expGained = (int)amount;
                increaseExpBar = true;
            }
        }
    }

    public void LevelUp()
    {
        experience = 0; // Reset experience on level up
        level++;
        upgradePanel.Visible = true;
        bloodBar.Visible = false;
        GetTree().Paused = true;
        SetTree();
        SetScreen();
    }

    public void UpdateData()
    {
        playerController.UpdateData();
        playerMass.UpdateData();
        // Mass pickup range & bullet dmg grabbed from playerData when they are spawned, nothing to update
    }

    public void SetTree()
    {
        Random random = new Random();
        int randomUpgrade;
        switch (level)
        {
            case 2:
                // Set Choices
                upgradeChoices.Clear();
                randomUpgrade = random.Next(0, tier1Upgrades.Count() - 1);
                upgradeChoices[0] = tier1Upgrades[randomUpgrade];
                tier1Upgrades.RemoveAt(randomUpgrade);
                upgradeChoices[1] = tier1Upgrades[random.Next(0, tier1Upgrades.Count() - 1)];
                tier1Upgrades.Clear();

                // Set follow-up trees
                for (int i = 0; i < 4; i++)
                {
                    randomUpgrade = random.Next(0, tier2Upgrades.Count() - 1);
                    if (i < 2)
                    {
                        leftTreeUpgrades.Add(tier2Upgrades[randomUpgrade]);
                    }
                    else
                    {
                        rightTreeUpgrades.Add(tier2Upgrades[randomUpgrade]);
                    }
                    tier2Upgrades.RemoveAt(randomUpgrade);
                }
                tier2Upgrades.Clear();

                break;
            case 3:
                // Set Choices
                upgradeChoices.Clear();
                if (lastUpgrade == 0)
                {
                    upgradeChoices = leftTreeUpgrades;
                }
                else if (lastUpgrade == 1)
                {
                    upgradeChoices = rightTreeUpgrades;
                }
                leftTreeUpgrades.Clear();
                rightTreeUpgrades.Clear();

                // Set follow-up trees
                for (int i = 0; i < 4; i++)
                {
                    randomUpgrade = random.Next(0, tier3Upgrades.Count() - 1);
                    if (i < 2)
                    {
                        leftTreeUpgrades.Add(tier3Upgrades[randomUpgrade]);
                    }
                    else
                    {
                        rightTreeUpgrades.Add(tier3Upgrades[randomUpgrade]);
                    }
                    tier3Upgrades.RemoveAt(randomUpgrade);
                }
                tier3Upgrades.Clear();

                break;
            case 4:
                // Set Choices
                upgradeChoices.Clear();
                if (lastUpgrade == 0)
                {
                    upgradeChoices = leftTreeUpgrades;
                }
                else if (lastUpgrade == 1)
                {
                    upgradeChoices = rightTreeUpgrades;
                }
                leftTreeUpgrades.Clear();
                rightTreeUpgrades.Clear();

                // Set follow-up trees
                for (int i = 0; i < 4; i++)
                {
                    randomUpgrade = random.Next(0, tier4Upgrades.Count() - 1);
                    if (i < 2)
                    {
                        leftTreeUpgrades.Add(tier4Upgrades[randomUpgrade]);
                    }
                    else
                    {
                        rightTreeUpgrades.Add(tier4Upgrades[randomUpgrade]);
                    }
                    tier4Upgrades.RemoveAt(randomUpgrade);
                }
                tier4Upgrades.Clear();

                break;
            case 5:
                // Set Choices
                upgradeChoices.Clear();
                if (lastUpgrade == 0)
                {
                    upgradeChoices = leftTreeUpgrades;
                }
                else if (lastUpgrade == 1)
                {
                    upgradeChoices = rightTreeUpgrades;
                }
                leftTreeUpgrades.Clear();
                rightTreeUpgrades.Clear();

                // Set follow-up trees
                for (int i = 0; i < 4; i++)
                {
                    randomUpgrade = random.Next(0, tier5Upgrades.Count() - 1);
                    if (i < 2)
                    {
                        leftTreeUpgrades.Add(tier5Upgrades[randomUpgrade]);
                    }
                    else
                    {
                        rightTreeUpgrades.Add(tier5Upgrades[randomUpgrade]);
                    }
                    tier5Upgrades.RemoveAt(randomUpgrade);
                }
                tier5Upgrades.Clear();

                break;
            case 6:
                // Set Choices
                upgradeChoices.Clear();
                if (lastUpgrade == 0)
                {
                    upgradeChoices = leftTreeUpgrades;
                }
                else if (lastUpgrade == 1)
                {
                    upgradeChoices = rightTreeUpgrades;
                }
                leftTreeUpgrades.Clear();
                rightTreeUpgrades.Clear();

                //Set follow-up trees


                break;
            default:
                upgradePanel.Hide();
                GD.Print("No Upgrade");
                break;
        }
    }

    public void SetScreen()
    {
        GetParent().GetNode<Upgrade>("LeftChoice").Free();
        Upgrade leftChoice = biggerBodyT1Upgrade.Instantiate<Upgrade>();
        leftChoice.Position = GetParent().GetNode<Node2D>("LeftChoicePos").Position;
    }
}