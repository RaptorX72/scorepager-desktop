using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scorepager_desktop.Classes {
    class Score {
        private string composer;
        private string title;
        private string storageFolder;
        private string url;

        public string Composer { get => composer; }
        public string Title { get => title; }
        public string StorageFolder { get => storageFolder; }
        public string Url { get => url; }

        public Score(string composer, string title, string storageFolder, string url) {
            this.composer = composer;
            this.title = title;
            this.storageFolder = storageFolder;
            this.url = url;
        }

        public override string ToString()
        {
            return composer + " | " + title;
        }
    }
}
