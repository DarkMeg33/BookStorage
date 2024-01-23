using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BookStorage.Extensions
{
    public static class HtmlHelperExtensions
    {
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
            return WrapWithScriptTag(helper.Raw($"_set('{key}', {JsonConvert.SerializeObject(value)})"));
        }
    }
}