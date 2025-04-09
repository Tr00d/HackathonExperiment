#region
using HackathonExperiment.Api.Adapters;
using Microsoft.AspNetCore.Mvc;
using Vonage.Common.Monads;
using Vonage.Voice.EventWebhooks;
using Vonage.Voice.Nccos;
#endregion

namespace HackathonExperiment.Api;

[ApiController]
[Route("[controller]")]
public class WebhooksController(IAiAdapter aiAdapter) : ControllerBase
{
    private const string GreetingsMessage = "Adam here. So, what's the question about?";
    private const string FailedToUnderstandQuestion = "I'm sorry, I didn't get that. Could you please repeat?";

    [HttpPost("asr")]
    public async Task<IActionResult> Speech(MultiInput speechResponse) =>
        await FetchQuestion(speechResponse.Speech.SpeechResults)
            .DoWhenSome(Console.WriteLine)
            .MapAsync(aiAdapter.AskAsync)
            .DoWhenSome(Console.WriteLine)
            .Map(VoiceAdapter.MakeAdamTalk)
            .Map(talk => this.Ok(new Ncco(talk)))
            .IfNone(this.Ok(new Ncco(VoiceAdapter.MakeAdamTalk(FailedToUnderstandQuestion))));

    private static Maybe<string> FetchQuestion(SpeechRecognitionResult[] results) =>
        results.Length != 0
            ? results.First().Text
            : Maybe<string>.None;

    [HttpPost("answer")]
    public IActionResult Answer()
    {
        var talkAction = VoiceAdapter.MakeAdamTalk(GreetingsMessage);
        var inputAction = new MultiInputAction
        {
            Type = [NccoInputType.Speech],
            EventUrl = [Environment.GetEnvironmentVariable("VCR_INSTANCE_PUBLIC_URL") + "/Webhooks/asr"],
            Dtmf = null,
            Speech = new SpeechSettings
            {
                Language = "en-GB",
                MaxDuration = 20,
                EndOnSilence = 2,
            },
        };
        return this.Ok(new Ncco(talkAction, inputAction));
    }
}