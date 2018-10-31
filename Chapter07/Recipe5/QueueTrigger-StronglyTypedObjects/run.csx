using System;
        public static void Run(User myQueueItem, TraceWriter log)
        {
           log.Info($"A Message has been created for a new User");
           log.Info($"First name: {myQueueItem.firstname}" );
           log.Info($"Last name: {myQueueItem.lastname}" );
           log.Info($"email: {myQueueItem.email}" );
           log.Info($"Profile Pic Url: {myQueueItem.ProfilePicUrl}" );
        }
        public class User
        {
           public string firstname { get;set;}
           public string lastname { get;set;}
           public string email { get;set;}
           public string ProfilePicUrl { get;set;}
        }