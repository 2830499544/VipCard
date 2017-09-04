using System;

[Serializable]
public class OnlineMember
{
	private int _memberid;

	private string _sessionid;

	private DateTime _lastnotifytime;

	private string _loginip;

	private bool _isnotextrude;

	private string _useragent;

	public int MemberID
	{
		get
		{
			return this._memberid;
		}
		set
		{
			this._memberid = value;
		}
	}

	public string SessionID
	{
		get
		{
			return this._sessionid;
		}
		set
		{
			this._sessionid = value;
		}
	}

	public DateTime LastNotifyTime
	{
		get
		{
			return this._lastnotifytime;
		}
		set
		{
			this._lastnotifytime = value;
		}
	}

	public string LoginIP
	{
		get
		{
			return this._loginip;
		}
		set
		{
			this._loginip = value;
		}
	}

	public bool IsNotExtrude
	{
		get
		{
			return this._isnotextrude;
		}
		set
		{
			this._isnotextrude = value;
		}
	}

	public string UserAgent
	{
		get
		{
			return this._useragent;
		}
		set
		{
			this._useragent = value;
		}
	}
}
