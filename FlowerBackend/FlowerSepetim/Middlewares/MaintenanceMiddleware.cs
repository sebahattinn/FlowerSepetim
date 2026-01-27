using CicekSepeti.Domain.Interfaces; 

namespace CicekSepeti.API.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MaintenanceMiddleware> _logger;

        public MaintenanceMiddleware(RequestDelegate next, ILogger<MaintenanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        
        public async Task Invoke(HttpContext context, IFlowerRepository repository)
        {
           
            bool isMaintenance = await repository.GetMaintenanceStateAsync();

            if (isMaintenance)
            {
                var path = context.Request.Path.Value?.ToLower();

               
                bool isAllowed = path != null && (
                    path.Contains("/api/auth/login") ||
                    path.Contains("/api/settings/toggle") ||
                    path.StartsWith("/swagger")
                );

                if (!isAllowed)
                {
                    _logger.LogWarning($"Bakım modu aktif. Erişim engellendi: {path}");

                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\": \"Site bakım modundadır.\"}");
                    return; // İsteği kes
                }
            }

            await _next(context);
        }
    }
}