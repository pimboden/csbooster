using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
namespace _4screen.CSB.DataAccess.Business
{
    /// <summary>
    /// Controller class for aspnet_Membership
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class AspnetMembershipController
    {
        // Preload our schema..
        AspnetMembership thisSchemaLoad = new AspnetMembership();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public AspnetMembershipCollection FetchAll()
        {
            AspnetMembershipCollection coll = new AspnetMembershipCollection();
            Query qry = new Query(AspnetMembership.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AspnetMembershipCollection FetchByID(object UserId)
        {
            AspnetMembershipCollection coll = new AspnetMembershipCollection().Where("UserId", UserId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public AspnetMembershipCollection FetchByQuery(Query qry)
        {
            AspnetMembershipCollection coll = new AspnetMembershipCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object UserId)
        {
            return (AspnetMembership.Delete(UserId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object UserId)
        {
            return (AspnetMembership.Destroy(UserId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid ApplicationId,Guid UserId,string Password,int PasswordFormat,string PasswordSalt,string MobilePIN,string Email,string LoweredEmail,string PasswordQuestion,string PasswordAnswer,bool IsApproved,bool IsLockedOut,DateTime CreateDate,DateTime LastLoginDate,DateTime LastPasswordChangedDate,DateTime LastLockoutDate,int FailedPasswordAttemptCount,DateTime FailedPasswordAttemptWindowStart,int FailedPasswordAnswerAttemptCount,DateTime FailedPasswordAnswerAttemptWindowStart,string Comment)
	    {
		    AspnetMembership item = new AspnetMembership();
		    
            item.ApplicationId = ApplicationId;
            
            item.UserId = UserId;
            
            item.Password = Password;
            
            item.PasswordFormat = PasswordFormat;
            
            item.PasswordSalt = PasswordSalt;
            
            item.MobilePIN = MobilePIN;
            
            item.Email = Email;
            
            item.LoweredEmail = LoweredEmail;
            
            item.PasswordQuestion = PasswordQuestion;
            
            item.PasswordAnswer = PasswordAnswer;
            
            item.IsApproved = IsApproved;
            
            item.IsLockedOut = IsLockedOut;
            
            item.CreateDate = CreateDate;
            
            item.LastLoginDate = LastLoginDate;
            
            item.LastPasswordChangedDate = LastPasswordChangedDate;
            
            item.LastLockoutDate = LastLockoutDate;
            
            item.FailedPasswordAttemptCount = FailedPasswordAttemptCount;
            
            item.FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart;
            
            item.FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount;
            
            item.FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart;
            
            item.Comment = Comment;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid ApplicationId,Guid UserId,string Password,int PasswordFormat,string PasswordSalt,string MobilePIN,string Email,string LoweredEmail,string PasswordQuestion,string PasswordAnswer,bool IsApproved,bool IsLockedOut,DateTime CreateDate,DateTime LastLoginDate,DateTime LastPasswordChangedDate,DateTime LastLockoutDate,int FailedPasswordAttemptCount,DateTime FailedPasswordAttemptWindowStart,int FailedPasswordAnswerAttemptCount,DateTime FailedPasswordAnswerAttemptWindowStart,string Comment)
	    {
		    AspnetMembership item = new AspnetMembership();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ApplicationId = ApplicationId;
				
			item.UserId = UserId;
				
			item.Password = Password;
				
			item.PasswordFormat = PasswordFormat;
				
			item.PasswordSalt = PasswordSalt;
				
			item.MobilePIN = MobilePIN;
				
			item.Email = Email;
				
			item.LoweredEmail = LoweredEmail;
				
			item.PasswordQuestion = PasswordQuestion;
				
			item.PasswordAnswer = PasswordAnswer;
				
			item.IsApproved = IsApproved;
				
			item.IsLockedOut = IsLockedOut;
				
			item.CreateDate = CreateDate;
				
			item.LastLoginDate = LastLoginDate;
				
			item.LastPasswordChangedDate = LastPasswordChangedDate;
				
			item.LastLockoutDate = LastLockoutDate;
				
			item.FailedPasswordAttemptCount = FailedPasswordAttemptCount;
				
			item.FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart;
				
			item.FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount;
				
			item.FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart;
				
			item.Comment = Comment;
				
	        item.Save(UserName);
	    }
    }
}
