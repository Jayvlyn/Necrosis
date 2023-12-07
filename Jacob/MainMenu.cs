using Godot;
using System;
using System.Diagnostics;

public partial class MainMenu : Node2D
{
	[Export] Resource cursor;

	public override void _Ready()
	{
		Input.SetCustomMouseCursor(cursor, Input.CursorShape.Arrow, new Vector2(32, 32));
		
	}

	public override void _Process(double delta)
	{
	}

	public void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Jacob/ClassSelect.tscn");
	}

	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
}
