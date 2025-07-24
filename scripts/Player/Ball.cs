using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    [Export] private float speed = 300;
    [Export] private int damage = 3;
    private bool isPitching;
    private int combo;
    private Racket racket;

    public override void _Ready()
    {
        InitializeProperties();
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateMovement(delta);
    }

    private void InitializeProperties()
    {
        isPitching = false;
        combo = 0;
        racket = GetNode<Racket>("../Racket");
        Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        Rotation = racket.Rotation;
    }

    private void UpdateMovement(double delta)
    {
        if (!isPitching)
        {
            MakeTheFirstShoot();
            Position = new Vector2(racket.Position.X, racket.Position.Y - 24);
        }
        else
        {
            Move(delta);
        }
    }

    private void MakeTheFirstShoot()
    {
        var shoot = Input.IsActionJustPressed("shoot");

        if (shoot)
        {
            isPitching = true;
            Velocity = new Vector2(speed, speed).Rotated(Rotation);
        }
    }

    private void Move(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        if (collision != null)
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(KinematicCollision2D collision)
    {
        Node2D node = (Node2D)collision.GetCollider();
        if (node is Enemy enemy)
        {
            HandleEnemyCollision(enemy, collision);
        }
        else if (node is Racket racket)
        {
            HandleRacketCollision(racket, collision);
        }
        else
        {
            Velocity = Velocity.Bounce(collision.GetNormal());
        }
    }

    private void HandleEnemyCollision(Enemy enemy, KinematicCollision2D collision)
    {
        enemy.Hit(damage);
        combo++;
        GD.Print($"combo : {combo}");
        racket.IncreaseSuperCharge(1, combo);
        Velocity = Velocity.Bounce(collision.GetNormal());
    }

    private void HandleRacketCollision(Racket racket, KinematicCollision2D collision)
    {
        float leftEdge = racket.Position.X - 40;
        float rightEdge = racket.Position.X + 40;

        int chargeAmount = racket.chargeAmountBase;
        int edgeChargeAmount = racket.chargeAmountBase * 2 - racket.chargeAmountBase / 2;

        combo = 0;

        if (Position.X <= leftEdge)
        {
            racket.IncreaseSuperCharge(edgeChargeAmount, 1);
            Velocity = new Vector2(speed, speed).Rotated(Rotation);
        }
        else if (Position.X >= rightEdge)
        {
            racket.IncreaseSuperCharge(edgeChargeAmount, 1);
            Velocity = new Vector2(-speed, speed).Rotated(Rotation);
        }
        else
        {
            racket.IncreaseSuperCharge(chargeAmount, 1);
            Velocity = Velocity.Bounce(collision.GetNormal());
        }
    }
}
