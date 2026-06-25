using Godot;
using IglooTestProj.Scripts;

namespace IglooTestProj.Test.Scenes;

// Manual verification scene for issue #1: a circle moving along a triangle path.
public partial class Issue1 : Node2D
{
    private static readonly Vector2[] TriangleVertices =
    {
        new(200, 100),
        new(350, 400),
        new(50, 400),
    };

    private const float Speed = 120f;
    private const float Radius = 14f;
    private const double QuitAfterSeconds = 5.0;

    private TrianglePath _path = null!;
    private double _elapsed;

    public override void _Ready()
    {
        _path = new TrianglePath(TriangleVertices, Speed);
        GetTree().CreateTimer(QuitAfterSeconds).Timeout += () => GetTree().Quit();
    }

    public override void _Process(double delta)
    {
        _elapsed += delta;
        QueueRedraw();
    }

    public override void _Draw()
    {
        for (var i = 0; i < TriangleVertices.Length; i++)
            DrawLine(TriangleVertices[i], TriangleVertices[(i + 1) % TriangleVertices.Length], Colors.Gray, 2f);

        var position = _path.GetPosition((float)_elapsed);
        DrawCircle(position, Radius, Colors.Red);
    }
}
