using Spectre.Console;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates Panel, Markup, BoxBorder, Padding, and Align.
/// </summary>
internal static class PanelsAndBordersDemo
{
    public static void Run()
    {
        AnsiConsole.MarkupLine("[bold cyan]Panels & Borders Demo[/]");

        // ── 1. Five border styles ─────────────────────────────────────────────
        DemoHelpers.SectionRule("1 · Five Border Styles");

        // IMPORTANT: Panel content must be IRenderable — use new Markup(...)
        // NOT bare strings.  Panel(string) is deprecated and skips markup processing.
        var content = new Markup("[deepskyblue1]Panel content goes here.[/]");

        AnsiConsole.Write(new Panel(new Markup("[deepskyblue1]RoundedBorder[/]"))
            .RoundedBorder()
            .Header("[dim]rounded[/]"));

        AnsiConsole.Write(new Panel(new Markup("[deepskyblue1]HeavyBorder[/]"))
            .HeavyBorder()
            .Header("[dim]heavy[/]"));

        AnsiConsole.Write(new Panel(new Markup("[deepskyblue1]DoubleBorder[/]"))
            .DoubleBorder()
            .Header("[dim]double[/]"));

        AnsiConsole.Write(new Panel(new Markup("[deepskyblue1]AsciiBorder[/]"))
            .AsciiBorder()
            .Header("[dim]ascii[/]"));

        AnsiConsole.Write(new Panel(new Markup("[deepskyblue1]SquareBorder[/]"))
            .SquareBorder()
            .Header("[dim]square[/]"));

        // ── 2. Border colors ──────────────────────────────────────────────────
        DemoHelpers.SectionRule("2 · Border Colors");

        // Bot messages use DeepSkyBlue1; user messages use Yellow — consistent theme
        AnsiConsole.Write(new Panel(new Markup("[deepskyblue1]CyberBot:[/] Hello! I'm your cybersecurity assistant."))
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Header("[bold deepskyblue1] CyberBot [/]"));

        AnsiConsole.Write(new Panel(new Markup("[yellow]You:[/] What is a firewall?"))
            .RoundedBorder()
            .BorderColor(Color.Yellow)
            .Header("[bold yellow] You [/]", Justify.Right));

        // ── 3. Header justification ───────────────────────────────────────────
        DemoHelpers.SectionRule("3 · Header Justification");

        AnsiConsole.Write(new Panel(new Markup("Left-justified header"))
            .RoundedBorder()
            .Header("[bold cyan] Left [/]", Justify.Left));

        AnsiConsole.Write(new Panel(new Markup("Center-justified header"))
            .RoundedBorder()
            .Header("[bold cyan] Center [/]", Justify.Center));

        AnsiConsole.Write(new Panel(new Markup("Right-justified header"))
            .RoundedBorder()
            .Header("[bold cyan] Right [/]", Justify.Right));

        // ── 4. Padding ────────────────────────────────────────────────────────
        DemoHelpers.SectionRule("4 · Padding");

        AnsiConsole.Write(new Panel(new Markup("No padding — text hugs the border."))
            .RoundedBorder()
            .Header("[dim]No Padding[/]"));

        // Padding(horizontal, vertical) — adds space inside the panel border
        AnsiConsole.Write(new Panel(new Markup("Padding(4, 1) — four spaces left/right, one line top/bottom."))
            .RoundedBorder()
            .Padding(4, 1)
            .Header("[dim]With Padding[/]"));

        // ── 5. Expand ─────────────────────────────────────────────────────────
        DemoHelpers.SectionRule("5 · Expand (Fill Terminal Width)");

        AnsiConsole.Write(new Panel(new Markup("Default — panel is only as wide as its content."))
            .RoundedBorder()
            .Header("[dim]Default Width[/]"));

        // .Expand() stretches the panel to fill the full terminal width
        AnsiConsole.Write(new Panel(new Markup("Expanded — panel fills the full terminal width."))
            .RoundedBorder()
            .Expand()
            .Header("[dim]Expanded[/]"));

        // ── 6. Nested panels ──────────────────────────────────────────────────
        DemoHelpers.SectionRule("6 · Nested Panels");

        // An inner Panel is itself IRenderable, so it can be the content of an outer Panel.
        // Useful for card-in-container layouts like a chat bubble inside a chat window.
        var infoCard = new Panel(new Markup(
            "[bold]Tip:[/] Always verify sender addresses before clicking links.\n" +
            "[dim]Source: SANS Internet Storm Center[/]"))
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Header("[bold deepskyblue1] Security Tip [/]")
            .Padding(1, 0);

        var chatContainer = new Panel(infoCard)
            .HeavyBorder()
            .BorderColor(Color.Grey)
            .Header("[bold cyan] Chat Window [/]")
            .Expand();

        AnsiConsole.Write(chatContainer);
    }
}
