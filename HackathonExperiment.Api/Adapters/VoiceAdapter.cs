using Vonage.Voice.Nccos;

namespace HackathonExperiment.Api.Adapters;

public class VoiceAdapter
{
    public static TalkAction MakeAdamTalk(string text) =>
        new TalkAction
        {
            Text = text,
            Language = "en-GB",
            Style = 6,
            Premium = true,
        };
}