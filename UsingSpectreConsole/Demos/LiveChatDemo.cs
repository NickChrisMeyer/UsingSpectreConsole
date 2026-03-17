using Spectre.Console;
using Spectre.Console.Rendering;

namespace UsingSpectreConsole.Demos;

/// <summary>
/// Demonstrates AnsiConsole.Live(), LiveDisplayContext, Rows, Panel, Markup,
/// FigletText, Rule, Align, and TextPrompt — assembled into a TUI-style chat layout.
///
/// ARCHITECTURE — Two-phase design (IMPORTANT):
///
///   Phase 1 — Static header (before Live block):
///     FigletText and Rule are rendered normally before Live() starts.
///
///   Phase 2 — Live scripted conversation:
///     The Live() block owns the terminal. No prompts can run inside it.
///
///   Phase 3 — Post-Live prompt:
///     After Live() ends, the terminal is free again for prompts.
///
/// WHY Live() + prompts cannot mix:
///   AnsiConsole.Live() takes exclusive control of the output area to enable
///   in-place updates. Rendering a prompt simultaneously would corrupt the display.
///   Always collect user input BEFORE or AFTER the Live block, never inside it.
/// </summary>
internal static class LiveChatDemo
{
    public static void Run()
    {
        // ── Phase 1: Static header (must be BEFORE Live block) ───────────────

        // FigletText renders a large ASCII-art title
        AnsiConsole.Write(
            new FigletText("CyberBot")
                .Color(Color.Cyan1)
                .Justify(Justify.Center));

        AnsiConsole.Write(new Rule("[dim cyan]v1.0 — Cybersecurity Awareness Assistant[/]")
        {
            Style = Style.Parse("grey")
        });

        AnsiConsole.WriteLine();

        // ── Phase 2: Live scripted conversation ──────────────────────────────

        var messages = new List<IRenderable>();

        // Script: 7 alternating bot/user messages
        var script = new[]
        {
            (IsBot: true,  Text: "Hello! I'm [bold]CyberBot[/], your cybersecurity awareness assistant. What topic can I help you with today?"),
            (IsBot: false, Text: "What is phishing?"),
            (IsBot: true,  Text: "Phishing is a social engineering attack where attackers impersonate trusted entities — like banks or employers — via email or messages. The goal is to steal credentials, install malware, or trick you into transferring funds."),
            (IsBot: false, Text: "How can I spot a phishing email?"),
            (IsBot: true,  Text: "Watch for [yellow]urgent language[/] (\"Act now!\"), [yellow]mismatched sender addresses[/], [yellow]suspicious links[/] (hover to preview), and [yellow]unexpected attachments[/]. Legitimate organisations rarely ask for passwords by email."),
            (IsBot: false, Text: "What should I do if I clicked a suspicious link?"),
            (IsBot: true,  Text: "[bold red]Act immediately:[/] disconnect from the network, run a full antivirus scan, change passwords for any accounts you were logged in to, and report the incident to your IT department. Speed matters."),
        };

        // .Overflow + .Cropping ensure the panel grows from the top as content is added,
        // creating a smooth "scroll" effect without reflowing the entire terminal.
        AnsiConsole.Live(BuildChatDisplay(messages))
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Bottom)
            .Start(ctx =>
            {
                foreach (var (isBot, text) in script)
                {
                    messages.Add(BuildMessage(isBot, text));

                    // UpdateTarget replaces the entire live-display renderable.
                    // This is different from ctx.Refresh(), which merely redraws the
                    // current renderable.  We must call UpdateTarget every time the
                    // messages list grows so the new panel is included in the render.
                    ctx.UpdateTarget(BuildChatDisplay(messages));

                    // Simulate typing delay
                    Thread.Sleep(isBot ? 1400 : 800);
                }
            });

        // ── Phase 3: Post-Live interactive prompt ─────────────────────────────
        // The Live block has ended; the terminal is free for normal I/O again.

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[dim]Your turn[/]") { Style = Style.Parse("grey") });

        var userMessage = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]You:[/] ")
                .AllowEmpty());

        AnsiConsole.MarkupLine("[dim italic]CyberBot is typing...[/]");
        Thread.Sleep(1200);

        AnsiConsole.Write(new Panel(
            new Markup("[deepskyblue1]Thanks for asking! In a real application your message would be processed here. This demo shows the UI layer only.[/]"))
            .RoundedBorder()
            .BorderColor(Color.DeepSkyBlue1)
            .Header("[bold deepskyblue1] CyberBot [/]")
            .Expand());
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Wraps all current messages in a heavy outer panel that acts as the chat window.
    /// Called after each message is appended so Live() can swap in the new layout.
    /// </summary>
    private static IRenderable BuildChatDisplay(List<IRenderable> messages)
    {
        // Rows stacks renderables vertically — the natural container for a message list
        var rows = new Rows(messages);

        return new Panel(rows)
            .HeavyBorder()
            .BorderColor(Color.Grey)
            .Header("[bold cyan] Conversation [/]")
            .Expand();
    }

    /// <summary>
    /// Builds a single chat bubble: bot messages align left with a blue border,
    /// user messages align right with a yellow border.
    /// </summary>
    private static Panel BuildMessage(bool isBot, string text)
    {
        if (isBot)
        {
            return new Panel(new Markup(text))
                .RoundedBorder()
                .BorderColor(Color.DeepSkyBlue1)
                .Header("[bold deepskyblue1] CyberBot [/]", Justify.Left);
        }
        else
        {
            return new Panel(new Markup($"[yellow]{text}[/]"))
                .RoundedBorder()
                .BorderColor(Color.Yellow)
                .Header("[bold yellow] You [/]", Justify.Right);
        }
    }
}
