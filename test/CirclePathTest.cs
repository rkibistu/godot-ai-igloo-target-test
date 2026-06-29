using GdUnit4;
using Godot;
using IglooTestProj.Scripts;
using static GdUnit4.Assertions;

namespace IglooTestProj.Test;

[TestSuite]
public class CirclePathTest
{
    private static readonly Vector2 Center = new(300, 300);
    private const float Radius = 150f;
    private static readonly Vector2 Tolerance = new(0.01f, 0.01f);

    [TestCase]
    public void StartsAtAngleZero()
    {
        var path = new CirclePath(Center, Radius, 1f);

        AssertThat(path.GetPosition(0f)).IsEqualApprox(Center + new Vector2(Radius, 0), Tolerance);
    }

    [TestCase]
    public void ReachesQuarterTurn()
    {
        var path = new CirclePath(Center, Radius, 1f);

        AssertThat(path.GetPosition(Mathf.Pi / 2f)).IsEqualApprox(Center + new Vector2(0, Radius), Tolerance);
    }

    [TestCase]
    public void ReachesHalfTurn()
    {
        var path = new CirclePath(Center, Radius, 1f);

        AssertThat(path.GetPosition(Mathf.Pi)).IsEqualApprox(Center + new Vector2(-Radius, 0), Tolerance);
    }

    [TestCase]
    public void LoopsBackToStartAfterFullTurn()
    {
        var path = new CirclePath(Center, Radius, 1f);

        AssertThat(path.GetPosition(Mathf.Tau)).IsEqualApprox(Center + new Vector2(Radius, 0), Tolerance);
    }

    [TestCase]
    public void HigherAngularSpeedCoversMoreAngle()
    {
        var path = new CirclePath(Center, Radius, 2f);

        AssertThat(path.GetPosition(Mathf.Pi / 4f)).IsEqualApprox(Center + new Vector2(0, Radius), Tolerance);
    }

    [TestCase]
    public void AllPointsAreRadiusFromCenter()
    {
        var path = new CirclePath(Center, Radius, 1f);

        for (var t = 0f; t < 10f; t += 0.7f)
            AssertThat(path.GetPosition(t).DistanceTo(Center)).IsEqualApprox(Radius, 0.01f);
    }
}
