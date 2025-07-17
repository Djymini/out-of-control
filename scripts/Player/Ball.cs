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
        InitializeTheBall();
    }

    public override void _PhysicsProcess(double delta)
    {
        BallMovement(delta);
    }

    private void InitializeTheBall()
    {
        isPitching = false;
        racket = GetNode<CharacterBody2D>("../Racket");
        Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        Rotation = racket.Rotation;
    }

    private void BallMovement(double delta)
    {
        if (!isPitching)
        {
            ShootTheBall();
            Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        }
        else
        {
            BallMove(delta);
        }
    }

    private void ShootTheBall()
    {
        var shoot = Input.IsActionPressed("shoot");

        if (shoot)
        {
            isPitching = true;
            Velocity = new Vector2(speed, speed).Rotated(Rotation);
        }
    }

    private void BallMove(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        if (collision != null)
        {
            CollisionManager(collision);
        }
    }

    private void CollisionManager(KinematicCollision2D collision)
    {
        Node2D node = (Node2D)collision.GetCollider();
        if (node is Enemy enemy)
        {
            CollisionWithEnemy(enemy, collision);
        }
        else if (node is Racket racket)
        {
            CollisionWithRacket(racket, collision);
        }
        else
        {
            Velocity = Velocity.Bounce(collision.GetNormal());
        }
    }

    private void CollisionWithEnemy(Enemy enemy, KinematicCollision2D collision)
    {
        enemy.Hit(damage);
        Velocity = Velocity.Bounce(collision.GetNormal());
    }

    private void CollisionWithRacket(Racket racket, KinematicCollision2D collision)
    {
        float leftPart = racket.Position.X - 40;
        float rightPart = racket.Position.X + 40;

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
