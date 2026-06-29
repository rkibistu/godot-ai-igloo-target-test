using System;
using Godot;

namespace IglooTestProj.Scripts;

// Pure logic: maps elapsed time to a position along the perimeter of a hexagon,
// looping back to the start once the full perimeter has been traversed.
public class HexagonPath
{
    private const int SideCount = 6;

    private readonly Vector2[] _vertices;
    private readonly float[] _edgeLengths;
    private readonly float _perimeter;

    public float Speed { get; }

    public HexagonPath(Vector2[] vertices, float speed)
    {
        if (vertices == null || vertices.Length != SideCount)
            throw new ArgumentException("HexagonPath requires exactly 6 vertices.", nameof(vertices));

        _vertices = vertices;
        _edgeLengths = new float[SideCount];
        for (var i = 0; i < SideCount; i++)
            _edgeLengths[i] = _vertices[i].DistanceTo(_vertices[(i + 1) % SideCount]);

        _perimeter = 0f;
        foreach (var edgeLength in _edgeLengths)
            _perimeter += edgeLength;

        Speed = speed;
    }

    public Vector2[] Vertices => _vertices;

    public static Vector2[] BuildRegularHexagon(Vector2 center, float radius)
    {
        var vertices = new Vector2[SideCount];
        for (var i = 0; i < SideCount; i++)
        {
            var angle = Mathf.Tau * i / SideCount;
            vertices[i] = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        return vertices;
    }

    public Vector2 GetPosition(float elapsedTime)
    {
        var distance = (elapsedTime * Speed) % _perimeter;

        for (var i = 0; i < SideCount; i++)
        {
            var edgeLength = _edgeLengths[i];
            if (distance <= edgeLength)
            {
                var t = edgeLength == 0f ? 0f : distance / edgeLength;
                return _vertices[i].Lerp(_vertices[(i + 1) % SideCount], t);
            }

            distance -= edgeLength;
        }

        return _vertices[0];
    }
}
