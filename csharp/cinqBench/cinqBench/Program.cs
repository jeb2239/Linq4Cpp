﻿using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.IO;


namespace cinqBench
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			

			List<WeatherPoint> weatherData = loadWeather ();





		}

		public static string fix(string s){

			if (s == "" || s == "T") return "0";
			else return s;

		}
			

		public static List<WeatherPoint> loadWeather(){


			string[] lines = System.IO.File.ReadAllLines (Path.Combine ("..","..","..", "..", "..", "data", "weather_kjfk_1948-2014.csv"));
			string[] headers = lines [0].Split (',');
			Console.WriteLine (headers.Length);
			List<WeatherPoint> weatherData = new List<WeatherPoint> ();
List<Dictionary<string,string>> dictList = new List<Dictionary<string,string>> ();
			for (int i = 1; i < lines.Length; i++) {
				string[] tok = lines [i].Split (',');
				if (tok.Length < 23)
					continue;

				Dictionary<string,string> dict = new Dictionary<string,string> (); 

				int count = 0;
				foreach (string header in headers) {
					try{
						
						dict [header] = fix(tok [count]);
						count++;
					}
					catch(Exception expt){
						//Console.WriteLine (headers.Length);
						foreach(string a in tok){
							Console.WriteLine(a);
						}
					}
					finally{}
				}
				dictList.Add (dict);
			}

			foreach (var line in dictList) {
				
				var p = new WeatherPoint ();
			//	DateTime.ParseExact(MyString, "yyyy-M-dd",null);
				try{
				p.date = DateTime.ParseExact(fix(line ["EST"]),"yyyy-M-d",null);
				p.temp_max = Int32.Parse (fix( line["Max TemperatureF"]));
				p.temp_avg = Int32.Parse(fix(line ["Mean TemperatureF"]));
				p.temp_min = Int32.Parse (fix(line ["Min TemperatureF"]));
				p.dew_max = Int32.Parse (fix(line ["Max Dew PointF"]));

				// These next 3 lines look wrong, but that's actually how Wunderground names their column headers.
				//p.dew_max = stoi(fix(line["Max Dew PointF"]));

				p.dew_avg = Convert.ToInt32 (fix (line ["MeanDew PointF"]));
				p.dew_min = Convert.ToInt32 (fix (line ["Min DewpointF"]));

				p.humidity_max = Convert.ToInt32 (fix (line ["Max Humidity"]));
				p.humidity_avg = Convert.ToInt32 (fix (line ["Mean Humidity"]));
				p.humidity_min = Convert.ToInt32 (fix (line ["Min Humidity"]));

				p.pressure_max = Convert.ToDouble (fix (line ["Max Sea Level PressureIn"]));
				p.pressure_avg = Convert.ToDouble (fix (line ["Mean Sea Level PressureIn"]));
				p.pressure_min = Convert.ToDouble (fix (line ["Min Sea Level PressureIn"]));

				p.visibility_max = Convert.ToInt32 (fix (line ["Max VisibilityMiles"]));
				p.visibility_avg = Convert.ToInt32 (fix (line ["Mean VisibilityMiles"]));
				p.visibility_min = Convert.ToInt32 (fix (line ["Min VisibilityMiles"]));

				p.windspeed_max = Convert.ToInt32 (fix (line ["Max Wind SpeedMPH"]));
				p.windspeed_avg = Convert.ToInt32 (fix (line ["Mean Wind SpeedMPH"]));

				p.gustspeed_max = Convert.ToInt32 (fix (line ["Max Gust SpeedMPH"]));

				p.precipitation = Convert.ToDouble (fix (line ["PrecipitationIn"]));

				p.cloud_cover = Convert.ToInt32 (fix (line ["CloudCover"]));
				p.fog = false;
				p.rain = false;
				p.thunderstorm = false;
				p.snow = false;

				string[] events = line ["Events"].Split ('-');
				foreach (string eventr  in events) {
					if (eventr == "Fog")
						p.fog = true;
					if (eventr == "Rain")
						p.rain = true;
					if (eventr == "Thunderstorm")
						p.thunderstorm = true;  
					if (eventr == "Snow")
						p.snow = true;  
				}

				p.wind_direction = Convert.ToInt32 (fix (line ["WindDirDegrees"]));
				
				weatherData.Add (p);
				}
				catch(Exception e){
					foreach (string header in headers) {
						Console.WriteLine (header);
					}
				}
				
			}
			return weatherData;
		}





						






		}






	}

