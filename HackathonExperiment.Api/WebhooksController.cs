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
    [HttpPost("asr")]
    public async Task<IActionResult> Speech(MultiInput speechResponse) =>
        await FetchQuestion(speechResponse.Speech.SpeechResults)
            .MapAsync(aiAdapter.AskAsync)
            .Map(BuildNccoWithAnswer)
            .Map(this.Ok)
            .IfNone(this.Ok(BuildNccoWithoutAnswer()));

    private static Ncco BuildNccoWithAnswer(string answer) =>
        new Ncco(
            new TalkAction
            {
                Text = answer,
                Language = "en-GB",
                Style = 6,
                Premium = true,
            });

    private static Ncco BuildNccoWithoutAnswer() =>
        BuildNccoWithAnswer("I'm sorry, I didn't get that. Could you please repeat?");

    private static Maybe<string> FetchQuestion(SpeechRecognitionResult[] results) =>
        results.Length != 0
            ? results.OrderByDescending(result => decimal.Parse(result.Confidence)).First().Text
            : Maybe<string>.None;
}