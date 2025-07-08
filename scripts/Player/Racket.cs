using Godot;
using System;

public partial class Racket : CharacterBody2D
{
    [Export] private int speed;
    [Export] private int strength;
    private Vector2 startPosition = new Vector2(1152 / 2, 576);

    public override void _Ready()
    {
        Position = startPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        float originalY = Position.Y;
        var velocity = Velocity;
        Velocity = velocity;
        GetInput();
        MoveAndSlide();
        Position = new Vector2(Position.X, originalY);
    }

    private void GetInput()
    {
        var velocity = Velocity;
        velocity.X = 0;

        var right = Input.IsActionPressed("go_right");
        var left = Input.IsActionPressed("go_left");

        if (right)
            velocity.X += speed;

        if (left)
            velocity.X -= speed;

        Velocity = velocity;
    }
}
