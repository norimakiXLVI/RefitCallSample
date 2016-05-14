using RefitCallSample.Shared;
using System;
using Xamarin.Forms;

namespace RefitCallSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void Button_Click(object sender, EventArgs e)
		{
			string userName = "xamarin";
			TimeSpan timeout = TimeSpan.FromMilliseconds(5000);

			// 正常
			await CallApi.GetUser0Async(userName, timeout);
			await CallApi.GetUser1Async(userName, timeout);
			await CallApi.GetUser2Async(userName, timeout);
			await CallApi.GetUser3Async(userName, timeout);

			// 異常
			userName = "xamarin111";
			await CallApi.GetUser0Async(userName, timeout);
			await CallApi.GetUser1Async(userName, timeout);
			await CallApi.GetUser2Async(userName, timeout);
			await CallApi.GetUser3Async(userName, timeout);

			// タイムアウト
			userName = "xamarin";
			timeout = TimeSpan.FromMilliseconds(10);
			await CallApi.GetUser0Async(userName, timeout);
			await CallApi.GetUser1Async(userName, timeout);
			await CallApi.GetUser2Async(userName, timeout);
			await CallApi.GetUser3Async(userName, timeout);
		}
	}
}

