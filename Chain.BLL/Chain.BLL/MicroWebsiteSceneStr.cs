using Chain.IDAL;
using Chain.Model;
using System;

namespace Chain.BLL
{
	public class MicroWebsiteSceneStr
	{
		private readonly Chain.IDAL.MicroWebsiteSceneStr dal = new Chain.IDAL.MicroWebsiteSceneStr();

		public int Add(Chain.Model.MicroWebsiteSceneStr model)
		{
			return this.dal.Add(model);
		}

		public bool Delete(string OppenId)
		{
			return this.dal.Delete(OppenId);
		}

		public Chain.Model.MicroWebsiteSceneStr GetModel(string OppenId)
		{
			return this.dal.GetModel(OppenId);
		}
	}
}
