using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clock_date_time : MonoBehaviour {
    // display text
    public UnityEngine.UI.Text clock_hr;
    public UnityEngine.UI.Text clock_min;
    public UnityEngine.UI.Text clock_date;

    // datetime object
    private System.DateTime time_now = new System.DateTime();
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update()
    {
        time_now = System.DateTime.Now;
        //Debug.Log(time_now.ToString("s") + "Z");
        if(time_now.Hour > 9)
            clock_hr.text = time_now.Hour.ToString();
        else
            clock_hr.text = "0" + time_now.Hour.ToString();
        if (time_now.Minute > 9)
            clock_min.text = time_now.Minute.ToString();
        else
            clock_min.text = "0" + time_now.Minute.ToString();

        if (time_now.Day > 9)
            clock_date.text = time_now.Day.ToString() + "/";
        else
            clock_date.text = "0" + time_now.Day.ToString() + "/";

        if (time_now.Month > 9)
            clock_date.text += time_now.Month.ToString() + "/";
        else
            clock_date.text += "0" + time_now.Month.ToString() + "/";

        clock_date.text += time_now.Year.ToString();
	}
}
