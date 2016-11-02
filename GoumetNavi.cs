using System;
using System.Collections.Generic;
using QuickLook;

namespace SaitamaGourmet
{
	public class PrefMaster
	{
		public string pref_code { get; set; }
		public string pref_name { get; set; }
	}
	public class AreaMasterL
	{
		public string areacode_l { get; set; }
		public string areaname_l { get; set; }
	}
	public class AreaMasterM
	{
		public string areacode_m { get; set; }
		public string areaname_m { get; set; }
	}

	public class AreaMasterS
	{
		public string areacode_s { get; set; }
		public string areaname_s { get; set; }
		public List<AreaMasterM> area_middle { get; set; }
		public List<AreaMasterL> area_large { get; set; }
		public List<PrefMaster> pref { get; set; }

		public AreaMasterS() {
		}
	}

	public class AreaMasterMiddles
	{
		public string areacode_m { get; set; }
		public string areaname_m { get; set; }
		public string areacode_l { get; set; }
		public string areaname_l { get; set; }
		public string pref_code { get; set; }
		public string pref_name { get; set; }

		public AreaMasterMiddles() {
		}
	}

	public class Restaurants
	{
		public Restaurants()
		{

		}

		public string name { get; set; }
		public string name_sub { get; set; }
		public string name_kana { get; set; }
		public string business_hour { get; set; }
		public string opentime { get; set; }
		public string holiday { get; set; }
		public string address { get; set; }
		public string tel { get; set; }
		public string fax { get; set; }

		public string pr_short { get; set; }
		public string pr_long { get; set; }

		public string line { get; set; }
		public string station { get; set; }
		public string station_exit { get; set; }
		public string walk { get; set; }
		public string note { get; set; }

		public string budget { get; set; }
		public string credit_card { get; set; }

		public double latitude { get; set; }
		public double longitude { get; set; }
		public double latitude_wgs84 { get; set; }
		public double longitude_wgs84 { get; set; }
		public string areaname_m { get; set; }
		public string areaname_s { get; set; }

		public string url { get; set; }
		public string url_mobile { get; set; }

		public string shop_image1 { get; set; }
		public string shop_image2 { get; set; }
		public string qrcode { get; set; }
	}

	public class RestaurantSearchParam
	{
		public int hit_per_page = 20;
		public int offset = 1;
		public int offset_page = 1;

		public string category_l;
		public string areacode_m;
	}

	public class CategoryLarge
	{
		public string category_code { get; set; }
		public string category_name { get; set; }
		public bool selected { get; set; } = false;
	}
}
