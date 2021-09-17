using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNet.OData.Query;



namespace AcheruFriends.Controllers.ConData
{
  using Models;
  using Data;
  using Models.ConData;

  [ODataRoutePrefix("odata/ConData/MyFriends")]
  [Route("mvc/odata/ConData/MyFriends")]
  public partial class MyFriendsController : ODataController
  {
    private Data.ConDataContext context;

    public MyFriendsController(Data.ConDataContext context)
    {
      this.context = context;
    }
    // GET /odata/ConData/MyFriends
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.ConData.MyFriend> GetMyFriends()
    {
      var items = this.context.MyFriends.AsQueryable<Models.ConData.MyFriend>();
      this.OnMyFriendsRead(ref items);

      return items;
    }

    partial void OnMyFriendsRead(ref IQueryable<Models.ConData.MyFriend> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{FriendID}")]
    public SingleResult<MyFriend> GetMyFriend(int key)
    {
        var items = this.context.MyFriends.Where(i=>i.FriendID == key);
        return SingleResult.Create(items);
    }
    partial void OnMyFriendDeleted(Models.ConData.MyFriend item);
    partial void OnAfterMyFriendDeleted(Models.ConData.MyFriend item);

    [HttpDelete("{FriendID}")]
    public IActionResult DeleteMyFriend(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.MyFriends
                .Where(i => i.FriendID == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnMyFriendDeleted(item);
            this.context.MyFriends.Remove(item);
            this.context.SaveChanges();
            this.OnAfterMyFriendDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnMyFriendUpdated(Models.ConData.MyFriend item);
    partial void OnAfterMyFriendUpdated(Models.ConData.MyFriend item);

    [HttpPut("{FriendID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutMyFriend(int key, [FromBody]Models.ConData.MyFriend newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.FriendID != key))
            {
                return BadRequest();
            }

            this.OnMyFriendUpdated(newItem);
            this.context.MyFriends.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.MyFriends.Where(i => i.FriendID == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Gender");
            this.OnAfterMyFriendUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{FriendID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchMyFriend(int key, [FromBody]Delta<Models.ConData.MyFriend> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.MyFriends.Where(i => i.FriendID == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnMyFriendUpdated(item);
            this.context.MyFriends.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.MyFriends.Where(i => i.FriendID == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Gender");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnMyFriendCreated(Models.ConData.MyFriend item);
    partial void OnAfterMyFriendCreated(Models.ConData.MyFriend item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.ConData.MyFriend item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnMyFriendCreated(item);
            this.context.MyFriends.Add(item);
            this.context.SaveChanges();

            var key = item.FriendID;

            var itemToReturn = this.context.MyFriends.Where(i => i.FriendID == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Gender");

            this.OnAfterMyFriendCreated(item);

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
