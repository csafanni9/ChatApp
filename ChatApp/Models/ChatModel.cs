using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    // CHAT_APP_KEY: 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
    // My API Key: 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
    public class ChatModel
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GenerateText(string prompt)
        {
            string apiKey = "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";
            string endpoint = "https://api.openai.com/v1/chat/completions";

            string message = "[{\"role\": \"user\", \"content\": \"" + $"{prompt}" + "\"}]";

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Authorization", $"Bearer {apiKey}");
            request.Content = new StringContent(
                $"{{ \"model\": \"gpt-3.5-turbo\", \"messages\": {message}, \"temperature\": 1, \"top_p\": 1, \"frequency_penalty\": 0, \"presence_penalty\": 0 }}",
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var generatedText = "";
            if (content.Contains("content"))
            {
                int startInd = content.IndexOf("content");
                startInd = startInd + 11;
                char[] charArray = content.ToCharArray();
                char c = charArray[startInd];
                while (c != '"')
                {
                    if (c == '\\' && charArray[startInd + 1] == 'n')
                    {
                        generatedText = generatedText + "\n";
                        startInd++;
                    }
                    else
                    {
                        generatedText = generatedText + $"{c}";
                    }
                    startInd++;
                    c = charArray[startInd];
                }
            }

            return generatedText;
        }
    }
}