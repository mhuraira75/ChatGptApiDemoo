using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatGptApiDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "generate your own key"; // Replace with your API key from OpenAI
            string endpoint = "https://api.openai.com/v1/chat/completions"; // ChatGPT API endpoint

            // Set up the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Add authorization header
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

                // Create the request body with JSON content for ChatGPT
                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Model name (You can change this to gpt-4 or other models)
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = "Hello, ChatGPT!" }
                    }
                };

                string jsonRequestBody = JsonConvert.SerializeObject(requestBody);

                // Send the POST request to OpenAI API
                HttpResponseMessage response = await client.PostAsync(
                    endpoint,
                    new StringContent(jsonRequestBody, Encoding.UTF8, "application/json")
                );

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                    // Extract the reply from the API response
                    string chatGptReply = jsonResponse.choices[0].message.content;

                    // Display the response from ChatGPT
                    Console.WriteLine("ChatGPT says: " + chatGptReply);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }
    }
}
