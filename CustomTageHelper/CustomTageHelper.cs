
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookMovieShow.CustomTageHelper
{
    [HtmlTargetElement("MyImg")]
    public class CustomTageHelper : TagHelper
    {
        public string? ImageLink { get; set; }
        public string? AlternativeText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Your logic to modify or generate HTML output
            base.Process(context, output);
            output.TagName = "img";
            output.TagMode = TagMode.StartTagOnly;

            output.Attributes.SetAttribute("src", ImageLink);
            output.Attributes.SetAttribute("alt", AlternativeText);


        }
    }
}
