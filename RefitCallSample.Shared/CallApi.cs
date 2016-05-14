﻿using Refit;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RefitCallSample.Shared
{
	public static class CallApi
	{
		public static async Task GetUser0Async(string userName, TimeSpan timeout)
		{
			try
			{
				var api = RestService.For<IGitHubApi>("https://api.github.com");

				(api as AutoGeneratedIGitHubApi).Client.Timeout = timeout;

				var result = await api.GetUserAsync(userName);

				Debug.WriteLine($"Id = {result.Id}, Name = {result.Name}");
			}
			catch (ApiException ex)
			{
				Debug.WriteLine($"StatusCode = {ex.StatusCode}, Content = {ex.Content}");
				Debug.WriteLine($"ApiException : {ex.Message}");
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine($"TaskCanceledException : {ex.Message}");
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

				Debug.WriteLine($"Id = {result.Id}, Name = {result.Name}");
			}
			catch (ApiException ex)
			{
				Debug.WriteLine($"StatusCode = {ex.StatusCode}, Content = {ex.Content}");
				Debug.WriteLine($"ApiException : {ex.Message}");
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine($"TaskCanceledException : {ex.Message}");
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

				Debug.WriteLine($"Id = {result.Id}, Name = {result.Name}");
			}
			catch (ApiException ex)
			{
				Debug.WriteLine($"StatusCode = {ex.StatusCode}, Content = {ex.Content}");
				Debug.WriteLine($"ApiException : {ex.Message}");
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine($"TaskCanceledException : {ex.Message}");
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

				Debug.WriteLine($"Id = {result.Id}, Name = {result.Name}");
			}
			catch (ApiException ex)
			{
				Debug.WriteLine($"StatusCode = {ex.StatusCode}, Content = {ex.Content}");
				Debug.WriteLine($"ApiException : {ex.Message}");
			}
			catch (TimeoutException ex)
			{
				Debug.WriteLine($"TimeoutException : {ex.Message}");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}

