#pragma checksum "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ff2507476ea112269c7901fe4d5ccbc198380552"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Ecommerce.Views.Customer.Views_Customer_Details), @"mvc.1.0.view", @"/Views/Customer/Details.cshtml")]
namespace Ecommerce.Views.Customer
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff2507476ea112269c7901fe4d5ccbc198380552", @"/Views/Customer/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b88a5823a23ee2afcba0f5690a425aaa8fabd953", @"/_ViewImports.cshtml")]
    public class Views_Customer_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Ecommerce.Models.Account>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
  
    ViewData["Title"] = "Invoice Details";
    Layout = "~/Views/Shared/_UserLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            WriteLiteral("\n<ul class=\"breadcrumb\">\n    <li>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff2507476ea112269c7901fe4d5ccbc1983805523674", async() => {
                WriteLiteral("Главная");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" <span class=""divider"">/</span></li>
    <li class=""active"">Избранное</li>
</ul>

<section id=""Table"">
    <div class=""page-header"">
        <h3>Invoice Details</h3>
    </div>
    <div class=""row-fluid"">

        <div class=""span12"">
            <table class=""table"">
                <thead>
                    <tr>
                        <th>Product Id</th>
                        <th>Product Name</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Sub Total</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 32 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                     foreach (var invoicesDetail in ViewBag.invoicesDetails)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td>");
#nullable restore
#line 35 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                           Write(invoicesDetail.Product.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 36 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                           Write(invoicesDetail.Product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>$");
#nullable restore
#line 37 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                            Write(invoicesDetail.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 38 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                           Write(invoicesDetail.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>$");
#nullable restore
#line 39 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                             Write(invoicesDetail.Price * invoicesDetail.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        </tr>\n");
#nullable restore
#line 41 "/Users/nszhukov/Documents/Projects/dbcoursework-mainq/Views/Customer/Details.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n\n    </div>\n</section>\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Ecommerce.Models.Account> Html { get; private set; }
    }
}
#pragma warning restore 1591
