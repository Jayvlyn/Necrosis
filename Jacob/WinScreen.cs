using Godot;
using System;

public partial class WinScreen : Node2D
{
	private void _on_menu_button_pressed()
	{
        GetTree().ChangeSceneToFile("res://Jacob/MainMenu.tscn");
    }
}
