﻿using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiGpioConsoleHost
{
	class Program
	{
		static void Main(string[] args)
		{
			new Program().Run(args);
		}

		private void Run(string[] args)
		{
			var host = ConfigurationManager.AppSettings["GpioServerHost"];
			var port = Convert.ToInt32(ConfigurationManager.AppSettings["GpioServerPort"]);

			// start queue manager
			GpioActionManager.Instance.Start();

			try
			{
				string baseAddress = $"http://{host}:{port}";
				using (WebApp.Start<Startup>(url: baseAddress))
				{
					Console.WriteLine(this.GetType().Namespace);
					Console.WriteLine("Press Enter to quit");
					Console.ReadLine();
				}
			}
			finally
			{
				GpioActionManager.Instance.Stop();
			}


		}
	}
}