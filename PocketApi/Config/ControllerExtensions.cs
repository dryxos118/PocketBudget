using System.Security.Claims;
using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using PocketApi.Models;

namespace PocketApi.Config
{
    public static class ControllerExtensions
    {
        public static IActionResult HandlePocketActionResult(this ControllerBase controller, PocketActionResult ex,
            ILogger logger)
        {
            if (string.IsNullOrEmpty(ex.Location))
            {
                var action = controller.ControllerContext.ActionDescriptor.ActionName;
                var controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;
                ex.Location = $"{controllerName}.{action}";
            }

            logger.LogWarning($"Warning: {ex.Message}, Location: {ex.Location}");

            object resultObj = new
                { type = ex.Type.GetName(), message = ex.Message, location = ex.Location, details = ex.Details };


            return ex.Type switch
            {
                ErrorType.Success => controller.Ok(resultObj),
                ErrorType.BadRequest => controller.BadRequest(resultObj),
                ErrorType.Unauthorized => controller.Unauthorized(resultObj),
                ErrorType.NotFound => controller.NotFound(resultObj),
                ErrorType.InternalServerError => controller.StatusCode(500, resultObj),
                _ => controller.StatusCode((int)ex.Type, resultObj)
            };
        }

        public static void HandleModelValidation(this ControllerBase controller, ILogger logger)
        {
            if (!controller.ModelState.IsValid)
            {
                object errors = controller.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    }).ToArray();

                throw new PocketActionResult(
                    "Validation failed for one or more fields.",
                    ErrorType.BadRequest,
                    detail: errors
                );
            }
        }

        public static int GetUserId(this Controller controller)
        {
            string? userId = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new PocketActionResult("No user found", ErrorType.Unauthorized);
            }
            return int.Parse(userId);
        }
    }
}