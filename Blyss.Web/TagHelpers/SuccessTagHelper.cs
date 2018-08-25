namespace Blyss.Web.TagHelpers
{

    using System.Text;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class SuccessTagHelper : TagHelper
    {

        public string Message { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(this.Message))
            {
                return;
            }

            output.TagName = "span";

            StringBuilder sb = new StringBuilder();

            sb.Append($@"<h5 class=""alert alert-success text-center"">{this.Message}</h5>");


            output.Content.SetHtmlContent($"{sb}");
        }

    }

}