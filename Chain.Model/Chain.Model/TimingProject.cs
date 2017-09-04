using System;

namespace Chain.Model
{
	public class TimingProject
	{
		private int _projectid;

		private string _projectname;

		private int _projectcategoryid;

		private int _projectrulesid;

		private DateTime _projectaddtime;

		private int _projectshopid;

		private int _projectuserid;

		private string _projectremark;

		public int ProjectID
		{
			get
			{
				return this._projectid;
			}
			set
			{
				this._projectid = value;
			}
		}

		public string ProjectName
		{
			get
			{
				return this._projectname;
			}
			set
			{
				this._projectname = value;
			}
		}

		public int ProjectCategoryID
		{
			get
			{
				return this._projectcategoryid;
			}
			set
			{
				this._projectcategoryid = value;
			}
		}

		public int ProjectRulesID
		{
			get
			{
				return this._projectrulesid;
			}
			set
			{
				this._projectrulesid = value;
			}
		}

		public DateTime ProjectAddTime
		{
			get
			{
				return this._projectaddtime;
			}
			set
			{
				this._projectaddtime = value;
			}
		}

		public int ProjectShopID
		{
			get
			{
				return this._projectshopid;
			}
			set
			{
				this._projectshopid = value;
			}
		}

		public int ProjectUserID
		{
			get
			{
				return this._projectuserid;
			}
			set
			{
				this._projectuserid = value;
			}
		}

		public string ProjectRemark
		{
			get
			{
				return this._projectremark;
			}
			set
			{
				this._projectremark = value;
			}
		}
	}
}
