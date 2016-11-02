using Foundation;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using BigTed;
using UIKit;
using System.Reflection;

namespace SaitamaGourmet
{
	public partial class RestaurantListViewController : UIViewController
	{
		public AreaMasterMiddles targetAreaItem { get; set; }
		public List<Restaurants> restData { get; set; }
		DataSource dataSource;
		public string selectedCategory;

		public RestaurantListViewController(IntPtr handle) : base(handle) {
		}

		public void SetDetailItem(AreaMasterMiddles newTargetAreaItem) {
			if (targetAreaItem != null) {
				targetAreaItem = newTargetAreaItem;

				ConfigureView();
			}
		}

		void ConfigureView() {
			// Update the user interface for the detail item
			Title = NSBundle.MainBundle.LocalizedString("レストラン一覧", "レストラン一覧");

			restData = new List<Restaurants>();
			dataSource = new DataSource(this);

			if (IsViewLoaded && targetAreaItem != null) {

				// レストラン情報取得

				BTProgressHUD.Show();

				RestaurantSearchParam param = new RestaurantSearchParam();
				param.areacode_m = targetAreaItem.areacode_m;
				param.category_l = selectedCategory;

				GourmetNaviAPI gourmetApi = new GourmetNaviAPI();
				JObject data = gourmetApi.GetRestrantData(GourmetNaviAPI.ResturantSearch, param);

				int i = 0;
				int count = (int)data["total_hit_count"] - 4;
				int perPage = (int)data["hit_per_page"];
				int index = 0;

				while (i < count) {
					if (i < count) {
						Restaurants rest = new Restaurants();

						try {
							if (!IsCheckNull(data["rest"][index]["name"]))
							{
								rest.name = (string)data["rest"][index]["name"];
							}

							if (!IsCheckNull(data["rest"][index]["name_sub"]))
							{
								rest.name_sub = (string)data["rest"][index]["name_sub"];
							}

							if (!IsCheckNull(data["rest"][index]["name_kana"]))
							{
								rest.name_kana = (string)data["rest"][index]["name_kana"];
							}

							if (!IsCheckNull(data["rest"][index]["business_hour"]))
							{
								rest.business_hour = (string)data["rest"][index]["business_hour"];
							}

							if (!IsCheckNull(data["rest"][index]["opentime"]))
							{
								rest.opentime = data["rest"][index]["opentime"].ToString();
							}

							if (!IsCheckNull(data["rest"][index]["holiday"]))
							{
								rest.holiday = (string)data["rest"][index]["holiday"];
							}

							if (!IsCheckNull(data["rest"][index]["address"]))
							{
								rest.address = (string)data["rest"][index]["address"];
							}

							if (!IsCheckNull(data["rest"][index]["tel"]))
							{
								rest.tel = (string)data["rest"][index]["tel"];
							}

							if (!IsCheckNull(data["rest"][index]["fax"]))
							{
								rest.fax = (string)data["rest"][index]["fax"];
							}
                            
							if (!IsCheckNull(data["rest"][index]["pr"]["pr_short"]))
							{
								rest.pr_short = (string)data["rest"][index]["pr"]["pr_short"];
							}

							if (!IsCheckNull(data["rest"][index]["pr"]["pr_long"]))
							{
								rest.pr_long = (string)data["rest"][index]["pr"]["pr_long"];
							}

							if (!IsCheckNull(data["rest"][index]["access"]["line"]))
							{
								rest.line = (string)data["rest"][index]["access"]["line"];
							}

							if (!IsCheckNull(data["rest"][index]["access"]["station"]))
							{
								rest.station = (string)data["rest"][index]["access"]["station"];
							}

							if (!IsCheckNull(data["rest"][index]["access"]["station_exit"]))
							{
								rest.station_exit = (string)data["rest"][index]["access"]["station_exit"];
							}

							if (!IsCheckNull(data["rest"][index]["access"]["walk"]))
							{
								rest.walk = (string)data["rest"][index]["access"]["walk"];
							}

							if (!IsCheckNull(data["rest"][index]["access"]["note"]))
							{
								rest.note = (string)data["rest"][index]["access"]["note"];
							}

							if (!IsCheckNull(data["rest"][index]["budget"]))
							{
								rest.budget = (string)data["rest"][index]["budget"];
							}

							if (!IsCheckNull(data["rest"][index]["image_url"]["shop_image1"]))
							{
								rest.shop_image1 = (string)data["rest"][index]["image_url"]["shop_image1"];
							}

							if (!IsCheckNull(data["rest"][index]["image_url"]["shop_image2"]))
							{
								rest.shop_image2 = (string)data["rest"][index]["image_url"]["shop_image2"];
							}

							if (!IsCheckNull(data["rest"][index]["image_url"]["qrcode"]))
							{
								rest.qrcode = (string)data["rest"][index]["image_url"]["qrcode"];
							}

							if (!IsCheckNull(data["rest"][index]["url_mobile"]))
							{
								rest.url_mobile = (string)data["rest"][index]["url_mobile"];
							}

							if (!IsCheckNull(data["rest"][index]["url"]))
							{
								rest.url = (string)data["rest"][index]["url"];
							}

							if (!IsCheckNull(data["rest"][index]["credit_card"]))
							{
								rest.credit_card = (string)data["rest"][index]["credit_card"];
							}

							if (!IsCheckNull(data["rest"][index]["latitude"]))
							{
								rest.latitude = (double)data["rest"][index]["latitude"];
							}

							if (!IsCheckNull(data["rest"][index]["longitude"]))
							{
								rest.longitude = (double)data["rest"][index]["longitude"];
							}
							//rest.latitude_wgs84 = (double)data["rest"][index]["latitude_wgs84"];
							//rest.longitude_wgs84 = (double)data["rest"][index]["ongitude_wgs84"];

						}
						catch (Exception e) {
							Console.WriteLine(e.ToString());
							break;
						}
						index++;

						restData.Add(rest);
						dataSource.Objects.Add(rest);
					}

					i++;

					if ((i % perPage) == 0) {
						param.offset_page++;
						index = 0;
						try {
							data = gourmetApi.GetRestrantData(GourmetNaviAPI.ResturantSearch, param);
						}
						catch (Exception e) {
							Console.WriteLine(e.ToString());
							break;
						};
					}
				}
				BTProgressHUD.Dismiss();
			}
			RestaurantListTableView.Source = dataSource;
			((RestaurantTabViewController)(this.ParentViewController)).SetRestData(restData);
		}

