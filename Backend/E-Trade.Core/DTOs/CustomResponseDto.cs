using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Trade.Core.DTOs
{
    // Kullancı tarafına her işlem sonrası farklı yapılar dönmek UI tarafında zorluklar çıkarabilir.
    // Bu sorunu ortadan kaldırmak için tek bir yapı dönülmesi gerekir.
    // Generic CustomResponseDto class işlem sonucu ne olursa olsun UI tarafına tek bir yapı dönmesini sağlıyor.

    // CustomResponseDto Generic class
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        // Static Factory Design Pattern
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T>{ StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
