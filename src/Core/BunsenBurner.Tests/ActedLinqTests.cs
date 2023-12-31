﻿using System.Diagnostics.CodeAnalysis;

namespace BunsenBurner.Tests;

public static class ActedLinqTests
{
    [Fact(DisplayName = "Acted Aaa scenarios can be selected")]
    public static async Task Case1() =>
        await Aaa.Arrange(1)
            .Act(i => i)
            .Select(x => x.ToString(InvariantCulture))
            .Assert(r => r == "1");

    [Fact(DisplayName = "Acted Bdd scenarios can be selected")]
    [SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions")]
    public static async Task Case2() =>
        await Bdd.Given(1)
            .When(i => i)
            .Select(x => x.ToString(InvariantCulture))
            .Then(r => r == "1");

    [Fact(DisplayName = "Acted Aaa scenarios can be combined with bind")]
    public static async Task Case3() =>
        await Aaa.Arrange(1)
            .Act(i => i)
            .SelectMany(a => Aaa.Arrange(2).Act(_ => DateTime.Now).Select(b => (a, b)))
            .Assert(r => r.a == 1 && r.b.Ticks > r.a);

    [Fact(DisplayName = "Acted Aaa scenarios can be combined with bind")]
    public static async Task Case4() =>
        await (
            from a in Aaa.Arrange(1).Act(x => x.ToString(InvariantCulture))
            from b in Aaa.Arrange(2).Act(_ => DateTime.Now.ToString(InvariantCulture))
            from c in Aaa.Arrange(3).Act(_ => "some string")
            select a + b + c
        ).Assert(r => r.StartsWith('1') && r.EndsWith("some string"));

    [Fact(DisplayName = "Scenarios can be combined with Sequence")]
    public static async Task Case5() =>
        await Enumerable
            .Range(1, 10)
            .Select(x => x.ArrangeData().Act(i => i).Assert(i => i <= 10))
            .Sequence()
            .And(x => x.Take(11).Count() == 10);
}
