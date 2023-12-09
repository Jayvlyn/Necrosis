using Godot;
using System;

public partial class PauseMenu : Panel
{
    [Export] public Panel UpgradePanel;
    [Export] public Panel AllUpgradesPanel;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if(GetTree().Paused)
        {
            GetTree().Paused = false;
            UpgradePanel.Visible = false;
            AllUpgradesPanel.Visible = false;
            Visible = false;
        }
        else
        {
            GetTree().Paused = true;
            UpgradePanel.Visible = false;
            Visible = true;
        }
    }

    private void _on_resume_button_pressed()
    {
        TogglePause();
    }

    private void _on_quit_button_pressed()
    {
        TogglePause();
        GetTree().ChangeSceneToFile("res://Jacob/MainMenu.tscn");
    }
}
