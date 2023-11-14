namespace usipav_cockpit.Middlewares
{
    public class RedirectToLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401 && !context.Request.Path.StartsWithSegments("/Users/Login"))
            {
                context.Response.Redirect("/Users/Login");
            }
        }
    }
}
