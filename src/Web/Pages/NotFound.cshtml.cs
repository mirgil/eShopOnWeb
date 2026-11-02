using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.eShopWeb.Web.Pages;

public class NotFoundModel : PageModel
{
    public string RequestedPath { get; private set; } = string.Empty;

    public void OnGet()
    {
        var originalPath = HttpContext.Features.Get<IStatusCodeReExecuteFeature>()?.OriginalPath
            ?? HttpContext.Request.Path.Value;
        RequestedPath = HttpContext.Request.PathBase + originalPath;
        Response.StatusCode = StatusCodes.Status404NotFound;
    }
}
