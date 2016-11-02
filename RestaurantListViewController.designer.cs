// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SaitamaGourmet
{
    [Register ("RestaurantListViewController")]
    partial class RestaurantListViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView RestaurantListTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RestaurantListTableView != null) {
                RestaurantListTableView.Dispose ();
                RestaurantListTableView = null;
            }
        }
    }
}