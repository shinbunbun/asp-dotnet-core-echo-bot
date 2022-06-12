using Microsoft.AspNetCore.Mvc;
namespace EchoBot.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookController : ControllerBase
{
  public WebhookController()
  {
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Ok();
  }

  [HttpPost]
  public IActionResult Webhook(Object body)
  {
    string? stringBody = body.ToString();
    if (stringBody == null)
    {
      return BadRequest();
    }
    dynamic? data = System.Text.Json.JsonSerializer.Deserialize<System.Dynamic.ExpandoObject>(stringBody);
    string? text = data?.events[0].GetProperty("message").GetProperty("text").GetString();
    string? replyToken = data?.events[0].GetProperty("replyToken").GetString();
    Models.ReplyTextMessage message = new Models.ReplyTextMessage(replyToken, "text", text);
    message.Reply();
    

    return Ok();
  }
}