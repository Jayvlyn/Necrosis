using Godot;
using System;
using System.Diagnostics;

public partial class BloodBar : ProgressBar
{
	private playerData pd = new playerData();

	[Export] private float gainTime = 0.01f; 
	private float gainTimer = 0;

	private bool increaseExp = false;
	private int expGained = 0;

	public override void _Ready()
	{
		pd = (playerData)GetParent().GetParent().GetChild(0);
        Value = 0;
    }

	public override void _Process(double delta)
	{
        if (increaseExp)
		{
			Value = Mathf.Lerp(pd.experience - expGained, pd.experience, gainTimer);

			gainTimer += gainTime;
			if(gainTimer > 1)
			{
				increaseExp = false;
				gainTimer = 0;
			}
		}
	}

	public void ExpGained(int amount)
	{
		expGained = amount;
		increaseExp = true;
		Debug.WriteLine("Gained exp!: " +  amount);
	}

	public void _on_player_data_test(int amount)
	{
		Debug.WriteLine("TEst 1" + amount);
	}

	public void _on_player_data_test_2()
	{
		Debug.WriteLine("test 2");
	}
}
