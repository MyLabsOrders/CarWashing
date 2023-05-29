using System;
using System . Net . Http;
using System . Net . Http . Headers;
using System . Net . Http . Json;
using System . Threading . Tasks;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal class ConnectToDb : IDisposable {
		private bool _isDisposed;
		private readonly HttpClient _httpClient;

		public static string? AuthorizationToken { get; set; }

		public ConnectToDb ( string? authorization = null , bool restoreToken = true ) {
		_httpClient=new HttpClient ( );

		if ( authorization is not null ) {
		SetAuth ( authorization );
			} else if ( restoreToken&&AuthorizationToken is not null ) {
		SetAuth ( AuthorizationToken );
			}
			}

		~ConnectToDb ( ) {
		if ( !_isDisposed ) {
		Dispose ( );
			}
			}

		public void SetAuth ( string token ) => _httpClient . DefaultRequestHeaders . Authorization=new AuthenticationHeaderValue ( SCHEME , token );

		public Task<HttpResponseMessage> Post<T> ( string handle , T contentObject ) => _httpClient . PostAsync ( SERVER_URL+Correct ( handle ) , JsonContent . Create ( contentObject ) );

		public Task<HttpResponseMessage> Put<T> ( string handle , T contentObject ) => _httpClient . PutAsync ( SERVER_URL+Correct ( handle ) , JsonContent . Create ( contentObject ) );

		public void Dispose ( ) {
		Close ( );
		GC . SuppressFinalize ( this );
			}

		public Task<HttpResponseMessage> Patch<T> ( string handle , T contentObject ) => _httpClient . PatchAsync ( SERVER_URL+Correct ( handle ) , JsonContent . Create ( contentObject ) );

		public void Close ( ) {
		_httpClient . Dispose ( );
		_isDisposed=true;
			}

		private static string Correct ( string handle ) => !handle . StartsWith ( "/" )
						? "/"+handle
						: handle;

		public Task<HttpResponseMessage> Get ( string handle ) => _httpClient . GetAsync ( SERVER_URL+Correct ( handle ) );

		public const string HOST = "localhost";
		public const string PORT = "8080";

		public Task<HttpResponseMessage> Delete ( string handle ) => _httpClient . DeleteAsync ( SERVER_URL+Correct ( handle ) );

		public const string PROTOCOL = "http";
		public const string SERVER_URL = $"{PROTOCOL}://{HOST}:{PORT}";
		public const string SCHEME = "Bearer";
		}
	}
