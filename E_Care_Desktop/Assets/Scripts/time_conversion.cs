using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class time_conversion{
    public static DateTime unix_to_date(long unix_time)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(unix_time);
    }

    public static long date_to_unix(DateTime date)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);
    }
}
