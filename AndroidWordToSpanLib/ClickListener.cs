// Created by Rofiq Setiawan (rofiqsetiawan@gmail.com)

using System;

namespace Bachors.WordToSpan
{
	public class ClickListener : WordToSpan.IClickListener
	{
		private readonly Action<string, string> _action;

		public ClickListener(Action<string, string> action)
		{
			_action = action;
		}

		public void OnClick(string type, string text)
		{
			_action(type, text);
		}
	}
}
