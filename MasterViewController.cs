using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using BigTed;

using Foundation;
using UIKit;

namespace SaitamaGourmet
{
	public partial class MasterViewController : UITableViewController
	{
		DataSource dataSource;
		public string selectedCategory;
		const int saitamaPrefNumber = 10;
		string targetPrefCode;

		protected MasterViewController(IntPtr handle) : base(handle) {
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

			Title = NSBundle.MainBundle.LocalizedString("エリア選択", "エリア選択");

			// エリアMマスター取得
			dataSource = new DataSource(this);

			BTProgressHUD.Show();

			GourmetNaviAPI gourmetApi = new GourmetNaviAPI();
			JObject data = gourmetApi.GetApiData(GourmetNaviAPI.PrefMaster);
			targetPrefCode = (string)data["pref"][saitamaPrefNumber]["pref_code"];
			data = gourmetApi.GetApiData(GourmetNaviAPI.AreaMasterM);

			bool start = false;
			int i = 0;
			while (true) {
				if ((string)data["garea_middle"][i]["pref"]["pref_code"] == targetPrefCode) {
					AreaMasterMiddles am = new AreaMasterMiddles();

					am.areacode_m = (string)data["garea_middle"][i]["areacode_m"];
					am.areaname_m = (string)data["garea_middle"][i]["areaname_m"];
					am.areacode_l = (string)data["garea_middle"][i]["garea_large"]["areacode_l"];
					am.areaname_l = (string)data["garea_middle"][i]["garea_large"]["areaname_l"];
					am.pref_code = (string)data["garea_middle"][i]["pref"]["pref_code"];
					am.pref_name = (string)data["garea_middle"][i]["pref"]["pref_name"];

					dataSource.Objects.Add(am);
					start = true;
				} else {
					if (start && (string)data["garea_middle"][i]["pref"]["pref_code"] != targetPrefCode) {
						break;
					}
				}
				i++;
			}

			TableView.Source = dataSource;
			BTProgressHUD.Dismiss();
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender) {
			if (segue.Identifier == "showRestTab") {
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = dataSource.Objects[indexPath.Row];

				((RestaurantTabViewController)segue.DestinationViewController).SetDetailItem(item, selectedCategory);
            }
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString("Cell");
			readonly List<AreaMasterMiddles> objects = new List<AreaMasterMiddles>();

			public DataSource(MasterViewController controller) {
			}

			public IList<AreaMasterMiddles> Objects {
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
				var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

				cell.TextLabel.Text = objects[indexPath.Row].areaname_m;

				return cell;
			}

			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
				// Return false if you do not want the specified item to be editable.
				return true;
			}

		}
	}
}
