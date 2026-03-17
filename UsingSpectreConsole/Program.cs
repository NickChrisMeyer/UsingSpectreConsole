using Spectre.Console;
using UsingSpectreConsole.Demos;

AnsiConsole.Clear();

// ── Header ────────────────────────────────────────────────────────────────────
AnsiConsole.Write(
    new FigletText("CyberBot TUI")
        .Color(Color.Cyan1)
        .Justify(Justify.Center));

AnsiConsole.Write(new Rule("[dim]Spectre.Console Feature Demonstrations[/]")
{
    Style = Style.Parse("grey")
});

AnsiConsole.MarkupLine("\n[dim]Each demo highlights a different Spectre.Console capability — explore them in any order.[/]\n");

// ── Demo menu loop ────────────────────────────────────────────────────────────
const string Opt1    = "1. Colors & Markup";
const string Opt2    = "2. Panels & Borders";
const string Opt3    = "3. Tables & Columns";
const string Opt4    = "4. Rules & Dividers";
const string Opt5    = "5. Prompts & Input";
const string Opt6    = "6. Status & Progress";
const string Opt7    = "7. Live Chat Layout";
const string OptAll  = "► Run All Demos";
const string OptExit = "✗ Exit";

while (true)
{
    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[bold cyan]Select a demo:[/]")
            .HighlightStyle(Style.Parse("bold deepskyblue1"))
            .AddChoices(Opt1, Opt2, Opt3, Opt4, Opt5, Opt6, Opt7, OptAll, OptExit));

    switch (choice)
    {
        case Opt1:
            ColorsAndMarkupDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case Opt2:
            PanelsAndBordersDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case Opt3:
            TablesAndColumnsDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case Opt4:
            RulesAndDividersDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case Opt5:
            PromptsAndInputDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case Opt6:
            StatusAndProgressDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case Opt7:
            LiveChatDemo.Run();
            DemoHelpers.WaitForUser();
            break;

        case OptAll:
            ColorsAndMarkupDemo.Run();   DemoHelpers.WaitForUser();
            PanelsAndBordersDemo.Run();  DemoHelpers.WaitForUser();
            TablesAndColumnsDemo.Run();  DemoHelpers.WaitForUser();
            RulesAndDividersDemo.Run();  DemoHelpers.WaitForUser();
            PromptsAndInputDemo.Run();   DemoHelpers.WaitForUser();
            StatusAndProgressDemo.Run(); DemoHelpers.WaitForUser();
            LiveChatDemo.Run();          DemoHelpers.WaitForUser();
            break;

        case OptExit:
            AnsiConsole.MarkupLine("\n[deepskyblue1]Stay secure, stay informed. Goodbye![/]\n");
            return;
    }
}
