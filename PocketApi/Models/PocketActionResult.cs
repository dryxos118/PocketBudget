using System;
using System.Runtime.Serialization;

namespace PocketApi.Models
{
    public class PocketActionResult(string message, ErrorType type, string? location = null, object? detail = null) : Exception(message)
    {
        public ErrorType Type { get; set; } = type;
        public string? Location { get; set; } = location;
        public object? Details { get; set; } = detail;
    }

    public enum ErrorType
    {
        [EnumMember(Value = "Success")]
        Success = 200,
        [EnumMember(Value = "BadRequest")]
        BadRequest = 400,
        [EnumMember(Value = "Unauthorized")]
        Unauthorized = 401,
        [EnumMember(Value = "Forbidden")]
        Forbidden = 403,
        [EnumMember(Value = "NotFound")]
        NotFound = 404,
        [EnumMember(Value = "InternalServerError")]
        InternalServerError = 500
    }
}
