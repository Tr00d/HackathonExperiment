#region
using Microsoft.AspNetCore.Mvc;
using Vonage.Voice.EventWebhooks;
using Vonage.Voice.Nccos;
#endregion

namespace HackathonExperiment.Api;

[ApiController]
[Route("[controller]")]
public class WebhooksController : ControllerBase
{
    [HttpPost("asr")]
    public async Task<IActionResult> Speech(MultiInput response) =>
        this.Ok(new Ncco(
            new TalkAction
            {
                Text =
                    "So, the T28 measures 97mm long. As far as I know, Ericsson sold approximately 10 millions units. Stacking them would be... Please give me a second... Around 970 kilometers. It has to be Munich to Paris then!",
                Language = "en-GB",
                Style = 6, Premium = true,
            }));
}