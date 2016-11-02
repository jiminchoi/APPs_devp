using Foundation;
using System;
using UIKit;
using System.Drawing;
using System.CodeDom.Compiler;


namespace SaitamaGourmet
{

	public partial class RestDetailViewController : UIViewController
	{

		public Restaurants targetRestData { get; set; }

		public RestDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public void SetDetailItem(Restaurants newTargetRestaurant)
		{
			if (newTargetRestaurant != null)
			{
				targetRestData = newTargetRestaurant;
			}
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//Labelで指定してAPIを呼び出す。
			restNameLabel.Text = targetRestData.name;
			telLabel.Text = targetRestData.tel;
			lineLabel.Text = targetRestData.line;
			stationLabel.Text = targetRestData.station;
			stationExitLabel.Text = targetRestData.station_exit;
			walkLabel.Text = targetRestData.walk;
			addrLabel.Text = targetRestData.address;
			prShort.Text = targetRestData.pr_short;

			//Imageの表示。
			if (targetRestData.shop_image1 != null)
			{
				restImage.Image = FromUrl(targetRestData.shop_image1);
			}

			//変数として、指定している。下のリストに対して。
			string[] tableItems = new string[] {
				
				"[営業情報]",
				targetRestData.opentime,
				targetRestData.holiday,
				targetRestData.business_hour,

				"[平均予算]",
				targetRestData.budget,

				"[カード]",
				targetRestData.credit_card,

				"[その他]",
				targetRestData.note

			};
			// string[] tableItemsをテーブルに直接代入。
			if (tableItems != null){
				restTotalInfo.Source = new TableSource(tableItems);
			}
		}
		//写真のAPIを呼び出す。
		static UIImage FromUrl(string uri)
		{
			using (var url = new NSUrl(uri))
			using (var data = NSData.FromUrl(url))
				return UIImage.LoadFromData(data);
		}

		//TableのCellに中身を代入してから、繰り返す仕組み。
		public class TableSource : UITableViewSource
		{

			string[] TableItems;
			string CellIdentifier = "TableCell";

			public TableSource(string[] items)
			{
				TableItems = items;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return TableItems.Length;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
				string item = TableItems[indexPath.Row];

				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
				}
				if (item != null)
				{
					cell.TextLabel.Text = item;
				}
				if(item == null)
				{
					cell.TextLabel.Text = "情報無し";
				}

				return cell;
			}
		}
	}
}