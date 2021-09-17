using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcheruFriends.Models.ConData
{
  [Table("MyFriends", Schema = "dbo")]
  public partial class MyFriend
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FriendID
    {
      get;
      set;
    }
    public string FirstName
    {
      get;
      set;
    }
    public string MiddleName
    {
      get;
      set;
    }
    public string Surname
    {
      get;
      set;
    }
    public int GenderID
    {
      get;
      set;
    }
    public Gender Gender { get; set; }
    public string WhatsappNumber
    {
      get;
      set;
    }
    public string PhoneNumber2
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
  }
}
