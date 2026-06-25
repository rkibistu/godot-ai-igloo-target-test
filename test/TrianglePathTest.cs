using GdUnit4;
using Godot;
using IglooTestProj.Scripts;
using static GdUnit4.Assertions;

namespace IglooTestProj.Test;

[TestSuite]
public class TrianglePathTest
{
    private static readonly Vector2[] Vertices =
    {
        new(0, 0),
        new(100, 0),
        new(0, 100),
    };

    private static readonly Vector2 Tolerance = new(0.01f, 0.01f);

    [TestCase]
    public void StartsAtFirstVertex()
    {
        var path = new TrianglePath(Vertices, 1f);

        AssertThat(path.GetPosition(0f)).IsEqualApprox(Vertices[0], Tolerance);
    }

    [TestCase]
    public void MovesAlongFirstEdge()
    {
        var path = new TrianglePath(Vertices, 1f);

        AssertThat(path.GetPosition(50f)).IsEqualApprox(new Vector2(50, 0), Tolerance);
    }

    [TestCase]
    public void ReachesSecondVertex()
    {
        var path = new TrianglePath(Vertices, 1f);

        AssertThat(path.GetPosition(100f)).IsEqualApprox(Vertices[1], Tolerance);
    }

    [TestCase]
    public void ReachesThirdVertex()
    {
        var path = new TrianglePath(Vertices, 1f);
        var distanceToThirdVertex = 100f + Vertices[1].DistanceTo(Vertices[2]);

        AssertThat(path.GetPosition(distanceToThirdVertex)).IsEqualApprox(Vertices[2], Tolerance);
    }

    [TestCase]
    public void LoopsBackToStartAfterFullPerimeter()
    {
        var path = new TrianglePath(Vertices, 1f);
        var perimeter = Vertices[0].DistanceTo(Vertices[1])
                         + Vertices[1].DistanceTo(Vertices[2])
                         + Vertices[2].DistanceTo(Vertices[0]);

        AssertThat(path.GetPosition(perimeter)).IsEqualApprox(Vertices[0], Tolerance);
        AssertThat(path.GetPosition(perimeter + 50f)).IsEqualApprox(new Vector2(50, 0), Tolerance);
    }

    [TestCase]
    public void HigherSpeedCoversMoreDistance()
    {
        var path = new TrianglePath(Vertices, 2f);

        AssertThat(path.GetPosition(25f)).IsEqualApprox(new Vector2(50, 0), Tolerance);
    }
}
