using Godot;
using IglooTestProj.Scripts;

namespace IglooTestProj.Test.Scenes;

// Manual verification scene for issue #5: a rectangle moving along a circular path.
public partial class Issue5 : Node2D
{
    private static readonly Vector2 Center = new(300, 300);
    private const float CircleRadius = 150f;
    private const float AngularSpeed = 1.5f;
    private static readonly Vector2 RectangleSize = new(28, 28);
    private const double QuitAfterSeconds = 5.0;
    private const double HueCycleSeconds = 4.0;

    private CirclePath _path = null!;
    private double _elapsed;

    public override void _Ready()
    {
        _path = new CirclePath(Center, CircleRadius, AngularSpeed);
        GetTree().CreateTimer(QuitAfterSeconds).Timeout += () => GetTree().Quit();
    }

    public override void _Process(double delta)
    {
        _elapsed += delta;
        QueueRedraw();
    }

    public override void _Draw()
    {
        DrawArc(_path.Center, _path.Radius, 0f, Mathf.Tau, 64, Colors.Gray, 2f);

        var position = _path.GetPosition((float)_elapsed);
        var rect = new Rect2(position - RectangleSize / 2f, RectangleSize);
        var hue = (float)(_elapsed / HueCycleSeconds % 1.0);
        DrawRect(rect, Color.FromHsv(hue, 1f, 1f));
    }
}
