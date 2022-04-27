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
		private Task<List<Score>> res;
		private int countDown = 30;

		public ScoreBrowser() {
			InitializeComponent();
			client = FirebaseClient.GetInstance();
			Setup();
		}

		private void Setup() {
			res = client.GetScores();
			resultTimer.Enabled = true;
			buttonLoad.Enabled = false;
		}

		private void LoadLists() {
			scoresDB = res.Result;
			scoresLocal = StorageManager.GetRentedScoresForUser(client.UserID);
			CombineLists();
			FillListBox();
		}

		private void CombineLists() {
			scores.Clear();
			if (scoresDB == null || !res.IsCompleted) {
				scoresLocal.ForEach(item => { scores.Add(item); });
				return;
			}
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
				scores.Add(found ? scoresLocal[i] : dbscore);
			});
		}

		private void FillListBox() {
			AvailableScoresListBox.Items.Clear();
			if (scores.Count == 0) {
				AvailableScoresListBox.Items.Add("Failed to load any scores!");
				buttonLoad.Enabled = false;
				return;
			}
			scores.ForEach(score => { AvailableScoresListBox.Items.Add(score.ToString()); });
			AvailableScoresListBox.SelectedIndex = 0;
			buttonLoad.Enabled = true;
		}

		private void buttonLoad_Click(object sender, EventArgs e) {
			Score currentscore = scores[AvailableScoresListBox.SelectedIndex];
			if (currentscore.Rented) {
				PDFFile file = new PDFFile(currentscore);
				//TODO: Handle file exception
				using (MainForm mf = new MainForm(file)) {
					Hide();
					mf.ShowDialog();
					if (client.LoggedIn) Show();
					else Close();
				}
			} else {
				//TODO: Handle score exception
				scores[AvailableScoresListBox.SelectedIndex] = StorageManager.DownloadScoreForUser(client.UserID, currentscore);
				FillListBox();
			}
		}

		private void buttonLogout_Click(object sender, EventArgs e) {
			client.Logout();
			Close();
		}

		private void AvailableScoresListBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (scores.Count == 0) return;
			int index = AvailableScoresListBox.SelectedIndex;
			labelComposer.Text = scores[index].Composer;
			labelTitle.Text = scores[index].Title;
			labelLeased.Text = scores[index].Rented ? "Yes" : "No";
			labelEndDate.Text = scores[index].Rented ? CommonTools.UnixTimestampToDateTime(scores[index].RentDate).ToString("yyyy-MM-dd") : "";
			buttonLoad.Text = scores[index].Rented ? "Edit" : "Lease";
		}

		private void resultTimer_Tick(object sender, EventArgs e) {
			if (--countDown == 0) {
				MessageBox.Show("Failed to load scores from database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				resultTimer.Enabled = false;
				LoadLists();
				return;
			}
			if (res != null && res.IsCompleted) {
				resultTimer.Enabled = false;
				LoadLists();
			}
		}

		private void ScoreBrowser_FormClosing(object sender, FormClosingEventArgs e) {
			if (client.LoggedIn) Environment.Exit(0);
		}
	}
}