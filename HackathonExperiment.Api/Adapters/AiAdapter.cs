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
        "So, the T28 measures 97mm long. As far as I know, Ericsson sold approximately 10 millions units. Stacking them would be... Please give me a second... Around 970 kilometers. It has to be Munich to Paris then!");
}