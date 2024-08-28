namespace HackathonExperiment.Api.Adapters;

public interface IAiAdapter
{
    Task<string> AskAsync(string question);
}

internal class AiAdapter : IAiAdapter
{
    public Task<string> AskAsync(string question) => throw new NotImplementedException();
}

internal class FakeAiAdapter : IAiAdapter
{
    public Task<string> AskAsync(string question) => Task.FromResult(
        "<speak>So, the T28 measures 97mm long. As far as I know, Ericsson sold approximately 10 millions units. Stacking them would be... <break time='1s'/> Please give me a moment <break time='1s'/> Around 970 kilometers. <emphasis level='strong'>It has to be Munich to Paris then!</emphasis></speak>");
}