		private bool IsCheckNull(object o)
		{
			if (o == null || o.ToString() == "{}")
			{
				return true;
			}
			else {
				return false;
			}
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			targetAreaItem = ((RestaurantTabViewController)(this.ParentViewController)).targetAreaItem;
			selectedCategory = ((RestaurantTabViewController)(this.ParentViewController)).selectedCategory;

			ConfigureView();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			//RestaurantListTableView.RowHeight = 50f;
			//RestaurantListTableView.EstimatedRowHeight = 50f;
			//RestaurantListTableView.ReloadData();
		} 

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			((RestaurantTabViewController)(this.ParentViewController)).SetRestData(restData);
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender) {
			if (segue.Identifier == "restDetailFromList") {
				var indexPath = RestaurantListTableView.IndexPathForSelectedRow;
				var item = dataSource.Objects[indexPath.Row];

				((RestDetailViewController)segue.DestinationViewController).SetDetailItem(item);
			}
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString("Cell");
			readonly List<Restaurants> objects = new List<Restaurants>();

			public DataSource(RestaurantListViewController controller) {
			}

			public IList<Restaurants> Objects {
				get { return objects; }
			}

			// Customize the number of sections in the table view.
			public override nint NumberOfSections(UITableView tableView) {
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section) {
				return objects.Count;
			}

			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
				//var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier); 
				var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

				cell.TextLabel.Text = objects[indexPath.Row].name;

				return cell;
			}
		}
	}
}