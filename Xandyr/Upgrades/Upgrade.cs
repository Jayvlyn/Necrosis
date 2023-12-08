using Godot;
using System;

public partial class Upgrade : Button
{
    // Name of upgrade
    [Export] public string name;

    // Image for upgrade
    //[Export] public Sprite2D icon;

    // Upgrade Teir
	[Export(PropertyHint.Range, "1,5")] public int tier;

    // Stat changes (negative for decreasing stats)
    [Export] public float moveSpeedChange = 0.0f;
    [Export] public uint maxMassChange = 0;
    [Export] public float bulletSpeedChange = 0.0f;
    [Export] public float bpsChange = 0.0f;
    [Export] public float bulletDamageChange = 0.0f;
    [Export] public uint massPerBulletChange = 0;
    [Export] public float bulletTravelChange = 0.0f;
    [Export] public float massPickupRangeChange = 0.0f;

    // Player references 
    protected PlayerController player;
    protected playerData pd;

    private bool applied = false;

    public bool ApplyUpgrade()
    {
        if(pd != null)
        {
            pd.moveSpeed += moveSpeedChange;
            pd.maxMass += maxMassChange;
            pd.bulletSpeed += bulletSpeedChange;
            pd.bulletsPerSecond += bpsChange;
            pd.bulletDamage += bulletDamageChange;
            pd.massPerBullet += massPerBulletChange;

            if (pd.bulletTravel + bulletTravelChange <= 1) // bulletTravel shouldn't ever get higher than 1 (1 = infinite travel)
                pd.bulletTravel += bulletTravelChange; 
            else return false;

            pd.UpdateData(); // disperses data through other player classes
            return true; // Sucessfully changed all player data 
        }
        return false;
    }

    public override void _Ready()
	{
        player = (PlayerController)GetTree().Root.GetNode("PrototypeLevel").GetNode("PlayerController");
        pd = (playerData)player.GetChild(0); // playerData should be the controllers first child
    }

	public override void _Process(double delta)
    {
	}

    private void _on_pressed()
    {
        if(!applied)
        {
            if (ApplyUpgrade())
            {
                applied = true;
                GD.Print("Successful Upgrade");
            }
            Disabled = true;
            Panel upgradePanel = player.GetNode<CanvasLayer>("UI").GetNode<Panel>("UpgradePanel");
            upgradePanel.Hide();
        }
    }
}
