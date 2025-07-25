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
        speed += delta * 2;
        Position += (float)speed * (float)delta * direction;
    }


    public abstract void InitializeEffect();

    public void HandleEnemyCollision(Enemy enemy, KinematicCollision2D collision)
    {
        if (data == null)
            return;

        enemy.Hit(data.damage);

        if (effect != null)
            effect.ApplyOnHitEnemy();
    }
}
