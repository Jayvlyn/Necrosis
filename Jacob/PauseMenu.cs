using Godot;
using System;

public partial class PauseMenu : Panel
{
    [Export] public Panel UpgradePanel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }

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
            Visible = false;
        }
        else
        {
            GetTree().Paused = true;
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
