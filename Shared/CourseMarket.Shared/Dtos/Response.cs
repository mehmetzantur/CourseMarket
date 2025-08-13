using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CourseMarket.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get;  set; }

        [JsonIgnore]
        public int StatusCode { get;  set; }

        [JsonIgnore]
        public bool IsSuccessful { get;  set; }

        public List<string> Errors { get; set; }

        public static Response<T> Success(T data, int statusCode = 200)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(List<string> errors, int statusCode = 400)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(string error, int statusCode = 400)
        {
            return new Response<T> { Errors = new List<string> { error }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
