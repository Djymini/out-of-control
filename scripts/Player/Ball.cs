using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    private float speed = 350;
    private int damage = 3;
    private Boolean isPitching;
    private CharacterBody2D racket;

    public override void _Ready()
    {
        Initializing();
    }

    public override void _PhysicsProcess(double delta)
    {
        checkPitching(delta);
    }

    public void Initializing()
    {
        isPitching = false;
        racket = GetNode<CharacterBody2D>("../Racket");
        Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        Rotation = racket.Rotation;
    }

    private void GetInput()
    {
        var shoot = Input.IsActionPressed("shoot");

        if (shoot)
        {
            isPitching = true;
            Velocity = new Vector2(speed, speed).Rotated(Rotation);
        }
    }

    private void checkPitching(double delta)
    {
        if (!isPitching)
        {
            GetInput();
            Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        }
        else
        {
            var collision = BallMoved(delta);
            CollideEnemy(collision);
        }
    }

    private Node2D BallMoved(double delta)
{
    var collision = MoveAndCollide(Velocity * (float)delta);
    if (collision != null)
    {
        Velocity = Velocity.Bounce(collision.GetNormal());
        return (Node2D)collision.GetCollider();
    }

    return null;
}


    private void CollideEnemy(Node2D collision)
    {
        if (collision is Enemy enemy)
        {
            GD.Print(collision);
            enemy.Hit(damage);
        }
    }
}
