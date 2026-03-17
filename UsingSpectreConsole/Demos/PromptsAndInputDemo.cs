using Spectre.Console;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates TextPrompt&lt;T&gt;, SelectionPrompt&lt;T&gt;, MultiSelectionPrompt&lt;T&gt;,
/// AnsiConsole.Confirm, AnsiConsole.Ask&lt;T&gt;, and ValidationResult.
/// </summary>
internal static class PromptsAndInputDemo
{
    public static void Run()
    {
        AnsiConsole.MarkupLine("[bold cyan]Prompts & Input Demo[/]");

        // ── 1. Simple Ask ─────────────────────────────────────────────────────
        DemoHelpers.SectionRule("1 · Simple Ask");

        var name = AnsiConsole.Ask<string>("[deepskyblue1]What is your name?[/]");
        AnsiConsole.MarkupLineInterpolated($"[bold green]Nice to meet you, {name}![/]");

        // ── 2. TextPrompt with default ────────────────────────────────────────
        DemoHelpers.SectionRule("2 · TextPrompt with Default Value");

        var role = new TextPrompt<string>("[deepskyblue1]What is your role?[/]")
            .DefaultValue("student")
            .ShowDefaultValue();  // displays "[student]" hint next to prompt

        var chosenRole = AnsiConsole.Prompt(role);
        AnsiConsole.MarkupLineInterpolated($"[deepskyblue1]CyberBot:[/] Got it — I'll tailor my answers for a {chosenRole}.");

        // ── 3. TextPrompt with validation ─────────────────────────────────────
        DemoHelpers.SectionRule("3 · TextPrompt with Validation (Password)");

        var passwordPrompt = new TextPrompt<string>("[deepskyblue1]Create a demo password:[/]")
            .Secret()           // hides input with asterisks
            .PromptStyle("red") // style the asterisks
            .ValidationErrorMessage("[bold red]Password must be ≥8 characters and contain at least one digit.[/]")
            .Validate(pwd =>
            {
                if (pwd.Length < 8)
                    return ValidationResult.Error("Too short — must be at least 8 characters.");
                if (!pwd.Any(char.IsDigit))
                    return ValidationResult.Error("Must contain at least one digit.");
                return ValidationResult.Success();
            });

        var password = AnsiConsole.Prompt(passwordPrompt);
        AnsiConsole.MarkupLine("[bold green]Password accepted![/] [dim](not stored — this is a demo)[/]");

        // ── 4. SelectionPrompt ────────────────────────────────────────────────
        DemoHelpers.SectionRule("4 · SelectionPrompt");

        var topic = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a cybersecurity topic to explore:[/]")
                .PageSize(5)  // shows 5 items; scroll for more
                .HighlightStyle(Style.Parse("bold deepskyblue1"))
                .AddChoices(
                    "Phishing & Social Engineering",
                    "Password Security",
                    "Two-Factor Authentication",
                    "Malware & Ransomware",
                    "Network Security",
                    "Safe Browsing Habits"));

        AnsiConsole.Write(new Panel(new Markup($"[deepskyblue1]CyberBot:[/] Great choice! [bold]{topic}[/] is an important area of cybersecurity."))
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Header("[bold deepskyblue1] CyberBot [/]"));

        // ── 5. MultiSelectionPrompt ───────────────────────────────────────────
        DemoHelpers.SectionRule("5 · MultiSelectionPrompt");

        var threats = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("[deepskyblue1]Which threats have you encountered? (Space to select, Enter to confirm)[/]")
                .PageSize(6)
                .InstructionsText("[dim grey](Use [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]")
                .AddChoices(
                    "Phishing email",
                    "Suspicious link",
                    "Unknown USB device",
                    "Unsecured Wi-Fi",
                    "Weak password reuse",
                    "Software not updated"));

        AnsiConsole.MarkupLine($"[bold green]You selected {threats.Count} threat(s):[/]");
        foreach (var threat in threats)
        {
            AnsiConsole.MarkupLineInterpolated($"  [yellow]•[/] {threat}");
        }

        // ── 6. Confirmation ───────────────────────────────────────────────────
        DemoHelpers.SectionRule("6 · Confirmation Prompt");

        var wantsReport = AnsiConsole.Confirm("[deepskyblue1]Would you like a summary of cybersecurity tips?[/]");

        if (wantsReport)
        {
            AnsiConsole.Write(new Panel(new Markup(
                "[bold]Top 3 Cybersecurity Tips:[/]\n" +
                "[yellow]1.[/] Use a unique, strong password for every account.\n" +
                "[yellow]2.[/] Enable two-factor authentication wherever possible.\n" +
                "[yellow]3.[/] Think before you click — verify links and senders."))
                .RoundedBorder()
                .BorderColor(Color.DeepSkyBlue1)
                .Header("[bold deepskyblue1] Security Tips [/]")
                .Expand());
        }
        else
        {
            AnsiConsole.MarkupLine("[dim]No problem — stay safe out there![/]");
        }
    }
}
