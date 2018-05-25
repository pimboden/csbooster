//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Notification;

namespace _4screen.CSB.Notification.Data
{
   internal class MessageSendEmail : IMessageSender
   {
      public void Send(MessageSend message)
      {
         MailMessage objMail = new MailMessage();
         objMail.IsBodyHtml = true;
         objMail.Subject = message.Subject;
         objMail.Body = message.Body;
         foreach (string strAddress in message.Address)
         {
            objMail.To.Add(strAddress);
         }

         SmtpClient client = new SmtpClient();
         client.Send(objMail);

      }
   }
}
