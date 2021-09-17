using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using AcheruFriends.Models.ConData;
using Microsoft.AspNetCore.Identity;
using AcheruFriends.Models;
using AcheruFriends.Client.Pages;

namespace AcheruFriends.Pages
{
    public partial class EditMyFriendComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }

        [Parameter]
        public dynamic FriendID { get; set; }

        AcheruFriends.Models.ConData.MyFriend _myfriend;
        protected AcheruFriends.Models.ConData.MyFriend myfriend
        {
            get
            {
                return _myfriend;
            }
            set
            {
                if (!object.Equals(_myfriend, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "myfriend", NewValue = value, OldValue = _myfriend };
                    _myfriend = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<AcheruFriends.Models.ConData.Gender> _getGendersResult;
        protected IEnumerable<AcheruFriends.Models.ConData.Gender> getGendersResult
        {
            get
            {
                return _getGendersResult;
            }
            set
            {
                if (!object.Equals(_getGendersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGendersResult", NewValue = value, OldValue = _getGendersResult };
                    _getGendersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var conDataGetMyFriendByFriendIdResult = await ConData.GetMyFriendByFriendId(friendId:FriendID);
            myfriend = conDataGetMyFriendByFriendIdResult;

            var conDataGetGendersResult = await ConData.GetGenders();
            getGendersResult = conDataGetGendersResult.Value.AsODataEnumerable();
        }

        protected async System.Threading.Tasks.Task Form0Submit(AcheruFriends.Models.ConData.MyFriend args)
        {
            try
            {
                var conDataUpdateMyFriendResult = await ConData.UpdateMyFriend(friendId:FriendID, myFriend:myfriend);
                DialogService.Close(myfriend);
            }
            catch (System.Exception conDataUpdateMyFriendException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update MyFriend" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
