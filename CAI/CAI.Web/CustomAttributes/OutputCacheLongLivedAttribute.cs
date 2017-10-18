namespace CAI.Web.CustomAttributes
{
    using System.Web.Mvc;

    public class OutputCacheLongLivedAttribute : OutputCacheAttribute
    {
        public OutputCacheLongLivedAttribute()
        {
            this.CacheProfile = "LongLived";
        }
    }
}