using System;
using System.Net.Http;
using Xunit;
using ValueType = Api.Domain.ValueType;

namespace Api.EndToEndTest
{
    public class EndToEndTest
    {
        [Fact]
        public void InsertAndRetrieveValue()
        {
            var apiEndpoint = new Uri("http://localhost:5000/api/values");

            var httpClient = new HttpClient();

            var postResponse = httpClient.PostAsJsonAsync(apiEndpoint,new ValueType()
            {
                Id = 1,
                Value = "hello"
            }).Result;

            postResponse.EnsureSuccessStatusCode();

            var value = httpClient.GetAsync($"{apiEndpoint}/1").Result.Content.ReadAsStringAsync().Result;
            
            Assert.Equal("hello", value);
        }
    }
}