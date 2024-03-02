using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookStorage.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent HideHeader(this IHtmlHelper helper)
        {
            return new HtmlString("<link rel=\"stylesheet\" href=\"/css/hide-header.css\" />");
        }

        public static IHtmlContent HideFooter(this IHtmlHelper helper)
        {
            return new HtmlString("<link rel=\"stylesheet\" href=\"/css/hide-footer.css\" />");
        }

        public static async Task<IHtmlContent> WrapWithScriptTag(IHtmlContent html)
        {
            TagBuilder tagBuilder = new TagBuilder("script");

            await using StringWriter sw = new StringWriter();
            tagBuilder.RenderStartTag().WriteTo(sw, HtmlEncoder.Default);
            html.WriteTo(sw, HtmlEncoder.Default);
            tagBuilder.RenderEndTag().WriteTo(sw, HtmlEncoder.Default);
            
            return new HtmlString(sw.ToString());
        }

        public static Task<IHtmlContent> SetClientSideJavascriptVariableAsync<TModel>(
            this IHtmlHelper<TModel> helper, string key, object value)
        {
            return WrapWithScriptTag(helper.Raw($@"_set('{key}', {JsonConvert.SerializeObject(value)})"));
        }
    }
}