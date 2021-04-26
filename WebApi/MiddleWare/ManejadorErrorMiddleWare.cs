using Aplicacion.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.MiddleWare
{
    public class ManejadorErrorMiddleWare
    {
        public readonly RequestDelegate _next;
        public readonly ILogger _logger;
        public ManejadorErrorMiddleWare(RequestDelegate next, ILogger<ManejadorErrorMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await ManejadorExcepcionAsync(context, ex, _logger);
            }
        }

        private async Task ManejadorExcepcionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            object errores = null;
            switch (ex)
            {
                case ManejadorExcepcion me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me._Errores;
                    context.Response.StatusCode = (int)me._Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            if(errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}
