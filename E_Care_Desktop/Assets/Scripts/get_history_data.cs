using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UI.Dates;

public class get_history_data : MonoBehaviour {

    [SerializeField] Transform slide_panel;

    [SerializeField] GameObject table_prefab;

    private class Data_row
    {
        public string value;
        public string timestamp;
    }
    private List<Data_row> data_list = new List<Data_row>();

    string data_type = "BR";

    private class Body_data
    {
        public int uid;
        public string tool;
        public string type;
        public string from;
        public string to;
    }

    string url_history = "http://203.151.85.73:3000/api/db/findDataDocuments";
    Dictionary<string, string> headers = new Dictionary<string, string>();

    int max_val = 0;
    int min_val = 999;
    float avg_val = 0;
    public UnityEngine.UI.Text max_val_text;
    public UnityEngine.UI.Text min_val_text;
    public UnityEngine.UI.Text avg_val_text;

    public DatePicker date_picker_from;
    public DatePicker date_picker_to;
    private string date_from;
    private string date_to;

    // Use this for initialization
    void Start () {
        headers.Add("Content-Type", "application/json");
    }
	
    public void press_submit()
    {
        Debug.Log("GET " + data_type + " DATA FROM SERVER");
        press_reset();
        Body_data body = new Body_data();
        body.uid = 1111;
        body.tool = "Hexoskin";
        body.type = data_type;
        body.from = date_from;
        body.to = date_to;
        //Debug.Log("Body: " + JsonUtility.ToJson(body));
        WWW www = new WWW(url_history, System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(body)), headers);
        StartCoroutine(get_history(www));
    }

    public void press_reset()
    {
        data_list.Clear();
        clear_val();
        slide_panel = clear_child_object(slide_panel);
    }

    IEnumerator get_history(WWW www)
    {
        yield return www;
        string txt = "";
        if (string.IsNullOrEmpty(www.error))
            txt = www.text;
        else
            txt = www.error;
        var json_data = JSON.Parse(txt);
        string data_r = "heart_rate";
        if (data_type.Equals("BR"))
        {
            data_r = "respiratory_rate";
        }
        Debug.Log("DATA SIZE : " + json_data[data_type].Count);
        for(int i = 0; i < json_data[data_type].Count; i++)
        {
            Data_row data = new Data_row();
            string value = json_data[data_type][i][data_r]["value"].ToString();
            if(value.Equals("null") == false){
                compare_val(value);
                find_avg(value, json_data[data_type].Count);
                data.value = value;
                data.timestamp = json_data[data_type][i]["effective_time_frame"]["date_time"].ToString();
                data_list.Add(data);
            }
        }
        if (min_val > 200) min_val = 0;
        max_val_text.text = max_val.ToString();
        min_val_text.text = min_val.ToString();
        avg_val_text.text = ((int)avg_val).ToString();
        create_data_list();
    }

    public void choose_type()
    {
        if (data_type.Equals("BR"))
        {
            data_type = "HR";
        }
        else
        {
            data_type = "BR";
        }
        Debug.Log(data_type);
    }

    void create_data_list()
    {
        slide_panel = clear_child_object(slide_panel);
        foreach(Data_row data in data_list)
        {
            GameObject table_row = Instantiate(table_prefab) as GameObject;
            table_row.GetComponentInChildren<Transform>().Find("value").GetComponent<UnityEngine.UI.Text>().text = data.value;
            table_row.GetComponentInChildren<Transform>().Find("timestamp").GetComponent<UnityEngine.UI.Text>().text = data.timestamp;
            table_row.transform.SetParent(slide_panel);
        }
    }

    Transform clear_child_object(Transform parent_object)
    {
        foreach(Transform child_object in parent_object)
        {
            GameObject.Destroy(child_object.gameObject);
        }
        return parent_object;
    }

    void compare_val(string value)
    {
        int tmp = int.Parse(value);
        if (tmp > max_val) max_val = tmp;
        if (tmp < min_val) min_val = tmp;
    }

    void find_avg(string value, int size)
    {
        avg_val += float.Parse(value) / size;
    }

    void clear_val()
    {
        max_val = 0;
        min_val = 999;
        avg_val = 0;
    }

    public void selected_date()
    {
        if (date_picker_to.SelectedDate.Date != null)
        {
            date_from = date_picker_from.SelectedDate.Date.ToString("s") + "Z";
            Debug.Log("DATE FROM: " + date_from);
        }
        if (date_picker_to.SelectedDate.Date != null)
        {
            date_to = date_picker_to.SelectedDate.Date.ToString("s") + "Z";
            Debug.Log("DATE TO: " + date_to);
        }
    }
}
