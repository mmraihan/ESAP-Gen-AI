using OpenAI.Chat;

ChatClient client = new(
  model: "gpt-4o-mini",
  apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")
);

string prompt = "Give me an overview ESAP Company, which is located KSA.";

await foreach (var message in client.CompleteChatStreamingAsync(prompt))
{
    foreach (var item in message.ContentUpdate)
    {
        Console.Write(item.Text);
    }
}

Console.ReadKey();