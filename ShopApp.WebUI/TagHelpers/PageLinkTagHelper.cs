using Microsoft.AspNetCore.Razor.TagHelpers;
using ShopApp.WebUI.Models.Products;
using System.Text;

namespace ShopApp.WebUI.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        public PageInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<nav aria-label='Page navigation example'>");
            stringBuilder.Append("<ul class='pagination justify-content-end'>");

            for (int i = 1; i <= PageModel.TotalPages(); i++)
            {
                stringBuilder.AppendFormat("<li class='page-item {0}'>", i == PageModel.CurrentPage ? "active" : "");

                if (!string.IsNullOrEmpty(PageModel.CurrentCategory))
                    stringBuilder.AppendFormat("<a class='page-link' href='/products?page={0}'>{0}</a>", i);
                else
                    stringBuilder.AppendFormat("<a class='page-link' href='/products/{1}?page={0}'>{0}</a>", i, PageModel.CurrentCategory);

                stringBuilder.Append("</li>");
            }

            stringBuilder.Append("</ul>");
            stringBuilder.Append("</nav>");

            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}
