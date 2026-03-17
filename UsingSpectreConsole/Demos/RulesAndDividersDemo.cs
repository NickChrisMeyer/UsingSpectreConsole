using Spectre.Console;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates Rule, Style, and Justify.
/// </summary>
internal static class RulesAndDividersDemo
{
    public static void Run()
    {
        AnsiConsole.MarkupLine("[bold cyan]Rules & Dividers Demo[/]");

        // ── 1. Plain rule ─────────────────────────────────────────────────────
        DemoHelpers.SectionRule("1 · Plain Rule");

        AnsiConsole.MarkupLine("[dim]A plain Rule() with no arguments:[/]");
        AnsiConsole.Write(new Rule());

        // ── 2. Titled rules ───────────────────────────────────────────────────
        DemoHelpers.SectionRule("2 · Titled Rules");

        AnsiConsole.Write(new Rule("[bold cyan]New Conversation[/]"));

        AnsiConsole.Write(new Rule("[dim grey]09:41 — Topic: Phishing Awareness[/]"));

        AnsiConsole.Write(new Rule("[bold red]⚠ ALERT — Suspicious Activity Detected[/]"));

        // ── 3. Justification ──────────────────────────────────────────────────
        DemoHelpers.SectionRule("3 · Rule Justification");

        AnsiConsole.Write(new Rule("[cyan]Left-aligned title[/]")
        {
            Justification = Justify.Left
        });

        AnsiConsole.Write(new Rule("[cyan]Center-aligned title[/]")
        {
            Justification = Justify.Center  // this is the default
        });

        AnsiConsole.Write(new Rule("[cyan]Right-aligned title[/]")
        {
            Justification = Justify.Right
        });

        // ── 4. RuleStyle colors ────────────────────────────────────────────────
        DemoHelpers.SectionRule("4 · Rule Line Colors (RuleStyle)");

        // The Style on the Rule controls the line color independently of title markup
        AnsiConsole.Write(new Rule("[bold]deepskyblue1 line[/]")
        {
            Style = Style.Parse("deepskyblue1")
        });

        AnsiConsole.Write(new Rule("[bold]bold yellow line[/]")
        {
            Style = Style.Parse("bold yellow")
        });

        AnsiConsole.Write(new Rule("[bold]dim grey line[/]")
        {
            Style = Style.Parse("dim grey")
        });

        // ── 5. Chatbot pattern ────────────────────────────────────────────────
        DemoHelpers.SectionRule("5 · Chatbot Session Separator Pattern");

        AnsiConsole.MarkupLine("[dim]Rules visually separate conversation sessions:[/]");
        AnsiConsole.WriteLine();

        // Session 1
        AnsiConsole.Write(new Rule("[dim grey]Session 1 — 09:30[/]") { Style = Style.Parse("dim grey") });
        AnsiConsole.MarkupLine("[deepskyblue1]CyberBot:[/] Hello! What topic can I help with?");
        AnsiConsole.MarkupLine("[yellow]You:[/]      Tell me about phishing.");
        AnsiConsole.MarkupLine("[deepskyblue1]CyberBot:[/] Phishing is a social engineering attack...");

        // Session 2
        AnsiConsole.Write(new Rule("[dim grey]Session 2 — 10:15[/]") { Style = Style.Parse("dim grey") });
        AnsiConsole.MarkupLine("[deepskyblue1]CyberBot:[/] Welcome back! How can I help?");
        AnsiConsole.MarkupLine("[yellow]You:[/]      What is ransomware?");
        AnsiConsole.MarkupLine("[deepskyblue1]CyberBot:[/] Ransomware encrypts your files and demands payment...");

        // End of sessions
        AnsiConsole.Write(new Rule("[dim]end of history[/]") { Style = Style.Parse("dim grey") });
    }
}
