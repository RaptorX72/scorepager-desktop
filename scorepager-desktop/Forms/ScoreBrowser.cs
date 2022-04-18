using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using scorepager_desktop.Classes;

namespace scorepager_desktop.Forms {
	public partial class ScoreBrowser : Form {
		private List<Score> scores = new List<Score>();
		private List<Score> scoresDB = new List<Score>();
		private List<Score> scoresLocal = new List<Score>();
		private FirebaseClient client;
		Task<List<Score>> res;

		public ScoreBrowser() {
			InitializeComponent();
			client = FirebaseClient.GetInstance();
			Setup();
		}

		private void Setup() {
			res = client.GetScores();
			AvailableScoresListBox.Enabled = false;
			resultTimer.Enabled = true;
		}

		private void LoadLists() {
			scoresDB = res.Result;
			scoresLocal = StorageManager.GetRentedScoresForUser(client.UserID);
			CombineLists();
			FillListBox();
		}

		private void CombineLists() {
			scores.Clear();
			scoresDB.ForEach(dbscore => {
				bool found = false;
				int i;
				for (i = 0; i < scoresLocal.Count; i++) {
					if (scoresLocal[i].Composer == dbscore.Composer &&
					scoresLocal[i].Title == dbscore.Title && scoresLocal[i].Rented) {
						found = true;
						break;
					}
				}
				if (found) scores.Add(scoresLocal[i]);
				else scores.Add(dbscore);
			});
		}

		private void FillListBox() {
			AvailableScoresListBox.Items.Clear();
			scores.ForEach(score => { AvailableScoresListBox.Items.Add(score.ToString()); });
			AvailableScoresListBox.SelectedIndex = 0;
		}

		private void resultTimer_Tick(object sender, EventArgs e) {
			if (res != null && res.IsCompleted) {
				resultTimer.Enabled = false;
				AvailableScoresListBox.Enabled = true;
				LoadLists();
			}
		}

		private void AvailableScoresListBox_SelectedIndexChanged(object sender, EventArgs e) {
			int index = AvailableScoresListBox.SelectedIndex;
			labelComposer.Text = scores[index].Composer;
			labelTitle.Text = scores[index].Title;
			labelLeased.Text = scores[index].Rented ? "Yes" : "No";
			labelEndDate.Text = scores[index].Rented ? CommonTools.UnixTimestampToDateTime(scores[index].RentDate).ToString("yyyy-MM-dd") : "";
			buttonLoad.Text = scores[index].Rented ? "Edit" : "Lease";
		}

		private void buttonLoad_Click(object sender, EventArgs e) {
			Score currentscore = scores[AvailableScoresListBox.SelectedIndex];
			if (currentscore.Rented) {
				PDFFile file = new PDFFile(currentscore);
				MainForm mf = new MainForm(file);
				this.Hide();
				mf.ShowDialog();
				//
				mf.Dispose();
				this.Show();
			} else {
				scores[AvailableScoresListBox.SelectedIndex] = StorageManager.DownloadScoreForUser(client.UserID, currentscore);
				MessageBox.Show(scores[AvailableScoresListBox.SelectedIndex].ToString());
				FillListBox();
			}
		}
	}
}