using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.API.ApiResource
{
    public class Response<T>
    {
        public string message { get; set; } = "success";
        public T data { get; set; }
    }
}
