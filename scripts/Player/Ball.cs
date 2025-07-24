using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Ball : CharacterBody2D
{
    [Export] private float speed = 300;
    [Export] private int damage = 3;
    private bool isPitching;
    private int combo;
    private Racket racket;
    private List<BallEffect> ballEffects;

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
        ballEffects = new List<BallEffect>();

        //Test ne doit pa rester
        ballEffects.Add(new FireEffect());
        ballEffects.Add(new ElectricEffect());
        ballEffects.Add(new LightEffect());
        ballEffects.Add(new ExplodeEffect());
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

        ApplyBallEffectOnMove();

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
            ApplyBallEffectOnBounceAgainWall();
            Velocity = Velocity.Bounce(collision.GetNormal());
        }
    }

    private void HandleEnemyCollision(Enemy enemy, KinematicCollision2D collision)
    {
        enemy.Hit(damage);
        ApplyBallEffectOnHitEnemy();
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

    private void ApplyBallEffectOnHitEnemy()
    {
        if (ballEffects.Count < 1)
            return;

        foreach (var effect in ballEffects)
        {
            effect.ApplyOnHitEnemy();
        }
    }

    private void ApplyBallEffectOnBounceAgainWall()
    {
        if (ballEffects.Count < 1 && Velocity.X != 0 && Velocity.Y != 0)
            return;

        foreach (var effect in ballEffects)
        {
            effect.ApplyOnBounceAgainWall();
        }
    }

    private void ApplyBallEffectOnMove()
    {
        if (ballEffects.Count < 1)
            return;

        foreach (var effect in ballEffects)
        {
            effect.ApplyOnMove();
        }
    }
}
