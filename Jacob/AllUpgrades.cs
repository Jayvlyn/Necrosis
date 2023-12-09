using Godot;
using System;

public partial class AllUpgrades : Panel
{
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("upgrades"))
        {
            ToggleUpgrades();
        }
    }

    public void ToggleUpgrades()
    {
        if (GetTree().Paused)
        {
            GetTree().Paused = false;
            Visible = false;
        }
        else
        {
            GetTree().Paused = true;
            Visible = true;
        }
    }
}
