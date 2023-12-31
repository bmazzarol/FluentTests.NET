using System.Diagnostics.CodeAnalysis;

namespace BunsenBurner.Verify.NUnit.Tests;

using static BunsenBurner.Bdd;

[SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions")]
public static class BddTests
{
    [Test(Description = "Assertion with scrubbing and projection works")]
    public static async Task Case1() =>
        await Given(
                () =>
                    new
                    {
                        A = 1,
                        B = "2",
                        C = DateTimeOffset.Now
                    }
            )
            .When(i => i)
            .ThenResultIsUnchanged(x => new { x.A, x.C }, scrubResults: true);

    [Test(Description = "Named scenario assertion with scrubbing and projection works")]
    public static async Task Case2() =>
        await "Some description"
            .Given(
                () =>
                    new
                    {
                        A = 1,
                        B = "2",
                        C = DateTimeOffset.Now
                    }
            )
            .When(i => i)
            .ThenResultIsUnchanged(x => new { x.A, x.C }, scrubResults: true);

    [Test(Description = "Assertion without scrubbing works")]
    public static async Task Case3() =>
        await Given(() => new { A = 1, B = "2" }).When(i => i).ThenResultIsUnchanged();

    [Test(Description = "Named scenario assertion without scrubbing works")]
    public static async Task Case4() =>
        await "Some description"
            .Given(() => new { A = 1, B = "2" })
            .When(i => i)
            .ThenResultIsUnchanged();
}
