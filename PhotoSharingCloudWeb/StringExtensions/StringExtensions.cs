using System;

namespace PhotoSharingCloudWeb.StringExtensions
{
    public static class StringExtensions
    {

        public static string ToShortDate(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }
    }   
}