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
    public partial class MyFriendsComponent : ComponentBase
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
        protected RadzenDataGrid<AcheruFriends.Models.ConData.MyFriend> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<AcheruFriends.Models.ConData.MyFriend> _getMyFriendsResult;
        protected IEnumerable<AcheruFriends.Models.ConData.MyFriend> getMyFriendsResult
        {
            get
            {
                return _getMyFriendsResult;
            }
            set
            {
                if (!object.Equals(_getMyFriendsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMyFriendsResult", NewValue = value, OldValue = _getMyFriendsResult };
                    _getMyFriendsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getMyFriendsCount;
        protected int getMyFriendsCount
        {
            get
            {
                return _getMyFriendsCount;
            }
            set
            {
                if (!object.Equals(_getMyFriendsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMyFriendsCount", NewValue = value, OldValue = _getMyFriendsCount };
                    _getMyFriendsCount = value;
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
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMyFriend>("Add My Friend", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var conDataGetMyFriendsResult = await ConData.GetMyFriends(filter:$@"(contains(FirstName,""{search}"") or contains(MiddleName,""{search}"") or contains(Surname,""{search}"") or contains(WhatsappNumber,""{search}"") or contains(PhoneNumber2,""{search}"") or contains(Email,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Gender", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getMyFriendsResult = conDataGetMyFriendsResult.Value.AsODataEnumerable();

                getMyFriendsCount = conDataGetMyFriendsResult.Count;
            }
            catch (System.Exception conDataGetMyFriendsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load MyFriends" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(AcheruFriends.Models.ConData.MyFriend args)
        {
            if (@Security.IsInRole(new string[]{"SuperAdmin"})) {
              var dialogResult = await DialogService.OpenAsync<EditMyFriend>("Edit My Friend", new Dictionary<string, object>() { {"FriendID", args.FriendID} });
                await grid0.Reload();

                await InvokeAsync(() => { StateHasChanged(); });
            }
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var conDataDeleteMyFriendResult = await ConData.DeleteMyFriend(friendId:data.FriendID);
                    if (conDataDeleteMyFriendResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception conDataDeleteMyFriendException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete MyFriend" });
            }
        }
    }
}
