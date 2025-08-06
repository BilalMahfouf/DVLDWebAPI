using Core.Shared;
using Microsoft.AspNetCore.Mvc;
using static Core.Common.Enums;

namespace WebAPI.Controllers.Extensions
{
    
        /// <summary>
        /// Extension methods for converting <see cref="Result"/> and <see cref="Result{T}"/> objects 
        /// into appropriate <see cref="ActionResult"/> responses for ASP.NET Core Web APIs.
        /// </summary>
        public static class ResultExtensions
        {
            /// <summary>
            /// Converts a <see cref="Result{T}"/> to an appropriate <see cref="ActionResult{T}"/>.
            /// </summary>
            /// <typeparam name="T">The type of the returned value (must be a non-nullable value or reference type).</typeparam>
            /// <param name="result">The result object representing either a successful operation with a value or an error.</param>
            /// <param name="createdActionName">
            /// The name of the action to generate a 201 Created response using <see cref="CreatedAtActionResult"/>.
            /// If null or empty, a standard 200 OK response is returned using <see cref="OkObjectResult"/>.
            /// </param>
            /// <param name="routeValues">
            /// Route values used to generate the URL in the Location header when <paramref name="createdActionName"/> is provided.
            /// Required if <paramref name="createdActionName"/> is not null or empty.
            /// </param>
            /// <returns>
            /// - <see cref="CreatedAtActionResult"/> if <paramref name="createdActionName"/> is provided and the result is successful.<br/>
            /// - <see cref="OkObjectResult"/> if the result is successful and no action name is provided.<br/>
            /// - An appropriate error response (e.g. 400, 404, 409, 500) if the result is not successful.
            /// </returns>
            /// <exception cref="ArgumentNullException">
            /// Thrown if <paramref name="createdActionName"/> is provided but <paramref name="routeValues"/> is null.
            /// </exception>
            public static ActionResult<T> HandleResult<T>(
                this GenericResult<T> result,
                string? createdActionName = null,
                object? routeValues = null) where T : notnull
            {
                if (!result.IsSuccess)
                    return ToErrorActionResult(result);

                if (!string.IsNullOrEmpty(createdActionName))
                {
                    if (routeValues == null)
                        throw new ArgumentNullException(nameof(routeValues));

                    return new CreatedAtActionResult(
                        actionName: createdActionName,
                        controllerName: null,
                        routeValues: routeValues,
                        value: result.Data);
                }

                return new OkObjectResult(result.Data);
            }

            /// <summary>
            /// Converts a <see cref="Result{T}"/> containing a collection to an appropriate <see cref="ActionResult"/>.
            /// </summary>
            /// <typeparam name="T">The type of items in the collection (must be non-nullable).</typeparam>
            /// <param name="result">The result object containing a collection or an error.</param>
            /// <returns>
            /// - <see cref="OkObjectResult"/> if the result is successful.<br/>
            /// - An appropriate error response if the result is not successful.
            /// </returns>
            public static ActionResult HandleResult<T>(this GenericResult<IEnumerable<T>> result) where T : notnull
            {
                return result.IsSuccess
                    ? new OkObjectResult(result.Data)
                    : ToErrorActionResult(result);
            }

            /// <summary>
            /// Converts a non-generic <see cref="Result"/> (used for void operations) to an appropriate <see cref="ActionResult"/>.
            /// </summary>
            /// <param name="result">The result object indicating success or failure.</param>
            /// <returns>
            /// - <see cref="NoContentResult"/> (HTTP 204) if successful.<br/>
            /// - An appropriate error response if failed.
            /// </returns>
            public static ActionResult HandleResult(this Result result)
            {
                return result.IsSuccess
                    ? new NoContentResult()
                    : ToErrorActionResult(result);
            }

            /// <summary>
            /// Converts a failed <see cref="Result"/> to a corresponding error response.
            /// </summary>
            /// <param name="result">The result object containing the error.</param>
            /// <returns>
            /// A matching <see cref="ActionResult"/> based on the <see cref="ErrorType"/>:
            /// <list type="bullet">
            /// <item><see cref="NotFoundObjectResult"/> for <see cref="ErrorType.NotFound"/></item>
            /// <item><see cref="BadRequestObjectResult"/> for <see cref="ErrorType.BadRequest"/></item>
            /// <item><see cref="ConflictObjectResult"/> for <see cref="ErrorType.Conflict"/></item>
            /// <item><see cref="UnauthorizedObjectResult"/> for <see cref="ErrorType.Unauthorized"/></item>
            /// <item><see cref="ObjectResult"/> (500) for unhandled error types</item>
            /// </list>
            /// </returns>
            private static ActionResult ToErrorActionResult(Result result)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => new NotFoundObjectResult(result.ErrorMessage),
                    ErrorType.BadRequest => new BadRequestObjectResult(result.ErrorMessage),
                    ErrorType.Conflict => new ConflictObjectResult(result.ErrorMessage),
                   // ErrorType.Unauthorized => new UnauthorizedObjectResult(result.Error),
                    _ => new ObjectResult(result.ErrorMessage) { StatusCode = 500 }
                };
            }
        }
    }

