﻿using Core.Common.Enums;

namespace Core.Common;

public static class CommonHelper
{
    public static string TrimString(this string? str)
    {
        return string.IsNullOrWhiteSpace(str) ? string.Empty :  str.Trim();
    }


    public static string GetActiveStatus(this Status status, bool isHtml = false)
    {
        // Status.Active ? "<p class=\"text-success\">Active</p>": "<p class=\"text-danger\">Inactive</p>"
        
        if(isHtml == false)
        {
            return status switch
            {
                Status.Active => "Active",
                Status.InActive => "InActive",
                _ => "Unknown"
            };
        }
        else
        {
            return status switch
            {
                Status.Active => "<span class='badge text-success'>Active</span>",
                Status.InActive => "<span class='badge text-danger'>InActive</span>",
                _ => "<span class='badge text-warning'>Deleted</span>"
            };
        }
    }
    
    
    
    public static void Shuffle<T>(this IList<T> list)
    {
        var rng = new Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}