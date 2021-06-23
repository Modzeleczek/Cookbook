using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

public class GlobalPageFilter : IPageFilter
{
    private string companyName { get; set; }
    public GlobalPageFilter(IConfiguration configuration)
    {
        companyName = configuration.GetValue<string>("AppSettings:CompanyName");
    }
    public void OnPageHandlerSelected(PageHandlerSelectedContext pageContext) { }
    public void OnPageHandlerExecuting(PageHandlerExecutingContext pageContext) { }
    public void OnPageHandlerExecuted(PageHandlerExecutedContext pageContext)
    {
        SaveCompanyName(pageContext);
        SaveCurrentPath(pageContext);
    }
    private void SaveInViewData(PageHandlerExecutedContext pageContext, string key, object value)
    {
        var result = pageContext.Result;
        if ((result is PageResult) == false)
            return;
        var page = (PageResult)result;
        page.ViewData[key] = value;
    }
    private void SaveCompanyName(PageHandlerExecutedContext pageContext)
    {
        SaveInViewData(pageContext, "companyName", companyName); // zapisujemy w ViewData strony, na której działa filtr, nazwę firmy, którą w momencie konstrukcji filtra w Startup.ConfigureServices pobieramy z appsettings.json
    }
    private void SaveCurrentPath(PageHandlerExecutedContext pageContext)
    {
        string currentPath = pageContext.HttpContext.Request.Path; // zapisujemy ścieżkę do aktualnie renderowanej strony (np. /Products/List)
        string queryString = pageContext.HttpContext.Request.QueryString.Value; // query string zawiera parametry do strony i ma postać ?parametr1=d&parametr2=5...
        if (queryString.Length > 0) // jeżeli mamy jakiekolwiek parametry do strony
        {
            if (currentPath.EndsWith('/') == false) // jeżeli ścieżka nie ma na końcu "/"
                currentPath += "/"; // dopisujemy "/"
            currentPath += queryString; // dopisujemy parametry (np. ?id=2)
        }
        SaveInViewData(pageContext, "currentPath", currentPath);
    }
}