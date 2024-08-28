#region
using ChatGPT.Net;
using ChatGPT.Net.DTO.ChatGPT;
#endregion

namespace HackathonExperiment.Api.Adapters;

public interface IAiAdapter
{
    Task<string> AskAsync(string question);
}

internal class AiAdapter(IConfiguration configuration) : IAiAdapter
{
    public async Task<string> AskAsync(string question)
    {
        var client = new ChatGpt(
            configuration["openai-key"] ?? throw new InvalidOperationException("Missing OpenAI key."),
            new ChatGptOptions
            {
                Model = "gpt-4-turbo",
            });
        return await client.Ask(configuration["context"] + "\n" + question);
    }
}