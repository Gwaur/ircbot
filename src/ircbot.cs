using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace IrcBot {
	class MainClass {
		public static void Main(string[] args) {
			Bot bot = new Bot();
			if (bot.ReadSettings() == 1) {
				Console.WriteLine("Settings file unfound.");
			} else {
				bot.Connect();
				bot.Listen();
				bot.Disconnect();
			}

			Console.WriteLine("Ended");
			Console.ReadKey();
		}
	}
	class Channel {
		public string ChannelName;
		public string Topic;
		public string Mode;
		public List<string> Names = new List<string>();
	}
	class Bot {
		/* Bot Configuration*/
		public string Server;
		public int Port;
		public string Nick;
		public string AltNick;
		public string UserName;
		public string RealName;
		public List<string> Channels = new List<string>();

		/*Connection stuff*/
		private TcpClient connection;
		private NetworkStream stream;

		public void Connect() {
			connection = new TcpClient(this.Server, this.Port);
		}
		public void Listen() {
			/**/
		}
		public void Disconnect() {
			/**/
		}

		public int ReadSettings() {
			try {
				using (StreamReader settingsStream = new StreamReader("settings")) {
					String line;
					while ((line = settingsStream.ReadLine()) != null) {
						String[] substrings = line.Split('\t');
						switch (substrings[0]) {  //gotta see if there's a better way for doing this, this is ugly to me
							case "server":
								Server = substrings[1];
								break;
							case "port":
								int.TryParse(substrings[1], out Port);
								break;
							case "nick":
								Nick = substrings[1];
								break;
							case "altnick":
								AltNick = substrings[1];
								break;
							case "username":
								UserName = substrings[1];
								break;
							case "realname":
								RealName = substrings[1];
								break;
							case "channels":
								for (int i = 1; i < substrings.Length; i++) {
									Channels.Add(substrings[i]);
								}
								break;
							default:
								break;
						}
					}
				}
			} catch (Exception e) {
				return 1;
			}
			//TODO: Make it check that all critical settings are set
			return 0;
		}
	}
}

