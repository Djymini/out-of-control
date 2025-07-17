using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    [Export] private float speed;
    [Export] private int damage;
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
            Node2D node = (Node2D)collision.GetCollider();
            if (node.Name == "Racket")
            {
                CheckCollideWithRacket(node, collision);
            }
            else
            {
                Velocity = Velocity.Bounce(collision.GetNormal());
            }
            return node;
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

    private void CheckCollideWithRacket(Node2D node, KinematicCollision2D collision)
    {
        float leftPart = node.Position.X - 40;
        float rightPart = node.Position.X + 40;

        if (Position.X <= leftPart)
        {
            Velocity = new Vector2(speed, speed).Rotated(Rotation);
        }
        else if (Position.X >= rightPart)
        {
            Velocity = new Vector2(-speed, speed).Rotated(Rotation);
        }
        else
        {
            Velocity = Velocity.Bounce(collision.GetNormal());
        }
    }
}
