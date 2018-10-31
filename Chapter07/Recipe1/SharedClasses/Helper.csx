#r "SendGrid"
        using System.Net;
        using SendGrid.Helpers.Mail;
        public static class Helper 
        {
           public static Mail SendMail(string strSubject, string             
            strBody,string strFromAddress,string strToAddress,string              
            strAttachmentName)
           {
              Mail objMessage = new Mail();
              objMessage.Subject = strSubject;
              objMessage.From = new Email(strFromAddress);
 
              objMessage.AddContent(new Content("text/html",strBody));
 
              Personalization personalization = new Personalization();
              personalization.AddTo(new Email(strToAddress));
              objMessage.AddPersonalization(personalization);

              Attachment objAttachment = new Attachment();
              objAttachment.Content = System.Convert.ToBase64String
               (System.Text.Encoding.UTF8.GetBytes(strBody));
              objAttachment.Filename = strAttachmentName;
              objMessage.AddAttachment(objAttachment); 
              return objMessage;
          }
        }