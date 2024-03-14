namespace BookMovieShow.BAL
{
    public static class CV
    {
        //Provides access to the current
        //Microsoft.AspNetCore.Http.IHttpContextAccessor.HttpContext
        private static IHttpContextAccessor _httpContextAccessor;
        static CV()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        //public static int? UserID()
        //{
        //    //Initialize the UserID with null
        //    int? UserID = null;
        //    //check if session contains specified key?
        //    //if it contains then return the value contained by the key.
        //    if (_httpContextAccessor.HttpContext.Session.GetString("UserID") != null)
        //    {
        //        UserID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID").ToString());
        //    }
        //    return UserID;
        //}

        public static int? UserID()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID"));
        }

        public static string UserName()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserName");
        }

        public static string Email()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("Email");
        }
        //public static string? Username()
        //{
        //    string? Username = null;
        //    if (_httpContextAccessor.HttpContext.Session.GetString("Username") != null)
        //    {
        //        Username = _httpContextAccessor.HttpContext.Session.GetString("Username").ToString();
        //    }
        //    return Username;
        //}
    }
}
