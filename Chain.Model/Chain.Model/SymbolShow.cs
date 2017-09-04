using System;

namespace Chain.Model
{
	[Serializable]
	public class SymbolShow
	{
		private int _symbolid;

		private string _symboltitle;

		private string _symbolphoto;

		private string _symboldesc;

		private DateTime _symboltime;

		public int SymbolID
		{
			get
			{
				return this._symbolid;
			}
			set
			{
				this._symbolid = value;
			}
		}

		public string SymbolTitle
		{
			get
			{
				return this._symboltitle;
			}
			set
			{
				this._symboltitle = value;
			}
		}

		public string SymbolPhoto
		{
			get
			{
				return this._symbolphoto;
			}
			set
			{
				this._symbolphoto = value;
			}
		}

		public string SymbolDesc
		{
			get
			{
				return this._symboldesc;
			}
			set
			{
				this._symboldesc = value;
			}
		}

		public DateTime SymbolTime
		{
			get
			{
				return this._symboltime;
			}
			set
			{
				this._symboltime = value;
			}
		}
	}
}
