using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tuya01.Models;

namespace Tuya01.Controllers
{
    public class HomeController : Controller
    {
        string BaseURL = "https://jsonplaceholder.typicode.com/";
        string PrayerURL = "http://api.aladhan.com/v1/";
        public async Task<ActionResult> Index()
        {
            //Calling the web API and populating the in view using DataTable
            string cdate = DateTime.Now.ToString("HH:mm");
            string ctiming = "";
            string ntiming = "";
            TimeSpan[] atiming = new TimeSpan[6];
            TimeSpan Fajr;
            TimeSpan Sunrise;
            TimeSpan Dhuhr;
            TimeSpan Asr;
            TimeSpan Maghrib;
            TimeSpan Isha;
            int timingIndex = 0;
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(PrayerURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string parameters = "timingsByCity?city=Songkhla&country=Thailand&method=8";

                HttpResponseMessage getData = await client.GetAsync(parameters);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var objs = JsonConvert.DeserializeObject<Root>(result);
                    //dt = JsonConvert.DeserializeObject<DataTable>(result);
                    //PropertyInfo[] props = typeof(Timings).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           //.Where(p => properties.ContainsKey(p.Name)).ToArray();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Timings));
                    foreach (PropertyDescriptor p in props)
                        dt.Columns.Add(p.Name, p.PropertyType);

                    var values = new object[props.Count];
                    
                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyInfo propertyInfo = objs.data.timings.GetType().GetProperty(props[i].Name);
                        values[i] = propertyInfo.GetValue(objs.data.timings);
                        //values[i] = props[i].GetValue(objs.data.timings, null);
                    }
                    dt.Rows.Add(values);
                    ViewBag.CDate = objs.data.date.gregorian.date;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(getData);
                }

                parameters = "timingsByCity?city=Songkhla&country=Thailand&method=5";
                getData = await client.GetAsync(parameters);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var objs = JsonConvert.DeserializeObject<Root>(result);
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Timings));
                    var values = new object[props.Count];

                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyInfo propertyInfo = objs.data.timings.GetType().GetProperty(props[i].Name);
                        values[i] = propertyInfo.GetValue(objs.data.timings);
                    }
                    dt.Rows.Add(values);
                    ViewBag.CDate = objs.data.date.gregorian.date;

                    //*************************************************
                    TimeSpan.TryParse(objs.data.timings.Fajr, out Fajr);
                    TimeSpan.TryParse(objs.data.timings.Sunrise, out Sunrise);
                    TimeSpan.TryParse(objs.data.timings.Dhuhr, out Dhuhr);
                    TimeSpan.TryParse(objs.data.timings.Asr, out Asr);
                    TimeSpan.TryParse(objs.data.timings.Maghrib, out Maghrib);
                    TimeSpan.TryParse(objs.data.timings.Isha, out Isha);
                    TimeSpan ctime = DateTime.Now.TimeOfDay;
                    atiming[0] = Fajr;
                    atiming[1] = Sunrise;
                    atiming[2] = Dhuhr;
                    atiming[3] = Asr;
                    atiming[4] = Maghrib;
                    atiming[5] = Isha;
                    if (ctime >= Fajr && ctime < Sunrise)
                    {
                        ctiming = "Fajr";
                        ntiming = "Sunrise";
                        timingIndex = 0;
                    }
                    else if (ctime >= Sunrise && ctime < Dhuhr)
                    {
                        ctiming = "Sunrise";
                        ntiming = "Dhuhr";
                        timingIndex = 1;
                    }
                    else if (ctime >= Dhuhr && ctime < Asr)
                    {
                        ctiming = "Dhuhr";
                        ntiming = "Asr";
                        timingIndex = 2;
                    }
                    else if (ctime >= Asr && ctime < Maghrib)
                    {
                        ctiming = "Asr";
                        ntiming = "Maghrib";
                        timingIndex = 3;
                    }
                    else if (ctime >= Maghrib && ctime < Isha)
                    {
                        ctiming = "Maghrib";
                        ntiming = "Isha";
                        timingIndex = 4;
                    }
                    else
                    {
                        ctiming = "Isha";
                        ntiming = "Fajr";
                        timingIndex = 5;
                    }
                    //System.Diagnostics.Debug.WriteLine("*************************** : Fajr "+Fajr.ToString(@"hh\:mm"));
                    //*************************************************
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(getData);
                }

                parameters = "timingsByCity?city=Songkhla&country=Thailand&method=3";
                getData = await client.GetAsync(parameters);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var objs = JsonConvert.DeserializeObject<Root>(result);
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Timings));
                    var values = new object[props.Count];

                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyInfo propertyInfo = objs.data.timings.GetType().GetProperty(props[i].Name);
                        values[i] = propertyInfo.GetValue(objs.data.timings);
                    }
                    dt.Rows.Add(values);
                    ViewBag.CDate = objs.data.date.gregorian.date;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(getData);
                }

                parameters = "timingsByCity?city=Songkhla&country=Thailand&method=4";
                getData = await client.GetAsync(parameters);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var objs = JsonConvert.DeserializeObject<Root>(result);
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Timings));
                    var values = new object[props.Count];

                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyInfo propertyInfo = objs.data.timings.GetType().GetProperty(props[i].Name);
                        values[i] = propertyInfo.GetValue(objs.data.timings);
                    }
                    dt.Rows.Add(values);
                    ViewBag.CDate = objs.data.date.gregorian.date;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(getData);
                }

                parameters = "timings?latitude=6.78125498224127&longitude=100.43534788534434&method=5";
                getData = await client.GetAsync(parameters);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var objs = JsonConvert.DeserializeObject<Root>(result);
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Timings));
                    var values = new object[props.Count];

                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyInfo propertyInfo = objs.data.timings.GetType().GetProperty(props[i].Name);
                        values[i] = propertyInfo.GetValue(objs.data.timings);
                    }
                    dt.Rows.Add(values);
                    ViewBag.CDate = objs.data.date.gregorian.date + " " + cdate;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(getData);
                }
                ViewData.Model = dt;

            }
            ViewBag.Title = "Muslim Prayer";
            ViewBag.Message = "Timing";
            if (timingIndex < 5)
            {
                ViewBag.Wukto = ctiming+" --> "+ntiming+" ("+atiming[timingIndex+1].ToString(@"hh\:mm")+")";
            }
            else
            {
                ViewBag.Wukto = ctiming + " --> " + ntiming + " (" + atiming[0].ToString(@"hh\:mm") + ")";
            }
            
            TimeSpan abc;
            TimeSpan _24h = new TimeSpan(24, 0, 0);
            TimeSpan atnow = DateTime.Now.TimeOfDay;
            if (timingIndex > 4)
            {
                abc = (atiming[0] + _24h) - atnow;
            }else{
                abc = atiming[timingIndex + 1] - atnow;
            }
            ViewBag.Left = abc.ToString(@"hh\:mm");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}