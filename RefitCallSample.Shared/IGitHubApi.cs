using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RefitCallSample.Shared
{
	[Headers("User-Agent: RefitCallSample")]
	interface IGitHubApi
	{
		HttpClient Client { get; }

		[Get("/users/{username}")]
		Task<GitHubUser> GetUserAsync(string userName);

		[Get("/users/{username}")]
		IObservable<GitHubUser> GetUserObservableAsync(string userName);
	}
}

