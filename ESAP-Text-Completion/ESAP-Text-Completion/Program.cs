using OpenAI.Chat;

Console.Title = "ESAP AI Assistant 🤖";

ChatClient client = new(
    model: "gpt-4o-mini",
    apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")
);

while (true)
{
    Console.Write("\nEnter your question (or type 'exit' to quit): ");
    string userPrompt = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userPrompt) || userPrompt.ToLower() == "exit")
        break;

    Console.Write("\n ESAP AI Agent: ");

    await foreach (var message in client.CompleteChatStreamingAsync(userPrompt))
    {
        foreach (var item in message.ContentUpdate)
        {
            Console.Write(item.Text);
        }
    }

    Console.WriteLine(); // line break after response
}
