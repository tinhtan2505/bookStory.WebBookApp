using System;
using System.Collections.Generic;
using System.Text;

namespace bookStory.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
        }

        public ApiErrorResult(string message)// 1 lỗi
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationErrors)// nhiều lỗi
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}