using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    private float speed = 500;
    private Boolean isPitching;
    private CharacterBody2D racket;

    public override void _Ready()
    {
        Initializing();
    }


    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        if (!isPitching)
        {
            Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        }
        else
        {
            var collision = MoveAndCollide(Velocity * (float)delta);
            if (collision != null)
            {
                Velocity = Velocity.Bounce(collision.GetNormal());
            }
        }
    }

    public void Initializing()
    {
        isPitching = false;
        racket = GetNode<CharacterBody2D>("../Racket");
        Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
    }

    private void GetInput()
    {
        var shoot = Input.IsActionPressed("shoot");

        if (shoot)
        {
            isPitching = true;
            Velocity = new Vector2(0, -speed).Rotated(Rotation);
        }
        
    }
}
