using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Models.Common
{

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        //public string Warning { get; set; }
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string QueryString { get; set; }
        public string EndPointName { get; set; }

        public ApiResponse<T> ToApiResponse<T>()
        {
            return new ApiResponse<T>()
            {
                Title = Title,
                Message = Message,
                IsSuccess = IsSuccess
            };
        }
    }
}
