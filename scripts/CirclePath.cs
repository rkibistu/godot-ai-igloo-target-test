using Godot;

namespace IglooTestProj.Scripts;

// Pure logic: maps elapsed time to a position along the circumference of a circle.
public class CirclePath
{
    private readonly Vector2 _center;
    private readonly float _radius;

    public float AngularSpeed { get; }

    public CirclePath(Vector2 center, float radius, float angularSpeed)
    {
        _center = center;
        _radius = radius;
        AngularSpeed = angularSpeed;
    }

    public Vector2 Center => _center;

    public float Radius => _radius;

    public Vector2 GetPosition(float elapsedTime)
    {
        var angle = elapsedTime * AngularSpeed;
        return _center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;
    }
}
