using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    private float speed;
    private Boolean isPitching;
    private  CharacterBody2D racket;

    public override void _Ready()
    {
        Initializing();
    }


    public override void _PhysicsProcess(double delta)
    {
        if (!isPitching)
        {
            Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        }
    }

    public void Initializing()
    {
        isPitching = false;
        racket = GetNode<CharacterBody2D>("../Racket");
        Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
    }
}
