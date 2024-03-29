using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Threading.Tasks;
using System.Threading;

namespace iConvRse
{
	public partial class RootViewController : UITableViewController
	{
		DataSource dataSource;

		public RootViewController () : base ("RootViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");

			// Custom initialization
		}

		public DetailViewController DetailViewController {
			get;
			set;
		}

		void AddNewItem (object sender, EventArgs args)
		{
			AddNewItemCore (DateTime.Now);
		}

		void AddNewItemCore (object data)
		{
			dataSource.Objects.Insert (0, data);
			
			using (var indexPath = NSIndexPath.FromRowSection (0, 0))
				TableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// ---
			// TODO: move this out of here
			var hubConnection = new HubConnection("http://localhost:7777/");
			var chat = hubConnection.CreateHubProxy("chat");
			
			hubConnection.StateChanged += change =>
			{
				Console.WriteLine(change.OldState + " => " + change.NewState);
			};
			
			chat.On<string>("send", msg =>
			{
				AddNewItemCore(msg); 
			});
			
			hubConnection.Start().Wait();

			/*
			chat.Invoke("Send").ContinueWith(task =>
			                                          {
				using (var error = task.Exception.GetError())
				{
					Console.WriteLine(error);
				}
				
			}, TaskContinuationOptions.OnlyOnFaulted);
			*/

			//hubConnection.Stop();
			// ---

			// Perform any additional setup after loading the view, typically from a nib.
			NavigationItem.LeftBarButtonItem = EditButtonItem;

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, AddNewItem);
			NavigationItem.RightBarButtonItem = addButton;

			TableView.Source = dataSource = new DataSource (this);
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("DataSourceCell");
			List<object> objects = new List<object> ();
			RootViewController controller;

			public DataSource (RootViewController controller)
			{
				this.controller = controller;
			}

			public IList<object> Objects {
				get { return objects; }
			}

			// Customize the number of sections in the table view.
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return objects.Count;
			}

			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}

				cell.TextLabel.Text = objects [indexPath.Row].ToString ();

				return cell;
			}

			public override bool CanEditRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}

			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					objects.RemoveAt (indexPath.Row);
					controller.TableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}

			/*
			// Override to support rearranging the table view.
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{
			}
			*/

			/*
			// Override to support conditional rearranging of the table view.
			public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the item to be re-orderable.
				return true;
			}
			*/

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				if (controller.DetailViewController == null)
					controller.DetailViewController = new DetailViewController ();

				controller.DetailViewController.SetDetailItem (objects [indexPath.Row]);

				// Pass the selected object to the new view controller.
				controller.NavigationController.PushViewController (controller.DetailViewController, true);
			}
		}
	}
}
