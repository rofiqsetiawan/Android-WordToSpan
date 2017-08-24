// Author: Rofiq Setiawan rofiqsetiawan@gmail.com

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Bachors.WordToSpan;
using R = AndroidWordToSpanDemo.Resource;

#if DEBUG
using Android.Util;
#endif

namespace AndroidWordToSpanDemo
{
	[Activity(Label = "AndroidWordToSpanDemo", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@android:style/Theme.Light")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(R.Layout.Main);

			// Convert string to spannable & set text
			const string myText = "I know http://just.com/anu how to @whisper, And I #know just #how to cry,I know just @where to anu@find.com the answers";
			var tv = FindViewById<TextView>(R.Id.txt);

			var wts = new WordToSpan();

			// Set color link. Default = Color.Blue
			wts.SetColorTag(Color.Green);
			wts.SetColorUrl(Color.Magenta);
			wts.SetColorMail(Color.Pink);
			wts.SetColorMention(Color.Brown);
			//			wts.SetClickListener(new ClickListener((type, text) =>
			//			{
			//#if DEBUG
			//				Log.Debug(nameof(MainActivity), "Triggered by Set Listener");
			//#endif
			//				Toast.MakeText(this, $"Type: {type} | Text: {text}", ToastLength.Short).Show();
			//			}));
			wts.Click += (s, e) =>
			{
#if DEBUG
				Log.Debug(nameof(MainActivity), "Triggered by Event Handler");
#endif
				Toast.MakeText(this, $"Type: {e.Type} | Text: {e.Text}", ToastLength.Short).Show();
			};
			wts.SetText(myText, tv);


		}
	}
}

