using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri,                  //Because Get Method returns a string in form of a Json object.
            string authorizationToken = null,
            string authorizationMethod = "Bearer");
        
        Task<HttpResponseMessage> PostAsync<T>(string uri,
            T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");
        
        Task<HttpResponseMessage> PutAsync<T>(string uri,
            T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer"); 
        
        Task<HttpResponseMessage> DeleteAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");


    }
}
