// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace AcheruFriends.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Acheru\MyFriends\client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Acheru\MyFriends\client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Acheru\MyFriends\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Acheru\MyFriends\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Acheru\MyFriends\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Acheru\MyFriends\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Acheru\MyFriends\client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Acheru\MyFriends\client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Acheru\MyFriends\client\_Imports.razor"
using AcheruFriends.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Acheru\MyFriends\client\_Imports.razor"
using AcheruFriends.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
using AcheruFriends.Models.ConData;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
using AcheruFriends.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Acheru\MyFriends\client\Pages\EditGender.razor"
           [Authorize(Roles="SuperAdmin")]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/edit-gender/{GenderID}")]
    public partial class EditGender : AcheruFriends.Pages.EditGenderComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
