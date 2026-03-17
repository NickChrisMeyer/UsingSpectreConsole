using Spectre.Console;

namespace UsingSpectreConsole.Demos;

internal static class DemoHelpers
{
    /// <summary>
    /// Pauses execution until the user presses a key, then clears the console.
    /// </summary>
    public static void WaitForUser()
    {
        AnsiConsole.MarkupLine("\n[dim]Press any key to return to the menu...[/]");
        Console.ReadKey(intercept: true);
        AnsiConsole.Clear();
    }

    /// <summary>
    /// Writes a visible section divider with a bold cyan title.
    /// </summary>
    public static void SectionRule(string title)
    {
        AnsiConsole.WriteLine();
        var rule = new Rule($"[bold cyan]{title}[/]")
        {
            Justification = Justify.Left,
            Style = Style.Parse("cyan")
        };
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }
}
