namespace Utilities
{
    public static class EMailFormatter
    {
        public static string FrameBodyContent(string firstname, string lastname, string email, string profilePicUrl)
        {
            string strBody = "Thank you <b>" + firstname + " " + lastname + "</b> for your registration.<br><br>" +
        "Below are the details that you have provided us<br><br>" +
        "<b>First name:</b> " + firstname + "<br>" +
        "<b>Last name:</b> " + lastname + "<br>" +
        "<b>Email Address:</b> " + email + "<br>" +
        "<b>Profile Url:</b> " + profilePicUrl + "<br><br><br>" +
        "Best Regards," + "<br>" +
        "Website Team";
            return strBody;
        }
    }
}
