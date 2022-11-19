using System.Diagnostics.CodeAnalysis;
using static BunsenBurner.Xunit.TheoryExtensions;

namespace BunsenBurner.Xunit.Tests;

using TestScenario = Scenario<Syntax.Aaa>.Asserted<int, string>;

[ExcludeFromCodeCoverage]
public static class TheoryTests
{
    [Fact(DisplayName = "Arranged Scenarios can be collected into theory data")]
    public static async Task Case1() =>
        await TheoryData(1.ArrangeData(), 2.ArrangeData())
            .ArrangeData()
            .Act(_ => _)
            .Assert(r => r.Count() == 2);

    [Fact(DisplayName = "Acted Scenarios can be collected into theory data")]
    public static async Task Case2() =>
        await TheoryData(
                1.ArrangeData().Act(x => x.ToString()),
                2.ArrangeData().Act(x => x.ToString())
            )
            .ArrangeData()
            .Act(_ => _)
            .Assert(r => r.Count() == 2);

    [Fact(DisplayName = "Asserted Scenarios can be collected into theory data")]
    public static async Task Case3() =>
        await TheoryData(
                1.ArrangeData().Act(x => x.ToString()).Assert(r => r == "1"),
                2.ArrangeData().Act(x => x.ToString()).Assert(r => r == "2")
            )
            .ArrangeData()
            .Act(_ => _)
            .Assert(r => r.Count() == 2);

    public static TheoryData<TestScenario> TestCases = TheoryData(
        "Some test2".Arrange(1).Act(x => x.ToString()).Assert(r => r == "1"),
        "Some other test".Arrange(2).Act(x => x.ToString()).Assert(r => r == "2")
    );

    [Theory(DisplayName = "Example running scenarios from theory data")]
    [MemberData(nameof(TestCases))]
    public static async Task Case4(TestScenario scenario) => await scenario;
}
