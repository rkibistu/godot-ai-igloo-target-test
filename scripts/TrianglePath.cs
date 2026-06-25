using System;
using Godot;

namespace IglooTestProj.Scripts;

// Pure logic: maps elapsed time to a position along the perimeter of a triangle,
// looping back to the start once the full perimeter has been traversed.
public class TrianglePath
{
    private readonly Vector2[] _vertices;
    private readonly float[] _edgeLengths;
    private readonly float _perimeter;

    public float Speed { get; }

    public TrianglePath(Vector2[] vertices, float speed)
    {
        if (vertices == null || vertices.Length != 3)
            throw new ArgumentException("TrianglePath requires exactly 3 vertices.", nameof(vertices));

        _vertices = vertices;
        _edgeLengths = new float[3];
        for (var i = 0; i < 3; i++)
            _edgeLengths[i] = _vertices[i].DistanceTo(_vertices[(i + 1) % 3]);

        _perimeter = _edgeLengths[0] + _edgeLengths[1] + _edgeLengths[2];
        Speed = speed;
    }

    public Vector2[] Vertices => _vertices;

    public Vector2 GetPosition(float elapsedTime)
    {
        var distance = (elapsedTime * Speed) % _perimeter;

        for (var i = 0; i < 3; i++)
        {
            var edgeLength = _edgeLengths[i];
            if (distance <= edgeLength)
            {
                var t = edgeLength == 0f ? 0f : distance / edgeLength;
                return _vertices[i].Lerp(_vertices[(i + 1) % 3], t);
            }

            distance -= edgeLength;
        }

        return _vertices[0];
    }
}
