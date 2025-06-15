﻿namespace API.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";

        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.Headers.ContainsKey(CorrelationIdHeader)
                ? context.Request.Headers[CorrelationIdHeader].ToString()
                : Guid.NewGuid().ToString();

            context.Items[CorrelationIdHeader] = correlationId;

            context.Response.Headers[CorrelationIdHeader] = correlationId;

            await _next(context);
        }
    }

}
