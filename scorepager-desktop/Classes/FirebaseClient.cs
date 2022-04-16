﻿using System;
using Google.Cloud.Firestore;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace scorepager_desktop.Classes {
	class FirebaseClient {
		private const string API_KEY = "AIzaSyDq0hFVdYy4r-whzaLX7gR92qTdMPyQZSM";
		private static FirebaseClient client;
		private User user;
		private static bool loggedIn;
		private FirestoreDb db;

		public bool LoggedIn { get { return this.LoggedIn; } }

		public string UserID { get { return loggedIn ? user.LocalId : null; } }

		public FirebaseClient() {
			string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\JSON\firestoredb.json";
			Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
			db = FirestoreDb.Create("scores");
		}

		public static FirebaseClient GetInstance() {
			if (client == null) client = new FirebaseClient();
			return client;
		}

		public bool Login(string email, string pass) {
			try {
				var auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
				var a = auth.SignInWithEmailAndPasswordAsync(email, pass);
				user = a.Result.User;
				loggedIn = true;
			} catch (Exception) {
				return false;
			}
			return true;
		}

		public void Logout() {
			user = null;
			loggedIn = false;
		}

		public async Task<List<Score>> GetScores() {
			QuerySnapshot scoresSnap = await db.Collection("scores").GetSnapshotAsync();
			foreach (DocumentSnapshot docSnap in scoresSnap.Documents)
			{
				Score score = new Score(
					docSnap.GetValue<string>("composer"),
					docSnap.GetValue<string>("title"),
					docSnap.GetValue<string>("uid"),
					docSnap.GetValue<string>("url")
				);
				MessageBox.Show(score.ToString());
			}
			return null;
		}
	}
}
