using System;

namespace Chain.Model
{
	[Serializable]
	public class MapAddressInfo
	{
		private int status;

		private string message;

		private MapResultInfo result;

		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		public MapResultInfo Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}
	}
}
