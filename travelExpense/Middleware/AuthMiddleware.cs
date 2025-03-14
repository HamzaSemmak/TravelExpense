namespace travelExpense.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (path == "/auth/login" || path == "/auth/logout" || path.StartsWith("/css") || path.StartsWith("/js"))
            {
                await _next(context);
                return;
            }

            if (context.Session.GetString("User") == null)
            {
                context.Response.Redirect("/auth/login");
                return;
            }

            await _next(context);
        }
    }
}