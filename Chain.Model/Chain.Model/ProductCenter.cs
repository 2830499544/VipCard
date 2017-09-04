using System;

namespace Chain.Model
{
	[Serializable]
	public class ProductCenter
	{
		private int _productid;

		private string _productname;

		private string _productphoto;

		private string _productdesc;

		private DateTime? _productcreatetime;

		private string productRemark;

		private int classID;

		private int createUserID;

		public int ClassID
		{
			get
			{
				return this.classID;
			}
			set
			{
				this.classID = value;
			}
		}

		public string ProductRemark
		{
			get
			{
				return this.productRemark;
			}
			set
			{
				this.productRemark = value;
			}
		}

		public int CreateUserID
		{
			get
			{
				return this.createUserID;
			}
			set
			{
				this.createUserID = value;
			}
		}

		public int ProductID
		{
			get
			{
				return this._productid;
			}
			set
			{
				this._productid = value;
			}
		}

		public string ProductName
		{
			get
			{
				return this._productname;
			}
			set
			{
				this._productname = value;
			}
		}

		public string ProductPhoto
		{
			get
			{
				return this._productphoto;
			}
			set
			{
				this._productphoto = value;
			}
		}

		public string ProductDesc
		{
			get
			{
				return this._productdesc;
			}
			set
			{
				this._productdesc = value;
			}
		}

		public DateTime? ProductCreateTime
		{
			get
			{
				return this._productcreatetime;
			}
			set
			{
				this._productcreatetime = value;
			}
		}
	}
}
