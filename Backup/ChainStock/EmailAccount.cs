using System;

public class EmailAccount
{
	private int emailAccountId;

	private string email;

	private string displayName;

	private string host;

	private int port;

	private string username;

	private string pPassword;

	private bool enableSSL;

	private bool useDefaultCredentials;

	public int EmailAccountId
	{
		get
		{
			return this.emailAccountId;
		}
		set
		{
			this.emailAccountId = value;
		}
	}

	public string Email
	{
		get
		{
			return this.email;
		}
		set
		{
			this.email = value;
		}
	}

	public string DisplayName
	{
		get
		{
			return this.displayName;
		}
		set
		{
			this.displayName = value;
		}
	}

	public string Host
	{
		get
		{
			return this.host;
		}
		set
		{
			this.host = value;
		}
	}

	public int Port
	{
		get
		{
			return this.port;
		}
		set
		{
			this.port = value;
		}
	}

	public string Username
	{
		get
		{
			return this.username;
		}
		set
		{
			this.username = value;
		}
	}

	public string Password
	{
		get
		{
			return this.pPassword;
		}
		set
		{
			this.pPassword = value;
		}
	}

	public bool EnableSSL
	{
		get
		{
			return this.enableSSL;
		}
		set
		{
			this.enableSSL = value;
		}
	}

	public bool UseDefaultCredentials
	{
		get
		{
			return this.useDefaultCredentials;
		}
		set
		{
			this.useDefaultCredentials = value;
		}
	}

	public string FriendlyName
	{
		get
		{
			string result;
			if (!string.IsNullOrEmpty(this.displayName))
			{
				result = this.email + " (" + this.displayName + ")";
			}
			else
			{
				result = this.email;
			}
			return result;
		}
	}
}
