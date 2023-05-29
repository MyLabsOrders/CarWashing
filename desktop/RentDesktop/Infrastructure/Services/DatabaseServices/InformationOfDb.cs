﻿using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Collections . Generic;
using System . Linq;
using System . Net;
using System . Net . Http;
using System . Net . Http . Json;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class InformationOfDb {
		public static List<string> Genders ( ) => new ( )
				{
								User.MALE,
								User.FEMALE
						};

		public static DatabaseIdentityInfo Identity ( string userId , ConnectToDb? db = null ) {
		db??=new ConnectToDb ( );

		string getIdentityInfoHandle = $"/api/identity/{userId}";
		using HttpResponseMessage getIdentityInfoResponse = db.Get(getIdentityInfoHandle).Result;

		return getIdentityInfoResponse . Content . ReadFromJsonAsync<DatabaseIdentityInfo> ( ) . Result
				??throw new ContentException ( getIdentityInfoResponse . Content );
			}

		public static List<IUser> Users ( ) {
		var allUsers = new List<IUser>();
		using var db = new ConnectToDb();

		int currentPage = 1;
		IEnumerable<DatabaseUser> currentOrder;

		do {
		string allUsersHandle = $"/api/User?page={currentPage++}";
		using HttpResponseMessage allUsersResponse = db.Get(allUsersHandle).Result;

		if ( !allUsersResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( allUsersResponse );
			}

		DatabaseUsers? usersCollection = allUsersResponse.Content.ReadFromJsonAsync<DatabaseUsers>().Result;

		if ( usersCollection is null||usersCollection . users is null ) {
		throw new ContentException ( allUsersResponse . Content );
			}

		string[] positions = usersCollection.users
										.Select(t => Identity(t.id, db).role)
										.ToArray();

		List<IUser> currentUsers = ModelConverter.ConvertDbUsers(usersCollection, positions);

		foreach ( IUser user in currentUsers ) {
		user . Login=Identity ( user . ID , db ) . username;
		user . Password=User . HIDDEN;
			}

		allUsers . AddRange ( currentUsers );
		currentOrder=usersCollection . users;
			}
		while ( currentOrder . Any ( ) );

		return allUsers;
			}

		public static bool IsAdmin ( string login , ConnectToDb? db = null ) {
		db??=new ConnectToDb ( );

		string adminCheckHandle = "/api/identity/authorize-admin";
		var content = new DatabaseUsername(login);

		using HttpResponseMessage adminCheckResponse = db.Post(adminCheckHandle, content).Result;

		return adminCheckResponse . StatusCode==HttpStatusCode . OK;
			}

		public static List<string> Statuses ( ) => new ( )
				{
								User.ST_ACTIVE,
								User.ST_INACTIVE
						};

		public static List<string> Positions ( ) => new ( )
				{
								User.POS_USER,
								User.POS_ADMIN
						};
		}
	}
