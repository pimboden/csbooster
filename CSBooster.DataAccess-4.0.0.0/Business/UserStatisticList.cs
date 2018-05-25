using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserStatisticList
    {
        public List<UserStatisticUser> Users { get; set; }
        public List<UserStatisticAction> Columns { get; set; }

        public UserStatisticList()
        {
            Users = new List<UserStatisticUser>();
            Columns = new List<UserStatisticAction>();
        }

        public int GetColumn(string action)
        {
            foreach (UserStatisticAction item in Columns)
            {
                if (item.Action == action)
                    return item.Column;
            }

            UserStatisticAction newItem = new UserStatisticAction();
            newItem.Action = action;
            newItem.Column = Columns.Count;
            Columns.Add(newItem);
            return newItem.Column;
        }

        public UserStatisticUser GetUser(string nickname)
        {
            foreach (UserStatisticUser item in Users)
            {
                if (item.Nickname == nickname)
                    return item;
            }
            return null;
        }
    }

    public class UserStatisticUser
    {
        private List<UserStatisticAction> actions = new List<UserStatisticAction>();

        public string Nickname { get; set; }
        public List<UserStatisticAction> Actions
        {
            get { return actions; }
        }

        public UserStatisticAction GetAction(string action)
        {
            foreach (UserStatisticAction item in Actions)
            {
                if (item.Action == action)
                    return item;
            }
            return null;
        }

        public UserStatisticAction GetAction(int column)
        {
            foreach (UserStatisticAction item in Actions)
            {
                if (item.Column == column)
                    return item;
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            return ret.ToString();
        }

    }

    public class UserStatisticAction
    {
        private string action = string.Empty;
        public int Column { get; set; }
        public int ObjectType { get; set; }
        public string ActionName { get; set; }
        public string Action
        {
            get
            { 
                if(string.IsNullOrEmpty(action))
                    return string.Format("{0}{1}", ObjectType, ActionName);
                else
                    return action;
            }

            set
            {
                action = value; 
            }
        }

        public int Amount { get; set; }

        public override string ToString()
        {
            return this.Action;
        }
    }

    public class UserStatisticActivity
    {
        public DateTime Date { get; set; }
        public int ObjectType { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
    }
}
