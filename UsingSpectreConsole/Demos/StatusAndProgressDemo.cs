using Spectre.Console;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates AnsiConsole.Status(), AnsiConsole.Progress(), StatusContext,
/// ProgressTask, Spinner.Known, and the various progress columns.
///
/// NOTE: Thread.Sleep is used here for demo simplicity.
/// In real async applications, use await Task.Delay(...) with the async overloads:
///   await AnsiConsole.Status().StartAsync("...", async ctx => { ... })
///   await AnsiConsole.Progress().StartAsync(async ctx => { ... })
/// </summary>
internal static class StatusAndProgressDemo
{
    public static void Run()
    {
        AnsiConsole.MarkupLine("[bold cyan]Status & Progress Demo[/]");

        // ── 1. Basic status spinner ───────────────────────────────────────────
        DemoHelpers.SectionRule("1 · Basic Status Spinner (Three-Phase)");

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("deepskyblue1"))
            .Start("[deepskyblue1]Thinking...[/]", ctx =>
            {
                Thread.Sleep(900);

                ctx.Status("[deepskyblue1]Analyzing threat database...[/]");
                Thread.Sleep(1000);

                ctx.Status("[deepskyblue1]Formulating response...[/]");
                Thread.Sleep(800);
            });

        AnsiConsole.Write(new Panel(new Markup(
            "[deepskyblue1]CyberBot:[/] Analysis complete. Phishing remains the #1 attack vector in 2024."))
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Header("[bold deepskyblue1] CyberBot [/]"));

        // ── 2. Spinner varieties ──────────────────────────────────────────────
        DemoHelpers.SectionRule("2 · Spinner Varieties");

        // Dots — classic animated dots, good for general loading
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("[dim]Spinner.Known.Dots — classic dots[/]", _ => Thread.Sleep(1500));

        // Star — a rotating star character, more playful
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Star)
            .Start("[dim]Spinner.Known.Star — rotating star[/]", _ => Thread.Sleep(1500));

        // Arc — sweeping arc, smooth and modern
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arc)
            .Start("[dim]Spinner.Known.Arc — sweeping arc[/]", _ => Thread.Sleep(1500));

        AnsiConsole.MarkupLine("[bold green]All spinner styles complete![/]");

        // ── 3. SpinnerStyle ───────────────────────────────────────────────────
        DemoHelpers.SectionRule("3 · SpinnerStyle (Bold DeepSkyBlue1)");

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("deepskyblue1 bold"))  // bold blue spinner
            .Start("[deepskyblue1 bold]Processing with styled spinner...[/]", _ => Thread.Sleep(1500));

        AnsiConsole.MarkupLine("[bold green]Done![/]");

        // ── 4. Progress with full columns ─────────────────────────────────────
        DemoHelpers.SectionRule("4 · Progress with Full Column Set");

        // NOTE: Status and Progress cannot be nested — show them sequentially.
        AnsiConsole.Progress()
            .Columns(
                new SpinnerColumn(),           // animated spinner per task
                new TaskDescriptionColumn(),   // task label
                new ProgressBarColumn(),       // visual progress bar
                new PercentageColumn(),        // "42%"
                new ElapsedTimeColumn())       // "0:03"
            .Start(ctx =>
            {
                // Three parallel tasks incrementing at different rates
                var scanTask   = ctx.AddTask("[deepskyblue1]Scanning files[/]");
                var updateTask = ctx.AddTask("[yellow]Updating definitions[/]");
                var reportTask = ctx.AddTask("[green]Generating report[/]");

                while (!ctx.IsFinished)
                {
                    Thread.Sleep(50);
                    scanTask.Increment(2.0);    // fastest
                    updateTask.Increment(1.3);  // medium
                    reportTask.Increment(0.8);  // slowest
                }
            });

        // ── 5. Sequential tasks (autoStart: false) ────────────────────────────
        DemoHelpers.SectionRule("5 · Sequential Tasks (Dependency Chain)");

        AnsiConsole.Progress()
            .Columns(new SpinnerColumn(), new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn())
            .Start(ctx =>
            {
                // autoStart: false means the task is queued but not yet running
                var step1 = ctx.AddTask("[deepskyblue1]Step 1: Authenticate[/]",    autoStart: false);
                var step2 = ctx.AddTask("[yellow]Step 2: Fetch threat data[/]", autoStart: false);
                var step3 = ctx.AddTask("[green]Step 3: Build report[/]",      autoStart: false);

                // Run each step only after the previous one finishes
                step1.StartTask();
                while (!step1.IsFinished) { Thread.Sleep(40); step1.Increment(4); }

                step2.StartTask();
                while (!step2.IsFinished) { Thread.Sleep(50); step2.Increment(3); }

                step3.StartTask();
                while (!step3.IsFinished) { Thread.Sleep(60); step3.Increment(2.5); }
            });

        AnsiConsole.MarkupLine("[bold green]All steps complete![/]");
    }
}
