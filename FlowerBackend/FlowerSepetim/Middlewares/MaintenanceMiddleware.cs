using CicekSepeti.Domain.Interfaces; //  Sadece Domain Interface'i tanıyor

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

        // DİKKAT: Method Injection ile direkt IFlowerRepository alıyoruz.
        // Service yok, aracı yok. Direkt Repository Pattern.
        public async Task Invoke(HttpContext context, IFlowerRepository repository)
        {
            // 1. Veritabanından durumu soruyoruz.
            bool isMaintenance = await repository.GetMaintenanceStateAsync();

            if (isMaintenance)
            {
                var path = context.Request.Path.Value?.ToLower();

                // 2. İzinliler (Login, Toggle, Swagger)
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