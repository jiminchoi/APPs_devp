using Google.Maps;
using Foundation;
using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using UIKit;


namespace SaitamaGourmet
{
	public partial class RestaurantMapViewController : UIViewController
	{
		MapView mapView;
		public List<Restaurants> restData { get; set; }

		public RestaurantMapViewController(IntPtr handle) : base(handle) {
		}

		public override void LoadView() {
			base.LoadView();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			restData = ((RestaurantTabViewController)(this.ParentViewController)).restData;

			var camera = CameraPosition.FromCamera(latitude: restData[0].latitude,
												   longitude: restData[0].longitude,
												   zoom: 14);
			mapView = MapView.FromCamera(CGRect.Empty, camera);
			mapView.MyLocationEnabled = true;
			mapView.MapType = MapViewType.Normal;   
			View = mapView;
			mapView.InfoTapped += new EventHandler<GMSMarkerEventEventArgs>(mapViewTapedInfo);

			for (int i=0; i<restData.Count; i++)
			{
				var marker= MakeMarker(i, restData[i].name, restData[i].latitude, restData[i].longitude,restData[i].address, mapView);
			}
		}

		public Marker MakeMarker(int num, string title, double latitude, double longitude,string address, MapView mv)
		{
			Marker marker = new Marker();
			marker.Title = title;
			marker.Snippet = address;
			marker.Position = new CLLocationCoordinate2D(latitude, longitude);
			marker.Map = mv;

			NSNumber pos;
			pos = num;
			marker.UserData = pos;

			return marker;
		}


		void mapViewTapedInfo(object sender , GMSMarkerEventEventArgs e)
		{

			this.PerformSegue("restDetailFromMap", this);
		}
	public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
		if (segue.Identifier == "restDetailFromMap")
		{
				var indexPath = 10;
				var item = restData[indexPath];
			((RestDetailViewController)segue.DestinationViewController).SetDetailItem(item);
			
			}
		}


		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}