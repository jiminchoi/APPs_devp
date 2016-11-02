using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaitamaGourmet
{
	public class GourmetNaviAPI
	{
		public const int ForeignRestaurant = 1;
		public const int ResturantSearch = 2;
		public const int PrefMaster = 3;
		public const int AreaMasterL = 4;
		public const int AreaMasterM = 5;
		public const int AreaMasterS = 6;
		public const int CategoryLarge = 7;
		public const int CategorySmall = 8;

		const string gourmetNaviUrlForeignResturantSearch = "http://api.gnavi.co.jp/ForeignRestSearchAPI/20150630/"; // 外国語版レストラン検索
		const string gourmetNaviUrlResturantSearch = "http://api.gnavi.co.jp/RestSearchAPI/20150630/"; // レストラン検索
		const string gourmetNaviUrlPrefMaster = "http://api.gnavi.co.jp/master/PrefSearchAPI/20150630/"; // 都道府県マスタ
		const string gourmetNaviUrlAreaMasterL = "http://api.gnavi.co.jp/master/GAreaLargeSearchAPI/20150630/"; // エリアLマスタ
		const string gourmetNaviUrlAreaMasterM = "http://api.gnavi.co.jp/master/GAreaMiddleSearchAPI/20150630/"; // エリアMマスタ
		const string gourmetNaviUrlAreaMasterS = "http://api.gnavi.co.jp/master/GAreaSmallSearchAPI/20150630/"; // エリアSマスタ
		const string gourmetNaviUrlCategoryLarge = "http://api.gnavi.co.jp/master/CategoryLargeSearchAPI/20150630/"; // 大カテゴリ
		const string gourmetNaviUrlCategorySmall = "http://api.gnavi.co.jp/master/CategorySmallSearchAPI/20150630/"; // 小カテゴリ

		const string gourmetNaviKey = "4b459c82d6c82a40f653dfd1c031f70c";
		readonly string _accessKey;

		public GourmetNaviAPI() {
			_accessKey = gourmetNaviKey;
		}

		public JObject GetApiData(int mode) {

			string Url = string.Empty;

			WebClient client = new WebClient();
			switch (mode) {
				case 1:
					Url = gourmetNaviUrlForeignResturantSearch;
					break;
				case 2:
					Url = gourmetNaviUrlResturantSearch;
					break;
				case 3:
					Url = gourmetNaviUrlPrefMaster;
					break;
				case 4:
					Url = gourmetNaviUrlAreaMasterL;
					break;
				case 5:
					Url = gourmetNaviUrlAreaMasterM;
					break;
				case 6:
					Url = gourmetNaviUrlAreaMasterS;
					break;
				case 7:
					Url = gourmetNaviUrlCategoryLarge;
					break;
				case 8:
					Url = gourmetNaviUrlCategorySmall;
					break;
			}
			Url += "?keyid=" + _accessKey + "&format=json";
			Stream stream = client.OpenRead(Url);
			StreamReader reader = new StreamReader(stream);
			Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
			stream.Close();

			return jObject;
		}

		public JObject GetRestrantData(int mode, RestaurantSearchParam param) {

			string Url = string.Empty;

			WebClient client = new WebClient();
			switch (mode) {
				case 1:
					Url = gourmetNaviUrlForeignResturantSearch;
					break;
				case 2:
					Url = gourmetNaviUrlResturantSearch;
					break;
			}
			Url += "?keyid=" + _accessKey + "&format=json" + "&areacode_m=" + param.areacode_m + "&category_l=" + param.category_l + "&hit_per_page=" + param.hit_per_page.ToString()
																				   + "&offset=" + param.offset.ToString() + "&offset_page=" + param.offset_page.ToString();
			Stream stream = client.OpenRead(Url);
			StreamReader reader = new StreamReader(stream);
			Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
			stream.Close();

			return jObject;
		}
	}
}
