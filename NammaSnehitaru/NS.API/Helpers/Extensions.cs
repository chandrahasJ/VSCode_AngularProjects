using System;
using Microsoft.AspNetCore.Http;

namespace NS.API.Helpers
{
    public static class Extensions
    {
        public static void AddCustomHeader(this HttpResponse response,string message){
            response.Headers.Add("Application-Message",message);
            response.Headers.Add("Access-Control-Expose-Header","Application-Message");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }

        public static int CalCulateAge(this DateTime theDateTime)
        {
            int age = DateTime.Now.Year - theDateTime.Year;

            return (DateTime.Now.AddYears(age) > DateTime.Today ? age-- : age);
        }
    }
}