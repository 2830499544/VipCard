using System;

namespace Chain.Model
{
	[Serializable]
	public class MicroWebsiteSceneStr
	{
		private int _id;

		private string _oppenid;

		private int _scenestr;

		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public string OppenId
		{
			get
			{
				return this._oppenid;
			}
			set
			{
				this._oppenid = value;
			}
		}

		public int SceneStr
		{
			get
			{
				return this._scenestr;
			}
			set
			{
				this._scenestr = value;
			}
		}
	}
}
