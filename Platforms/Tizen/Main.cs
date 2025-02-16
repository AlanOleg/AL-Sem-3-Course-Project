using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace AL_Sem_3_Course_Project_I_Give_Up;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
