using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using BigTed;
using AnimatedButtons;


namespace SaitamaGourmet
{
	public partial class CategoryViewController : UIViewController
	{

		public string selectedCategory; // Default Code 居酒屋
		public List<CategoryLarge> categoryList;

		// カテゴリーボタンを押した時の処理
		protected CategoryViewController(IntPtr handle) : base(handle) {
			// Note: this .ctor should not contain any initialization logic.
		}

		partial void CategoryButtonUpInside(AnimatedShapeButton sender)
		{
			int dataNum = (int)sender.Tag;
			selectedCategory = categoryList[dataNum].category_code;
			categoryList[dataNum].selected = true;

			this.PerformSegue("showArea", this);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			BTProgressHUD.Show();

			GourmetNaviAPI gourmetApi = new GourmetNaviAPI();
			JObject data = gourmetApi.GetApiData(GourmetNaviAPI.CategoryLarge);

			BTProgressHUD.Dismiss();

			categoryList = new List<CategoryLarge>();
			int i = 0;
		    while (true)
			{
				CategoryLarge ct = new CategoryLarge();

				try
				{
					ct.category_code = (string)data["category_l"][i]["category_l_code"];
					ct.category_name = (string)data["category_l"][i]["category_l_name"];
					i++;
					categoryList.Add(ct);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
					break;
				}
			}
		}

		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "showArea")
			{
				((MasterViewController)segue.DestinationViewController).selectedCategory = this.selectedCategory;
			}
		}
	}
}
