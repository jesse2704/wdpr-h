#pragma checksum "/Users/jesseakoma/Documents/HHSJ2/WDPR/wdpr-h/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edf85cafe647b99a9a320e43862207eeda18d604"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/jesseakoma/Documents/HHSJ2/WDPR/wdpr-h/Views/_ViewImports.cshtml"
using wdpr_h;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/jesseakoma/Documents/HHSJ2/WDPR/wdpr-h/Views/_ViewImports.cshtml"
using wdpr_h.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf85cafe647b99a9a320e43862207eeda18d604", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39e9863174b79c2b89d4df27493af9838583a1c8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/dist/css/bootstrap.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/site.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/jesseakoma/Documents/HHSJ2/WDPR/wdpr-h/Views/Home/Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "edf85cafe647b99a9a320e43862207eeda18d6044368", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>");
#nullable restore
#line 7 "/Users/jesseakoma/Documents/HHSJ2/WDPR/wdpr-h/Views/Home/Index.cshtml"
      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - wdpr_h</title>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "edf85cafe647b99a9a320e43862207eeda18d6044988", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "edf85cafe647b99a9a320e43862207eeda18d6046151", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<div class=""text-center"">
    <b style = ""font-size:50px"">Zorg Maatschap Den Haag</b>
</div>

<div class=""text-center"">
    <img src=""img/pedagogiekk.jpg"" class=""img-fluid"" alt=""Praktijk voor Orthopedagogiek & psychologie"">
</div>


<b class=""img__alt"">
    Praktijk voor Orthopedagogiek & psychologie
</b>

<h2 class=""overons-altekst"">Mijn visie</h2>
  <p>
    Ik zal alleen gegevens delen met derden als ouders/verzorgers en/of kinderen ouder dan 12 jaar schriftelijk
    toestemming heeft verleend om vertrouwelijke informatie te delen met derden.
  </p>

  <h2 class=""overons-altekst"">Mijn missie</h2>
  <p>
    Mijn missie is het om uw bijzondere kind te ondersteunen op zijn/haar pad. Herkent u de worsteling
    van uw kind? Beleeft uw kind dingen intensief? Is uw kind uit balans op school en gaan zijn/haar
    prestaties achteruit? Is uw kind faalangstig? Concentreert uw kind zich moeilijk? Slaap uw kind
    moeilijk na een onrustige dag? Heeft uw kind moeite met grote veranderingen ");
            WriteLiteral(@"of met verrassingen?
    Dit zijn allemaal kenmerken van hooggevoeligheid. Kom gerust met uw kind langs, dan bespreken we
    in een vrijblijvend oriënterend gesprek wat ik voor u en vooral voor uw kind kan betekenen.
  </p>

  <h2 class=""overons-altekst"">Mijn kernwaarden</h2>
  <b>Luisteren</b>
  <p>
    Ieder kind mag zichzelf zijn
  </p>
  <b>Liefde:</b>
  <p>
    Ieder kind mag gelukkig zijn
  </p>
  <b>Aandacht:</b>
  <p>
    Ieder kind verdient oprechte aandacht
  </p>

  <h2 class=""overons-altekst"">Hoe kan je hooggevoeligheid herkennen?</h2>
  <span>(Kenmerken hooggevoeligheid)</span>
  <p>
  <ul>
    <li>Hebben vaak veel inzicht hebben in gevoelens van anderen</li>
    <li>Beleven dingen vaak intensief</li>
    <li>Kunnen snel schrikken</li>
    <li>Nemen snel emoties over van anderen</li>
    <li>Hebben snel last van kleren die kriebelen, naden in sokken of kledingmerkjes tegen de huid</li>
    <li>Houden vaak niet van verrassingen</li>
    <li>Kunnen snel van slag raken ");
            WriteLiteral(@"als iemand boos of heftig reageert</li>
    <li>Lijken geregeld gedachten te kunnen lezen</li>
    <li>Ruiken vaak elk vreemd geurtje</li>
    <li>Hebben een scherpzinnig gevoel voor humor</li>
    <li>Zijn vaak zeer intuïtief</li>
    <li>Kunnen vaak moeilijk in slaap komen na een opwindende dag</li>
    <li>Hebben vaak moeite met grote veranderingen</li>
    <li>Tellen vaak veel vragen</li>
    <li>Zijn vaak perfectionistisch</li>
    <li>Houden meestal van rustige spelletjes</li>
    <li>Kunnen diepzinnige, beschouwende vragen stellen</li>
  </ul>
  </p>

  <h2 class=""overons-altekst"">Wat maakt mijn praktijk anders?</h2>
  <p>
    Als jij samen met mij gaat onderzoeken hoe jij je weer prettiger voelt in de wereld, dan
    volgen we jouw tempo. Ik luister met aandacht naar jou! Omdat ik vooral kinderen begeleid
    die hooggevoelig zijn, heb ik veel ervaring opgebouwd met waar jij tegenaan loopt! Dat
    onderscheid mijn praktijk van de praktijken van anderen. Èn….ik breng kinderen met de");
            WriteLiteral("zelfde\r\n    ‘problemen’, bij elkaar. Elke maand is er een ‘lotgenoten’ inloopmiddag. Ook van elkaar\r\n    kunnen jullie heel veel leren!\r\n  </p>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
