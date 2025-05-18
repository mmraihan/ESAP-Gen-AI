using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

namespace ESAP.Knowledge_Base.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIAgentController : ControllerBase
    {
        private readonly ChatClient _client;

        public AIAgentController()
        {
            _client = new ChatClient(
                model: "gpt-4o-mini",
                apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")
            );
        }

   
        [HttpPost("ask")]
        public async Task<IActionResult> AskAgent([FromBody] UserPromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("Prompt cannot be empty.");

            string result = "";

            await foreach (var message in _client.CompleteChatStreamingAsync(request.Prompt))
            {
                foreach (var item in message.ContentUpdate)
                {
                    result += item.Text;
                }
            }

            return Ok(new { AgentResponse = result });
        }
    }

    public class UserPromptRequest
    {
        public string Prompt { get; set; }
    }
}
