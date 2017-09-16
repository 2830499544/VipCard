using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ChainStock.WebReference
{
	[GeneratedCode("System.Web.Services", "4.6.1055.0"), DesignerCategory("code"), DebuggerStepThrough]
	public class RegisterZsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		internal RegisterZsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
