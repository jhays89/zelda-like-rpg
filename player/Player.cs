using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Adding [Export] attribute allows the variable to be edited in the editor
	[Export]
	public int _speed = 35;
	private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
	{
		HandleInput();
		MoveAndSlide();
		UpdateAnimation();
	}

	#region HELPERS
	private void HandleInput()
	{
        Velocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * _speed;
	}

	private void UpdateAnimation()
	{
		if (Velocity.Length() == 0 && _animationPlayer.IsPlaying())
			_animationPlayer.Stop();
		else
		{
			var direction = "Down";

			if (Velocity.X < 0)
				direction = "Left";

			if (Velocity.X > 0)
				direction = "Right";

			if (Velocity.Y < 0)
				direction = "Up";

			_animationPlayer.Play($"Walk{direction}");
		}
    }
	#endregion
}
