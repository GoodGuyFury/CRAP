using code_review_analysis_platform.Enums;
using System.Text.Json.Serialization;

namespace code_review_analysis_platform.Responses
{
    public class ApiResponse<T>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }  // Enum for Success/Failed
        public string Message { get; set; } // Default/custom message
        public T? Data { get; set; }         // Generic data object

        // Constructor for default success response
        public ApiResponse(T data, string message = "Operation successful")
        {
            Status = Status.Success;
            Message = message;
            Data = data;
        }

        // Constructor for error response
        public ApiResponse(string message = "Operation failed")
        {
            Status = Status.Failed;
            Message = message;
            Data = default; // No data in case of failure
        }

        // Static helper methods for cleaner usage
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(data, message);
        }

        public static ApiResponse<T> ErrorResponse(string message = "Operation failed")
        {
            return new ApiResponse<T>(message);
        }
    }

}
