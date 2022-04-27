using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace scorepager_desktop.Classes {
	public class StorageManager {
		private const string LAYERS_NAME = @"\layers";
		private const string PDF_NAME = @"\score.pdf";
		private const string SAVEFILE_NAME = @"\info.sav";
		private static string appDataPath;
		private static string appPath;
		private static string appPathUsers;

		public static string AppPathUsers { get { return appPathUsers; } }

		public static void Initialize() {
			appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			appPath = $@"{appDataPath}\ScorePager";
			appPathUsers = $@"{appPath}\Users";
			CreateStorage();
		}

		private static void CreateStorage() {
			if (!Directory.Exists(appPath)) Directory.CreateDirectory(appPath);
			if (!Directory.Exists(appPathUsers)) Directory.CreateDirectory(appPathUsers);
		}

		public static List<Score> GetRentedScoresForUser(string uid) {
			List<Score> scores = new List<Score>();
			string path = $@"{appPathUsers}\{uid}";
			if (!Directory.Exists(path)) return new List<Score>();
			foreach (string directory in Directory.GetDirectories(path)) {
				string[] content = Directory.GetFiles(directory);
				string composer = "", title = "", url = "";
				long timestamp = 0;
				int elementsLoaded = 0;
				foreach (string item in content) {
					if (item.EndsWith(SAVEFILE_NAME)) {
						string[] info = File.ReadAllLines(item);
						if (info.Length < 3) continue;
						composer = info[0];
						title = info[1];
						if (!long.TryParse(info[2], out timestamp)) continue;
						elementsLoaded++;
					} else if (item.EndsWith(PDF_NAME)) {
						url = item;
						elementsLoaded++;
					}
				}
				if (elementsLoaded == 2) scores.Add(new Score(composer, title, directory, url, true, timestamp));
			}
			return scores;
		}

		public static Score DownloadScoreForUser(string uid, Score score) {
			string path = $@"{AppPathUsers}\{uid}";
			string scorePath = $@"{path}\{score.CombinedComposerTitle}";
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);
			Directory.CreateDirectory(scorePath);
			Directory.CreateDirectory(scorePath + LAYERS_NAME);
			long rentDate = CommonTools.DateTimeToUnixTimstamp(2592000);
			using (StreamWriter sw = new StreamWriter(scorePath + SAVEFILE_NAME)) {
				sw.WriteLine(score.Composer);
				sw.WriteLine(score.Title);
				sw.WriteLine(rentDate);
			}
			using (System.Net.WebClient download = new System.Net.WebClient()) {
				download.DownloadFile(score.Url, scorePath + PDF_NAME);
			}
			FirebaseClient.GetInstance().DownloadBitmaps(uid, score);
			return new Score(score.Composer, score.Title, scorePath, scorePath + PDF_NAME, true, rentDate);
		}

		public static Bitmap GetLayerForScore(Score score, int pageNumber) {
			string path = score.StorageFolder + LAYERS_NAME;
			string layerPath = $@"{path}\{pageNumber}.bmp";
			if (!File.Exists(layerPath)) return null;
			Bitmap image;
			using (var temp = new Bitmap(layerPath)) {
				image = new Bitmap(temp);
			}
			return image;
		}

		public static void SaveLayerForScore(Score score, int pageNumber, Bitmap image) {
			if (image == null) return;
			string path = score.StorageFolder + LAYERS_NAME;
			string layerPath = $@"{path}\{pageNumber}.bmp";
			if (File.Exists(layerPath)) File.Delete(layerPath);
			image.Save(layerPath);
		}
	} 
}
