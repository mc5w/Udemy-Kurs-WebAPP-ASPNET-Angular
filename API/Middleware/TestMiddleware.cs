using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;

namespace API.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate next;

        public TestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context){
                await next(context);
            throw new NotImplementedException();
        }
    }
}