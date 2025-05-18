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



#region Testing v-1

//using Microsoft.SemanticKernel;
//using Microsoft.SemanticKernel.ChatCompletion;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        // STEP 1: Build the Kernel and Add LLM via Ollama
//        var builder = Kernel.CreateBuilder();

//        // Suppress the diagnostic warning SKEXP0070 by explicitly acknowledging it
//#pragma warning disable SKEXP0070
//        builder.AddOllamaChatCompletion(
//            modelId: "llama3.2:latest",
//            endpoint: new Uri("http://localhost:11434")
//        );
//#pragma warning restore SKEXP0070

//        // Build the Kernel instance
//        var kernel = builder.Build();

//        // Retrieve the chat completion service (LLM interface)
//        var chatService = kernel.GetRequiredService<IChatCompletionService>();

//        // STEP 2: Set Initial Chat History with System Role
//        var history = new ChatHistory();
//        history.AddSystemMessage("You are a helpful assistant for the ESAP ERP system.");

//        // STEP 3: Begin interactive loop
//        while (true)
//        {
//            Console.Write("\nAsk ESAP AI Agent: ");
//            string userMessage = Console.ReadLine();

//            // Exit condition
//            if (string.IsNullOrWhiteSpace(userMessage) || userMessage.ToLower() == "exit")
//                break;

//            // Add user input to chat history
//            history.AddUserMessage(userMessage);

//            // Get LLM-generated response based on history
//            var response = await chatService.GetChatMessageContentAsync(history);

//            // Print the result from the AI
//            Console.WriteLine($"\n🤖 ESAP AI Agent: {response.Content}");
//        }
//    }
//}



#endregion

