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

		public static void Initialize() {
			appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			appPath = appDataPath + @"\ScorePager";
			appPathUsers = appPath + @"\Users";
			CreateStorage();
		}

		private static void CreateStorage() {
			if (!Directory.Exists(appPath)) Directory.CreateDirectory(appPath);
			if (!Directory.Exists(appPathUsers)) Directory.CreateDirectory(appPathUsers);
		}

		public static List<Score> GetRentedScoresForUser(string uid) {
			List<Score> scores = new List<Score>();
			string path = appPathUsers + @"\" + uid;
			if (!Directory.Exists(path)) return null;
			foreach (string directory in Directory.GetDirectories(path)) {
				string[] content = Directory.GetFiles(directory);
				string composer = "", title = "", url = "";
				int timestamp = 0;
				foreach (string item in content) {
					if (item.EndsWith(SAVEFILE_NAME)) {
						string[] info = File.ReadAllLines(item);
						composer = info[0];
						title = info[1];
						timestamp = Convert.ToInt32(info[2]);
					} else if (item.EndsWith(PDF_NAME)) url = item;
				}
				scores.Add(new Score(composer, title, directory, url, true, timestamp));
			}
			return scores;
		}

		public static Score DownloadScoreForUser(string uid, Score score) {
			string path = appPathUsers + @"\" + uid;
			string scorePath = path + @"\" + score.Composer + score.Title;
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);
			Directory.CreateDirectory(scorePath);
			Directory.CreateDirectory(scorePath + LAYERS_NAME);
			using (StreamWriter sw = new StreamWriter(scorePath + SAVEFILE_NAME)) {
				sw.WriteLine(score.Composer);
				sw.WriteLine(score.Title);
				sw.WriteLine(score.RentDate);
			}
			using (System.Net.WebClient download = new System.Net.WebClient()) {
				download.DownloadFile(score.Url, scorePath + PDF_NAME);
			}
			score.StorageFolder = scorePath;
			score.Url = scorePath + PDF_NAME;
			return score;
		}

		public static Bitmap GetLayerForScore(Score score, int pageNumber) {
			string path = score.StorageFolder + LAYERS_NAME;
			string layerPath = path + @"\" + pageNumber.ToString() + ".bmp";
			if (!File.Exists(layerPath)) return null;
			return (Bitmap)Bitmap.FromFile(layerPath);
		}

		public static void SaveLayerForScore(Score score, int pageNumber, Bitmap image) {
			string path = score.StorageFolder + LAYERS_NAME;
			string layerPath = path + @"\" + pageNumber.ToString() + ".bmp";
			image.Save(layerPath);
		}
	} 
}
