using System;
using Google.Cloud.Firestore;
using Firebase.Auth;
using Google.Cloud.Storage.V1;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace scorepager_desktop.Classes {
	class FirebaseClient {
		private static string API_KEY;
		private string firestoreDBName;
		private string storageBucketName;
		private static FirebaseClient client;
		private User user;
		private static bool loggedIn;
		private FirestoreDb db;
		private StorageClient storage;

		public bool LoggedIn { get { return loggedIn; } }

		public string UserID { get { return loggedIn ? user.LocalId : null; } }

		public FirebaseClient() {
			string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Resources\JSON\";
			using (StreamReader srB = new StreamReader(path + "storage.txt"))
			using (StreamReader sr = new StreamReader(path + "apikey.txt")) {
				API_KEY = sr.ReadLine();
				firestoreDBName = srB.ReadLine();
				storageBucketName = srB.ReadLine();
			}
			Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path + "firestoredb.json");
			db = FirestoreDb.Create(firestoreDBName);
			storage = StorageClient.Create();
			loggedIn = false;
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
			List<Score> scores = new List<Score>();
			QuerySnapshot scoresSnap = await db.Collection("scores").GetSnapshotAsync();
			foreach (DocumentSnapshot docSnap in scoresSnap.Documents) {
				scores.Add(new Score(
					docSnap.GetValue<string>("composer"),
					docSnap.GetValue<string>("title"),
					docSnap.GetValue<string>("uid"),
					docSnap.GetValue<string>("url")
				));
			}
			return scores;
		}

		public void UploadBitmaps(string uid, string score, List<Page> pages) {
			string localPathBase = $@"{StorageManager.AppPathUsers}\{uid}\{score}\layers\";
			string objectPathBase = $"layers/{uid}/{score}/";
			UploadObjectOptions options = new UploadObjectOptions();
			foreach (Page page in pages) {
				string localPath = $"{localPathBase}{page.Number}.bmp";
				if (!File.Exists(localPath)) continue;
				using (var fileStream = File.OpenRead(localPath)) {
					var upload = storage.UploadObject(storageBucketName, $"{objectPathBase}{page.Number}.bmp", null, fileStream);
				}
			}
		}

		public void DownloadBitmaps(string uid, Score score) {
			string localPathBase = $@"{StorageManager.AppPathUsers}\{uid}\{score.Composer}{score.Title}\layers\";
			string objectPath = $"layers/{uid}/{score.Composer}{score.Title}";
			var objects = storage.ListObjects(storageBucketName, objectPath);
			foreach (var item in objects) {
				string localPath = $"{localPathBase}{item.Name.Substring(item.Name.LastIndexOf('/') + 1)}";
				using (var stream = File.Create(localPath)) {
					storage.DownloadObject(storageBucketName, item.Name, stream, null);
				}
			}
		}
	}
}
