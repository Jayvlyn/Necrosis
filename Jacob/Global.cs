using Godot;
using System;

public partial class Global : Node
{
    // Singleton instance
    private static Global _instance;


    // Variables to store information
    public enum Class
    {
        Tanker,
        Soldier,
        Sprinter
    }
    public Class selectedClass;

    // Called every time the node is added to the scene
    public override void _Ready()
    {
        // Ensure there is only one instance of this class
        if (_instance == null)
        {
            _instance = this;
            // Don't destroy on load
            SetProcess(false);
        }
        else
        {
            QueueFree();
        }
    }

    public static Global GetInstance()
    {
        return _instance;
    }
}