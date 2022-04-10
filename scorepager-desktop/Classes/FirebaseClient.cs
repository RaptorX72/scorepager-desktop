using System;
using Google.Cloud.Firestore;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scorepager_desktop.Classes {
	class FirebaseClient {
		private const string API_KEY = "AIzaSyAqCPbrsMc437mOHXRZF0T2hpcP9XMlQ0c";
		private static FirebaseClient client;
		private static User user;
		private static bool loggedIn;

		public bool LoggedIn { get { return this.LoggedIn; } }

		public string UserID { get { return loggedIn ? user.LocalId : null; } }

		public FirebaseClient() {
			string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\JSON\firestoredb.json";
			Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
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
	}
}
