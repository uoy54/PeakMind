using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static PeakMind.Pages.MainPage;
using static System.Net.Mime.MediaTypeNames;

namespace PeakMind.Services
{
    public class Aiservices
    {
        private static readonly HttpClient client = new HttpClient();

        private const string apiKey = "AIzaSyBDgAXVMOt4bgqSpytRTemYKQCv9qI6MtM";
        private const string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=" + apiKey;
        public async Task<string> AnalyzeAsync(string text)
        {
            {
                var request = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new {text = text }
                            }
                        }
                    }


                };
                string json = JsonSerializer.Serialize(request);

                var content = new StringContent(
                json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Clear();
                var response = await client.PostAsync(url, content);
                string resultjson = await response.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Raw Response",resultjson,"ok");

                try
                {
                    using var doc = JsonDocument.Parse(resultjson);

                    return doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();
                }
                catch
                {
                    return "error with an answer";
                }
            }
        }
    }
       

    }



