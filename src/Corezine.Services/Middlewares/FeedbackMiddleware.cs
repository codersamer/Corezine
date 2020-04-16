using Corezine.Services.Contracts;
using Corezine.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Corezine.Services.Middlewares
{
    public class FeedbackMiddleware
    {
        public RequestDelegate Next { get; }

        

        public FeedbackMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            FeedbackService.Context = context;
            await Next.Invoke(context);
        }
    }
}
