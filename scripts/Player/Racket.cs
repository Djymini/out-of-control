using Godot;
using System;

public partial class Racket : CharacterBody2D
{
    private const int MaxSuperCharge = 100;

    [Export] private int speed = 500;
    [Export] private Vector2 startPosition = new Vector2(576.0f, 576.0f);
    [Export] private int superCharge;
    [Export] public int chargeAmountBase { get; set; } = 5;
    [Export] private Node2D shootPoint;
    [Export] private PackedScene superShootPrefab;

    public override void _Ready()
    {
        Position = startPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        float originalY = Position.Y;
        ApplyVelocityFromInput();
        CheckInputSuperShoot();
        MoveAndSlide();
        Position = new Vector2(Position.X, originalY);
    }

    private void ApplyVelocityFromInput()
    {
        var newVelocity = Velocity;
        newVelocity.X = 0;
        newVelocity.X += ReadInputValueForMove() * speed;
        Velocity = newVelocity;
    }

    private float ReadInputValueForMove()
    {
        float inputValue = 0;

        bool right = Input.IsActionPressed("go_right");
        bool left = Input.IsActionPressed("go_left");

        if (right)
            inputValue += 1;

        if (left)
            inputValue -= 1;

        return inputValue;
    }

    private void CheckInputSuperShoot()
    {
        if (Input.IsActionPressed("shoot") && superCharge >= MaxSuperCharge)
        {
            PerformSuperShoot();
        }
    }

    private void PerformSuperShoot()
    {
        var superShoot = (SuperShoot)superShootPrefab.Instantiate();
        superShoot.Position = shootPoint.GlobalPosition;
        GetTree().Root.AddChild(superShoot);
        superCharge = 0;
    }

    public void IncreaseSuperCharge(int chargeAmount, int multiplier)
    {
        if (superCharge < MaxSuperCharge)
        {
            superCharge += chargeAmount * multiplier;
            GD.Print($"A implémenter plus tard : Super Charge : {superCharge}");
        }
        else
        {
            GD.Print("A implémenter plus tard : Tir chargée !!");
        }
    }
}
