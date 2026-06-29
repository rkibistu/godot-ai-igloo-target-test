using GdUnit4;
using Godot;
using IglooTestProj.Scripts;
using static GdUnit4.Assertions;

namespace IglooTestProj.Test;

[TestSuite]
public class HexagonPathTest
{
    private static readonly Vector2[] Vertices =
    {
        new(100, 0),
        new(200, 0),
        new(250, 100),
        new(200, 200),
        new(100, 200),
        new(50, 100),
    };

    private static readonly Vector2 Tolerance = new(0.01f, 0.01f);

    [TestCase]
    public void StartsAtFirstVertex()
    {
        var path = new HexagonPath(Vertices, 1f);

        AssertThat(path.GetPosition(0f)).IsEqualApprox(Vertices[0], Tolerance);
    }

    [TestCase]
    public void MovesAlongFirstEdge()
    {
        var path = new HexagonPath(Vertices, 1f);

        AssertThat(path.GetPosition(50f)).IsEqualApprox(new Vector2(150, 0), Tolerance);
    }

    [TestCase]
    public void ReachesSecondVertex()
    {
        var path = new HexagonPath(Vertices, 1f);

        AssertThat(path.GetPosition(100f)).IsEqualApprox(Vertices[1], Tolerance);
    }

    [TestCase]
    public void ReachesLastVertexBeforeLoop()
    {
        var path = new HexagonPath(Vertices, 1f);
        var perimeter = 0f;
        for (var i = 0; i < Vertices.Length; i++)
            perimeter += Vertices[i].DistanceTo(Vertices[(i + 1) % Vertices.Length]);
        var distanceToLastVertex = perimeter - Vertices[5].DistanceTo(Vertices[0]);

        AssertThat(path.GetPosition(distanceToLastVertex)).IsEqualApprox(Vertices[5], Tolerance);
    }

    [TestCase]
    public void LoopsBackToStartAfterFullPerimeter()
    {
        var path = new HexagonPath(Vertices, 1f);
        var perimeter = 0f;
        for (var i = 0; i < Vertices.Length; i++)
            perimeter += Vertices[i].DistanceTo(Vertices[(i + 1) % Vertices.Length]);

        AssertThat(path.GetPosition(perimeter)).IsEqualApprox(Vertices[0], Tolerance);
        AssertThat(path.GetPosition(perimeter + 50f)).IsEqualApprox(new Vector2(150, 0), Tolerance);
    }

    [TestCase]
    public void HigherSpeedCoversMoreDistance()
    {
        var path = new HexagonPath(Vertices, 2f);

        AssertThat(path.GetPosition(25f)).IsEqualApprox(new Vector2(150, 0), Tolerance);
    }

    [TestCase]
    public void BuildRegularHexagonProducesSixEquidistantVertices()
    {
        var center = new Vector2(300, 300);
        const float radius = 80f;

        var vertices = HexagonPath.BuildRegularHexagon(center, radius);

        AssertThat(vertices.Length).IsEqual(6);
        foreach (var vertex in vertices)
            AssertThat(vertex.DistanceTo(center)).IsEqualApprox(radius, 0.01f);
    }
}
