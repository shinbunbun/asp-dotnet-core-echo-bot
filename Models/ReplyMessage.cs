namespace EchoBot.Models;

public class ReplyTextMessage
{
  public ReplyTextMessage(string? replyToken, string? type, string? text)
  {
    var message = new Message(type, text);
    this.replyToken = replyToken;
    this.messages = new Message[] { message };
  }
  public string? replyToken { get; set; }
  public Message[] messages { get; set; }

  public void Reply()
  {
    var json = System.Text.Json.JsonSerializer.Serialize(this);
    Console.WriteLine(json);
    var content = new StringContent(json, System.Text.Encoding.UTF8, @"application/json");

    using (var client = new HttpClient())
    {
      var request = new HttpRequestMessage(HttpMethod.Post, @"https://api.line.me/v2/bot/message/reply");
      request.Headers.Add(@"Authorization", @"Bearer {token}");
      request.Content = content;
      var res = client.Send(request);
      // Console.WriteLine(res);
    }
  }
}

public class Message
{
  public Message(string? type, string? text)
  {
    this.type = type;
    this.text = text;
  }
  public string? type { get; set; }
  public string? text { get; set; }
}