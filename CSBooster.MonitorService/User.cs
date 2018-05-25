//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace _4screen.CSB.MonitorService
{
  public class User
  {
    private string username;
    private string email;
    private string fullname;
    private DateTime lastLoginDate;
    private DateTime lastLockoutDate;
    private bool isLocked;

    public User()
    {
    }

    public User(string username, string email, string fullname, DateTime lastLoginDate, DateTime lastLockoutDate, bool isLocked)
    {
      this.username = username;
      this.email = email;
      this.fullname = fullname;
      this.lastLoginDate = lastLoginDate;
      this.lastLockoutDate = lastLockoutDate;
      this.isLocked = isLocked;
    }

    public string Username
    {
      get { return this.username; }
      set { this.username = value; }
    }

    public string Email
    {
      get { return this.email; }
      set { this.email = value; }
    }

    public string Fullname
    {
      get { return this.fullname; }
      set { this.fullname = value; }
    }
    public DateTime LastLoginDate
    {
      get { return this.lastLoginDate; }
      set { this.lastLoginDate = value; }
    }

    public DateTime LastLockoutDate
    {
      get { return this.lastLockoutDate; }
      set { this.lastLockoutDate = value; }
    }

    public bool IsLocked
    {
      get { return this.isLocked; }
      set { this.isLocked = value; }
    }
  }
}
