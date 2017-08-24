// Created by Bachors on 8/23/2017.
// Ported to Xamarin.Android by Rofiq Setiawan (rofiqsetiawan@gmail.com)

using System;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Bachors.WordToSpan
{
	[Android.Runtime.Register("com.bachors.wordtospan.WordToSpan")]
	public class WordToSpan : WordToSpan.IClickListener
	{
		/// <summary>
		/// The default color.
		/// </summary>
		private static readonly int DefaultColor = Color.Blue;

		private int _colorTag = DefaultColor;
		private int _colorMention = DefaultColor;
		private int _colorUrl = DefaultColor;
		private int _colorMail = DefaultColor;
		private int _colorCustom = DefaultColor;
		private string _regexCustom;
		private bool _underlineTag;
		private bool _underlineMention;
		private bool _underlineUrl;
		private bool _underlineMail;
		private bool _underlineCustom;

		internal IClickListener MyClickListener;

		/// <summary>
		/// Colors.
		/// </summary>
		/// <param name="colorTag"></param>
		public void SetColorTag(int colorTag)
		{
			_colorTag = colorTag;
		}

		public void SetColorMention(int colorMention)
		{
			_colorMention = colorMention;
		}

		public void SetColorUrl(int colorUrl)
		{
			_colorUrl = colorUrl;
		}

		public void SetColorMail(int colorMail)
		{
			_colorMail = colorMail;
		}

		/// <summary>
		/// Custom color.
		/// </summary>
		/// <param name="colorCustom"></param>
		public void SetColorCustom(int colorCustom)
		{
			_colorCustom = colorCustom;
		}

		public void SetRegexCustom(string regexCustom)
		{
			_regexCustom = regexCustom;
		}

		// underline
		public void SetUnderlineTag(bool underlineTag)
		{
			_underlineTag = underlineTag;
		}

		public void SetUnderlineMention(bool underlineMention)
		{
			_underlineMention = underlineMention;
		}

		public void SetUnderlineUrl(bool underlineUrl)
		{
			_underlineUrl = underlineUrl;
		}

		public void SetUnderlineMail(bool underlineMail)
		{
			_underlineMail = underlineMail;
		}

		public void SetUnderlineCustom(bool underlineCustom)
		{
			_underlineCustom = underlineCustom;
		}



		// converter
		public void SetText(string txt, View textView)
		{
			var ws = new SpannableString(txt);

			// Match hashtag eg. #Happy
			foreach (var tag in txt.GetMatchedString(@"(?<=\s|^)#(\w*[A-Za-z_]+\w*)"))
			{
				ws.SetSpan(
					new MyClickableSpan(_colorTag, _underlineTag, "tag", this),
					tag.Key, // Start
					tag.Key + tag.Value, // Length
					SpanTypes.ExclusiveExclusive
				);
			}


			// Match `@` mention eg. @rofiqsetiawan
			foreach (var mention in txt.GetMatchedString(@"(?<=\s|^)@(\w*[A-Za-z_]+\w*)"))
			{
				ws.SetSpan(
					new MyClickableSpan(_colorMention, _underlineMention, "mention", this),
					mention.Key, // Start
					mention.Key + mention.Value, // Length
					SpanTypes.ExclusiveExclusive
				);
			}

			// Match URL Link
			foreach (var url in txt.GetMatchedString(@"(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]"))
			{
				ws.SetSpan(
					new MyClickableSpan(_colorUrl, _underlineUrl, "url", this),
					url.Key, // Start
					url.Key + url.Value, // Length
					SpanTypes.ExclusiveExclusive
				);
			}


			// Match URL Link
			foreach (var email in txt.GetMatchedString(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
			{
				ws.SetSpan(
					new MyClickableSpan(_colorMail, _underlineMail, "mail", this),
					email.Key, // Start
					email.Key + email.Value, // Length
					SpanTypes.ExclusiveExclusive
				);
			}


			// Custom regex
			if (!string.IsNullOrEmpty(_regexCustom))
			{
				foreach (var customRgx in txt.GetMatchedString(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
				{
					ws.SetSpan(
						new MyClickableSpan(_colorCustom, _underlineCustom, "custom", this),
						customRgx.Key, // Start
						customRgx.Key + customRgx.Value, // Length
						SpanTypes.ExclusiveExclusive
					);
				}
			}


			// Set text
			var tv = (TextView)textView;
			tv.TextFormatted = ws;
			tv.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
			tv.SetHighlightColor(Color.Transparent);
		}

		#region Listener

		public class OnClickEventArgs : EventArgs
		{
			public string Type { get; set; }
			public string Text { get; set; }
		}

		/// <summary>
		/// Click listener. Please set before calling <see cref="M:SetText(string, View)"/>
		/// </summary>
		public event EventHandler<OnClickEventArgs> Click;

		public void OnClick(string type, string text)
		{
			Click?.Invoke(this, new OnClickEventArgs()
			{
				Type = type,
				Text = text
			});
		}

		/// <summary>
		/// Click listener.
		/// </summary>
		/// <param name="clickListener"></param>
		public void SetClickListener(IClickListener clickListener)
		{
			MyClickListener = clickListener;
		}


		public interface IClickListener
		{
			void OnClick(string type, string text);
		}

		#endregion
	}
}
