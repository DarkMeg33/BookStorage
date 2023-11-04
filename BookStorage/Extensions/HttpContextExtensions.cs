namespace BookStorage.Extensions
{
    public static class HttpContextExtensions
    {
        public static T GetService<T>(this HttpContext context)
        {
            return context.RequestServices.Get<T>();
        }
    }
}