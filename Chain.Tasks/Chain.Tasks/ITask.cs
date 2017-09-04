using System;
using System.Xml;

namespace Chain.Tasks
{
	public interface ITask
	{
		void Execute(XmlNode node);
	}
}
