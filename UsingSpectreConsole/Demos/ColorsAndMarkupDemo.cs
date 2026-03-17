using Spectre.Console;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates AnsiConsole.MarkupLine, MarkupLineInterpolated, and Rule.
/// </summary>
internal static class ColorsAndMarkupDemo
{
    public static void Run()
    {
        AnsiConsole.MarkupLine("[bold cyan]Colors & Markup Demo[/]");

        // ── 1. Named colors ──────────────────────────────────────────────────
        DemoHelpers.SectionRule("1 · Named Colors");

        string[] namedColors = ["red", "green", "blue", "yellow", "magenta", "cyan", "white", "grey"];
        foreach (var color in namedColors)
        {
            AnsiConsole.MarkupLine($"[{color}]██ {color}[/]");
        }

        // ── 2. Hex & RGB ─────────────────────────────────────────────────────
        DemoHelpers.SectionRule("2 · Hex & RGB Colors");

        AnsiConsole.MarkupLine("[#FF6600]hex orange  (#FF6600)[/]");
        AnsiConsole.MarkupLine("[rgb(128,0,255)]rgb purple  (rgb(128,0,255))[/]");
        AnsiConsole.MarkupLine("[#00BFFF]hex deepskyblue  (#00BFFF)[/]");

        // ── 3. Text styles ───────────────────────────────────────────────────
        DemoHelpers.SectionRule("3 · Text Styles");

        AnsiConsole.MarkupLine("[bold]bold — use for emphasis[/]");
        AnsiConsole.MarkupLine("[italic]italic — use for quotes or hints[/]");
        AnsiConsole.MarkupLine("[underline]underline — use for links or labels[/]");
        AnsiConsole.MarkupLine("[dim]dim — use for secondary / metadata text[/]");
        AnsiConsole.MarkupLine("[invert]invert — swaps foreground and background[/]");
        AnsiConsole.MarkupLine("[strikethrough]strikethrough — use for removed content[/]");

        // ── 4. Combined styles ───────────────────────────────────────────────
        DemoHelpers.SectionRule("4 · Combined Styles");

        AnsiConsole.MarkupLine("[bold underline deepskyblue1]bold + underline + deepskyblue1[/]");
        AnsiConsole.MarkupLine("[italic yellow]italic + yellow[/]");
        AnsiConsole.MarkupLine("[bold red]bold + red — good for warnings[/]");
        AnsiConsole.MarkupLine("[dim italic grey]dim + italic + grey — good for timestamps[/]");

        // ── 5. Safe interpolation ─────────────────────────────────────────────
        DemoHelpers.SectionRule("5 · Safe Interpolation (MarkupLineInterpolated)");

        // GOTCHA: MarkupLine will throw if the string contains unescaped [ or ]
        // characters — e.g. user-supplied text.  Always use MarkupLineInterpolated
        // when embedding runtime values so Spectre escapes them automatically.
        string userInput = "Hello [world]";  // contains literal brackets

        AnsiConsole.MarkupLine("[bold red]WRONG:[/] MarkupLine with unescaped brackets throws:");
        AnsiConsole.MarkupLine("[dim](skipped to avoid crash — try it yourself!)[/]");
        // AnsiConsole.MarkupLine($"User said: {userInput}");  // ← would throw

        AnsiConsole.MarkupLine("[bold green]CORRECT:[/] MarkupLineInterpolated escapes automatically:");
        // The {userInput} interpolation hole is automatically escaped.
        AnsiConsole.MarkupLineInterpolated($"[deepskyblue1]User said:[/] {userInput}");

        // ── 6. Chat simulation ───────────────────────────────────────────────
        DemoHelpers.SectionRule("6 · Chat Color Simulation");

        AnsiConsole.MarkupLine("[deepskyblue1]CyberBot:[/] Hello! I'm your cybersecurity assistant. How can I help?");
        AnsiConsole.MarkupLine("[yellow]You:[/]      What is phishing?");
        AnsiConsole.MarkupLine("[dim grey]System:   Session started at 09:41 — topic: phishing[/]");
        AnsiConsole.MarkupLine("[deepskyblue1]CyberBot:[/] Phishing is a social engineering attack where attackers");
        AnsiConsole.MarkupLine("           impersonate trusted entities to steal credentials.");
    }
}
