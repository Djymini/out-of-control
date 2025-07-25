using Godot;
using System;

public abstract partial class SuperShoot : Area2D
{
    [Export] private SuperShootData data;
    private SuperShootEffect effect;
    private Vector2 direction = Vector2.Up;
    private double speed;

    public override void _Ready()
    {
        speed = data.speed;
    }


    public override void _Process(double delta)
    {
        Move(delta, speed, direction);
    }

    public abstract void Move(double delta, double speed, Vector2 direction);

    public abstract void InitializeEffect();

    public void HandleEnemyCollision(Enemy enemy)
    {
        if (data == null)
            return;

        enemy.Hit(data.damage);

        if (effect != null)
            effect.ApplyOnHitEnemy();

        QueueFree();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            HandleEnemyCollision(enemy);
        }
    }
}
