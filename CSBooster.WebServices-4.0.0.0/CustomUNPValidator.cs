// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Web.Security;
using _4screen.CSB.Extensions.Business;

namespace _4screen.CSB.WebServices
{
    public class CustomUNPValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || !Membership.ValidateUser(userName, password))
            {
                WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, Message = "User validation failed -> Username " + userName, Result = HttpStatusCode.Forbidden.ToString()};
                log.Write();
                throw new SecurityTokenException("Unknown Username or Incorrect Password");
            }
        }
    }
}