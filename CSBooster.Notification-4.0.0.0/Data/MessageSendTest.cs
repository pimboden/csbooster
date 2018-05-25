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
   internal class MessageSendTest : IMessageSender
   {
      public void Send(MessageSend message)
      {
         string strTestFolder = ConfigSetting.TestModeFolder() + @"\";

         foreach (string addr in message.Address)
         {
            string strFile = string.Format("{0}_{1}_{2}.html", message.CarrierType, addr, DateTime.Now.Ticks);
            FileStream FS = new FileStream(string.Concat(strTestFolder, strFile), FileMode.CreateNew);
            StreamWriter W = new StreamWriter(FS, Encoding.UTF8);
            //W.WriteLine(message.Subject);
            //W.WriteLine();
            W.WriteLine(message.Body);

            W.Flush();
            W.Close();
            FS.Close();
         }
      }
   }
}
