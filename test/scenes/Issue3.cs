using Godot;
using IglooTestProj.Scripts;

namespace IglooTestProj.Test.Scenes;

// Manual verification scene for issue #3: a blue rectangle moving along a hexagonal path.
public partial class Issue3 : Node2D
{
    private static readonly Vector2 Center = new(300, 300);
    private const float HexagonRadius = 200f;
    private const float Speed = 150f;
    private static readonly Vector2 RectangleSize = new(28, 28);
    private const double QuitAfterSeconds = 5.0;

    private HexagonPath _path = null!;
    private double _elapsed;

    public override void _Ready()
    {
        var vertices = HexagonPath.BuildRegularHexagon(Center, HexagonRadius);
        _path = new HexagonPath(vertices, Speed);
        GetTree().CreateTimer(QuitAfterSeconds).Timeout += () => GetTree().Quit();
    }

    public override void _Process(double delta)
    {
        _elapsed += delta;
        QueueRedraw();
    }

    public override void _Draw()
    {
        var vertices = _path.Vertices;
        for (var i = 0; i < vertices.Length; i++)
            DrawLine(vertices[i], vertices[(i + 1) % vertices.Length], Colors.Gray, 2f);

        var position = _path.GetPosition((float)_elapsed);
        var rect = new Rect2(position - RectangleSize / 2f, RectangleSize);
        DrawRect(rect, Colors.Blue);
    }
}
