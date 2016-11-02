using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace SaitamaGourmet
{
	public partial class RestaurantTabViewController : UITabBarController
	{
		public AreaMasterMiddles targetAreaItem { get; set; }
		public List<Restaurants> restData { get; set; }
		public string selectedCategory;

		public RestaurantTabViewController(IntPtr handle) : base(handle) {
		}

		public void SetDetailItem(AreaMasterMiddles newTargetAreaItem, string selCategory) {
			if (newTargetAreaItem != null) {
				targetAreaItem = newTargetAreaItem;
				selectedCategory = selCategory;
			}
		}

		public void SetRestData(List<Restaurants> restList)
		{
			if (restList != null)
			{
				restData = restList;
			}
		}
		public override void ViewDidLoad() {
			base.ViewDidLoad();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender) {
			base.PrepareForSegue(segue, sender);
		}
	}
}