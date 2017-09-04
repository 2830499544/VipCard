using System;

namespace Chain.Model
{
	[Serializable]
	public class MerchantSite
	{
		private int _merchantid;

		private string _merchantdesc;

		private string _merchantphoto;

		private string _merchantremark;

		public int MerchantID
		{
			get
			{
				return this._merchantid;
			}
			set
			{
				this._merchantid = value;
			}
		}

		public string MerchantDesc
		{
			get
			{
				return this._merchantdesc;
			}
			set
			{
				this._merchantdesc = value;
			}
		}

		public string MerchantPhoto
		{
			get
			{
				return this._merchantphoto;
			}
			set
			{
				this._merchantphoto = value;
			}
		}

		public string MerchantRemark
		{
			get
			{
				return this._merchantremark;
			}
			set
			{
				this._merchantremark = value;
			}
		}
	}
}
