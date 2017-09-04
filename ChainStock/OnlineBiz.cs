using System;
using System.Collections.Generic;

namespace ChainStock
{
	public class OnlineBiz
	{
		public static Dictionary<int, OnlineMember> _onlineMember = new Dictionary<int, OnlineMember>();

		private static object _syncObj = new object();

		private static object _syncObj1 = new object();

		private static DateTime _lastClearTime = DateTime.Now;

		public static void Add(int MemberID, string SessionID, string LoginIP, string userAgent)
		{
			lock (OnlineBiz._syncObj)
			{
				if (OnlineBiz._onlineMember.ContainsKey(MemberID))
				{
					OnlineBiz._onlineMember.Remove(MemberID);
				}
				int Count = 0;
				int Key = 0;
				DateTime dtLastTime = DateTime.MaxValue;
				foreach (KeyValuePair<int, OnlineMember> kvp in OnlineBiz._onlineMember)
				{
					if (kvp.Value.IsNotExtrude)
					{
						Count++;
					}
					if (kvp.Value.LastNotifyTime < dtLastTime)
					{
						dtLastTime = kvp.Value.LastNotifyTime;
						Key = kvp.Key;
					}
				}
				if (PubFunction.curParameter.RestrainOnlineNumber > 0 && OnlineBiz._onlineMember.Count >= PubFunction.curParameter.RestrainOnlineNumber)
				{
					if (OnlineBiz._onlineMember.ContainsKey(Key))
					{
						OnlineBiz._onlineMember[Key].IsNotExtrude = false;
					}
				}
				OnlineMember member = new OnlineMember();
				member.MemberID = MemberID;
				member.SessionID = SessionID;
				member.LoginIP = LoginIP;
				member.LastNotifyTime = DateTime.Now;
				member.IsNotExtrude = true;
				member.UserAgent = userAgent;
				OnlineBiz._onlineMember.Add(MemberID, member);
			}
		}

		public static int IsValid(int MemberID, string SessionID, string userAgent)
		{
			int result;
			lock (OnlineBiz._syncObj1)
			{
				if (!OnlineBiz._onlineMember.ContainsKey(MemberID))
				{
					result = 0;
				}
				else if (!OnlineBiz._onlineMember[MemberID].LoginIP.Equals(SessionID) || !OnlineBiz._onlineMember[MemberID].UserAgent.Equals(userAgent))
				{
					result = 0;
				}
				else if (!OnlineBiz._onlineMember[MemberID].IsNotExtrude)
				{
					OnlineBiz._onlineMember.Remove(MemberID);
					result = -1;
				}
				else
				{
					DateTime dtNow = DateTime.Now;
					OnlineBiz._onlineMember[MemberID].LastNotifyTime = dtNow;
					OnlineBiz.ClearTimeoutMember(dtNow);
					result = 1;
				}
			}
			return result;
		}

		public static void Remove(int MemberID)
		{
			if (OnlineBiz._onlineMember.ContainsKey(MemberID))
			{
				OnlineBiz._onlineMember.Remove(MemberID);
			}
		}

		private static void ClearTimeoutMember(DateTime dtNow)
		{
			int MemberLoginTimeout = 12;
			lock (OnlineBiz._syncObj)
			{
				OnlineBiz._lastClearTime = dtNow;
				foreach (KeyValuePair<int, OnlineMember> kvp in OnlineBiz._onlineMember)
				{
					if ((dtNow - kvp.Value.LastNotifyTime).TotalSeconds > (double)MemberLoginTimeout)
					{
						OnlineBiz._onlineMember.Remove(kvp.Key);
					}
				}
			}
		}
	}
}
