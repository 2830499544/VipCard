using System;

namespace Chain.Model
{
	[Serializable]
	public class SmsTemplate
	{
		private int _templateid;

		private string _templatename;

		private string _templatecontent;

		private int _templateShopID;

		public int TemplateID
		{
			get
			{
				return this._templateid;
			}
			set
			{
				this._templateid = value;
			}
		}

		public string TemplateName
		{
			get
			{
				return this._templatename;
			}
			set
			{
				this._templatename = value;
			}
		}

		public string TemplateContent
		{
			get
			{
				return this._templatecontent;
			}
			set
			{
				this._templatecontent = value;
			}
		}

		public int TemplateShopID
		{
			get
			{
				return this._templateShopID;
			}
			set
			{
				this._templateShopID = value;
			}
		}
	}
}
