using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = Kernel.CreateBuilder();

// Suppress the diagnostic warning SKEXP0070 by explicitly acknowledging it
#pragma warning disable SKEXP0070
builder.AddOllamaChatCompletion("llama3.2:latest", new Uri("http://localhost:11434"));
#pragma warning restore SKEXP0070

var kernel = builder.Build();
var chatService = kernel.GetRequiredService<IChatCompletionService>();

var history = new ChatHistory();
history.AddSystemMessage("You are a helpful assistant.");

while (true)
{
    Console.Write("You: ");
    var userMessage = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userMessage))
    {
        break;
    }

    history.AddUserMessage(userMessage);
    var response = await chatService.GetChatMessageContentAsync(history);
    Console.WriteLine($"ESAP AI Agent: {response.Content}");
}
