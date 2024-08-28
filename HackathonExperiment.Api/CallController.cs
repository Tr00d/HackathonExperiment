#region
using Microsoft.AspNetCore.Mvc;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
#endregion

namespace HackathonExperiment.Api;

[ApiController]
[Route("[controller]")]
public class CallController(IVoiceClient client) : ControllerBase
{
    [HttpPost("CallMe")]
    public async Task<IActionResult> CallMe()
    {
        var toEndpoint = new PhoneEndpoint {Number = "+33607118924"};
        var fromEndpoint = new PhoneEndpoint {Number = "+447451260949"};
        var talkAction = new TalkAction
        {
            Text = "Thanks for thinking of me. So, what's the question about?", 
            Language = "en-GB", 
            Style = 6,
            Premium = true,
        };
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
        var ncco = new Ncco(talkAction, inputAction);
        var command = new CallCommand {To = [toEndpoint], From = fromEndpoint, Ncco = ncco};
        var response = await client.CreateCallAsync(command);
        return this.Ok(response);
    }
}