﻿using Refit;
using RefitCallSample.Shared;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RefitCallSample.WPF
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
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

		public static async Task GetUser0Async(string userName, TimeSpan timeout)
		{
			try
			{
				var api = RestService.For<IGitHubApi>("https://api.github.com");

				(api as AutoGeneratedIGitHubApi).Client.Timeout = timeout;

				var result = await api.GetUserAsync(userName);

				Debug.WriteLine("Id = {0}, Name = {1}", result.Id, result.Name);
			}
			catch (ApiException ex)
			{
//				Debug.WriteLine("ApiException = {0}", ex.Message);
				Debug.WriteLine("StatusCode = {0}, Content = {1}", ex.StatusCode, ex.Content);
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine("TaskCanceledException : {0}", ex.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public static async Task GetUser1Async(string userName, TimeSpan timeout)
		{
			try
			{
				var api = RestService.For<IGitHubApi>("https://api.github.com");

				var property = api.GetType().GetTypeInfo().GetDeclaredProperty("Client");
				var client = property.GetValue(api, null) as HttpClient;

				client.Timeout = timeout;

				var result = await api.GetUserAsync(userName);

				Debug.WriteLine("Id = {0}, Name = {1}", result.Id, result.Name);
			}
			catch (ApiException ex)
			{
				Debug.WriteLine("StatusCode = {0}, Content = {1}", ex.StatusCode, ex.Content);
				Debug.WriteLine("ApiException : " + ex.Message);
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine("TaskCanceledException : {0}", ex.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public static async Task GetUser2Async(string userName, TimeSpan timeout)
		{
			try
			{
				var api = RestService.For<IGitHubApi>("https://api.github.com");

				api.Client.Timeout = timeout;

				var result = await api.GetUserAsync(userName);

				Debug.WriteLine("Id = {0}, Name = {1}", result.Id, result.Name);
			}
			catch (ApiException ex)
			{
				Debug.WriteLine("StatusCode = {0}, Content = {1}", ex.StatusCode, ex.Content);
//				Debug.WriteLine("ApiException : {0}", ex.Message);
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine("TaskCanceledException : {0}", ex.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public static async Task GetUser3Async(string userName, TimeSpan timeout)
		{
			try
			{
				var api = RestService.For<IGitHubApi>("https://api.github.com");

				var result = await api.GetUserObservableAsync(userName).Timeout(timeout);

				Debug.WriteLine("Id = {0}, Name = {1}", result.Id, result.Name);
			}
			catch (ApiException ex)
			{
				Debug.WriteLine("StatusCode = {0}, Content = {1}", ex.StatusCode, ex.Content);
//				Debug.WriteLine("ApiException : {0}", ex.Message);
			}
			catch (TimeoutException ex)
			{
				Debug.WriteLine("TimeoutException : {0}", ex.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}

