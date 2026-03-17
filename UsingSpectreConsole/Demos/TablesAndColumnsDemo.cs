using Spectre.Console;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates Table, TableColumn, Markup, and the Columns layout widget.
/// </summary>
internal static class TablesAndColumnsDemo
{
    public static void Run()
    {
        AnsiConsole.MarkupLine("[bold cyan]Tables & Columns Demo[/]");

        // ── 1. Chat history table ─────────────────────────────────────────────
        DemoHelpers.SectionRule("1 · Chat History Table");

        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Expand();

        // Column configuration: alignment, width, and style are set on TableColumn
        table.AddColumn(new TableColumn("[bold]Sender[/]").RightAligned().Width(12));
        table.AddColumn(new TableColumn("[bold]Message[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold]Timestamp[/]").RightAligned().Width(19)
            .Footer("[dim grey]end of session[/]"));

        // IMPORTANT: Use AddRow(IRenderable[]) — NOT AddRow(string[]).
        // The string overload bypasses markup processing, so color/style tags are ignored.
        table.AddRow(
            new Markup("[deepskyblue1]CyberBot[/]"),
            new Markup("Hello! What cybersecurity topic can I help you with today?"),
            new Markup("[dim grey]09:41:02[/]"));

        table.AddRow(
            new Markup("[yellow]You[/]"),
            new Markup("What is two-factor authentication?"),
            new Markup("[dim grey]09:41:15[/]"));

        table.AddRow(
            new Markup("[deepskyblue1]CyberBot[/]"),
            new Markup("2FA adds a second verification step — something you [bold]know[/] plus something you [bold]have[/]."),
            new Markup("[dim grey]09:41:18[/]"));

        table.AddRow(
            new Markup("[yellow]You[/]"),
            new Markup("Which 2FA method is most secure?"),
            new Markup("[dim grey]09:41:30[/]"));

        table.AddRow(
            new Markup("[deepskyblue1]CyberBot[/]"),
            new Markup("Hardware keys (e.g. [bold]YubiKey[/]) are strongest. Authenticator apps beat SMS."),
            new Markup("[dim grey]09:41:34[/]"));

        AnsiConsole.Write(table);

        // ── 2. Column options ─────────────────────────────────────────────────
        DemoHelpers.SectionRule("2 · Column Options");

        var optTable = new Table().RoundedBorder().BorderColor(Color.Grey);

        optTable.AddColumn(new TableColumn("[bold]Default[/]"));
        optTable.AddColumn(new TableColumn("[bold]Centered[/]").Centered());
        optTable.AddColumn(new TableColumn("[bold]NoWrap[/]").NoWrap());
        optTable.AddColumn(new TableColumn("[bold]Width(20)[/]").Width(20));

        optTable.AddRow(
            new Markup("normal"),
            new Markup("[cyan]centered text[/]"),
            new Markup("[yellow]no wrapping here[/]"),
            new Markup("[deepskyblue1]fixed 20 cols[/]"));

        AnsiConsole.Write(optTable);

        // ── 3. Border styles ──────────────────────────────────────────────────
        DemoHelpers.SectionRule("3 · Table Border Styles");

        void ShowBorderTable(string label, Action<Table> applyBorder)
        {
            var t = new Table();
            applyBorder(t);
            t.AddColumn(new TableColumn(label));
            t.AddRow(new Markup("[dim]sample row[/]"));
            AnsiConsole.Write(t);
        }

        ShowBorderTable("[dim]HeavyBorder[/]",    t => t.HeavyBorder());
        ShowBorderTable("[dim]DoubleBorder[/]",   t => t.DoubleBorder());
        ShowBorderTable("[dim]AsciiBorder[/]",    t => t.AsciiBorder());
        ShowBorderTable("[dim]MarkdownBorder[/]", t => t.MarkdownBorder());

        // ── 4. Columns widget ─────────────────────────────────────────────────
        DemoHelpers.SectionRule("4 · Columns Widget (Side-by-Side Panels)");

        // Columns distributes its children into equal-width columns across the terminal.
        // Useful for tip cards, status summaries, or feature lists.
        var tip1 = new Panel(new Markup("[bold]Tip 1:[/]\nUse unique passwords\nfor every account."))
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Header("[bold deepskyblue1] Passwords [/]");

        var tip2 = new Panel(new Markup("[bold]Tip 2:[/]\nEnable two-factor\nauthentication (2FA)."))
            .RoundedBorder()
            .BorderColor(Color.Yellow)
            .Header("[bold yellow] 2FA [/]");

        var tip3 = new Panel(new Markup("[bold]Tip 3:[/]\nKeep software updated\nto patch vulnerabilities."))
            .RoundedBorder()
            .BorderColor(Color.Green)
            .Header("[bold green] Updates [/]");

        // Columns automatically divides the terminal width equally between its children
        AnsiConsole.Write(new Columns(tip1, tip2, tip3));
    }
}
