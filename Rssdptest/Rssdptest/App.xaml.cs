using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rssdptest
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new Rssdptest.MainPage();
            CommunicationController test = new CommunicationController();
        }

		protected override void OnStart ()
		{
            
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
