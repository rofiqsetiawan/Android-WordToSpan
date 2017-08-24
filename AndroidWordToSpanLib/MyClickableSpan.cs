// Created by Rofiq Setiawan (rofiqsetiawan@gmail.com)

using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;

#if DEBUG
using Android.Util;
#endif

namespace Bachors.WordToSpan
{
	internal class MyClickableSpan : ClickableSpan
	{
		private readonly int _color;
		private readonly string _type;
		private readonly bool _underline;
		private readonly WordToSpan _wts;

		public MyClickableSpan(int color, bool underline, string type, WordToSpan wts)
		{
			_color = color;
			_underline = underline;
			_type = type;
			_wts = wts;
		}

		public override void OnClick(View widget)
		{
			var tv = (TextView)widget;
			var s = (ISpanned)tv.TextFormatted;
			var start = s.GetSpanStart(this);
			var end = s.GetSpanEnd(this);
#if DEBUG
			if (_wts.MyClickListener == null)
			{
				Log.Debug(nameof(MyClickableSpan), "Listener is null");
			}
#endif
			// Set Listener
			_wts.MyClickListener?.OnClick(
                _type,
                s.SubSequenceFormatted(start, end).ToString().Trim()
            );

			// Event Handler
			_wts.OnClick(
                _type,
                s.SubSequenceFormatted(start, end).ToString().Trim()
            );
		}

		public override void UpdateDrawState(TextPaint ds)
		{
			base.UpdateDrawState(ds);
			ds.Color = _color.ToColor();
			ds.UnderlineText = _underline;
		}

	}
}
