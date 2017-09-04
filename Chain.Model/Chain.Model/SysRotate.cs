using System;

namespace Chain.Model
{
	[Serializable]
	public class SysRotate
	{
		private int rotateID;

		private string rotateName;

		private DateTime startTime;

		private DateTime endTime;

		private string rotateRemark;

		private int rotateCount;

		private int personTotalCount;

		private int personDayCount;

		private string imageUrl;

		private DateTime createTime;

		private int createUserID;

		private string onePrizeName;

		private int onePrizeCount;

		private int onePrizeWinCount;

		private string twoPrizeName;

		private int twoPrizeCount;

		private int twoPrizeWinCount;

		private string threePrizeName;

		private int threePrizeCount;

		private int threePrizeWinCount;

		private string fourPrizeName;

		private int fourPrizeCount;

		private int fourPrizeWinCount;

		private string fivePrizeName;

		private int fivePrizeCount;

		private int fivePrizeWinCount;

		private string sixPrizeName;

		private int sixPrizeCount;

		private int sixPrizeWinCount;

		private decimal oneRate;

		private decimal twoRate;

		private decimal threeRate;

		private decimal fourRate;

		private decimal fiveRate;

		private decimal sixRate;

		private string rotateRegion;

		private string oneMobile;

		private string twoMobile;

		private string threeMobile;

		private string fourMobile;

		private string fiveMobile;

		private string sixMobile;

		private string oneName;

		private string twoName;

		private string threeName;

		private string fourName;

		private string fiveName;

		private string sixName;

		private int isWinOne;

		public int RotateID
		{
			get
			{
				return this.rotateID;
			}
			set
			{
				this.rotateID = value;
			}
		}

		public string RotateName
		{
			get
			{
				return this.rotateName;
			}
			set
			{
				this.rotateName = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		public string RotateRemark
		{
			get
			{
				return this.rotateRemark;
			}
			set
			{
				this.rotateRemark = value;
			}
		}

		public int RotateCount
		{
			get
			{
				return this.rotateCount;
			}
			set
			{
				this.rotateCount = value;
			}
		}

		public int PersonTotalCount
		{
			get
			{
				return this.personTotalCount;
			}
			set
			{
				this.personTotalCount = value;
			}
		}

		public int PersonDayCount
		{
			get
			{
				return this.personDayCount;
			}
			set
			{
				this.personDayCount = value;
			}
		}

		public string ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		public DateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
			set
			{
				this.createTime = value;
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

		public string OnePrizeName
		{
			get
			{
				return this.onePrizeName;
			}
			set
			{
				this.onePrizeName = value;
			}
		}

		public int OnePrizeCount
		{
			get
			{
				return this.onePrizeCount;
			}
			set
			{
				this.onePrizeCount = value;
			}
		}

		public int OnePrizeWinCount
		{
			get
			{
				return this.onePrizeWinCount;
			}
			set
			{
				this.onePrizeWinCount = value;
			}
		}

		public string TwoPrizeName
		{
			get
			{
				return this.twoPrizeName;
			}
			set
			{
				this.twoPrizeName = value;
			}
		}

		public int TwoPrizeCount
		{
			get
			{
				return this.twoPrizeCount;
			}
			set
			{
				this.twoPrizeCount = value;
			}
		}

		public int TwoPrizeWinCount
		{
			get
			{
				return this.twoPrizeWinCount;
			}
			set
			{
				this.twoPrizeWinCount = value;
			}
		}

		public string ThreePrizeName
		{
			get
			{
				return this.threePrizeName;
			}
			set
			{
				this.threePrizeName = value;
			}
		}

		public int ThreePrizeCount
		{
			get
			{
				return this.threePrizeCount;
			}
			set
			{
				this.threePrizeCount = value;
			}
		}

		public int ThreePrizeWinCount
		{
			get
			{
				return this.threePrizeWinCount;
			}
			set
			{
				this.threePrizeWinCount = value;
			}
		}

		public string FourPrizeName
		{
			get
			{
				return this.fourPrizeName;
			}
			set
			{
				this.fourPrizeName = value;
			}
		}

		public int FourPrizeCount
		{
			get
			{
				return this.fourPrizeCount;
			}
			set
			{
				this.fourPrizeCount = value;
			}
		}

		public int FourPrizeWinCount
		{
			get
			{
				return this.fourPrizeWinCount;
			}
			set
			{
				this.fourPrizeWinCount = value;
			}
		}

		public string FivePrizeName
		{
			get
			{
				return this.fivePrizeName;
			}
			set
			{
				this.fivePrizeName = value;
			}
		}

		public int FivePrizeCount
		{
			get
			{
				return this.fivePrizeCount;
			}
			set
			{
				this.fivePrizeCount = value;
			}
		}

		public int FivePrizeWinCount
		{
			get
			{
				return this.fivePrizeWinCount;
			}
			set
			{
				this.fivePrizeWinCount = value;
			}
		}

		public string SixPrizeName
		{
			get
			{
				return this.sixPrizeName;
			}
			set
			{
				this.sixPrizeName = value;
			}
		}

		public int SixPrizeCount
		{
			get
			{
				return this.sixPrizeCount;
			}
			set
			{
				this.sixPrizeCount = value;
			}
		}

		public int SixPrizeWinCount
		{
			get
			{
				return this.sixPrizeWinCount;
			}
			set
			{
				this.sixPrizeWinCount = value;
			}
		}

		public decimal OneRate
		{
			get
			{
				return this.oneRate;
			}
			set
			{
				this.oneRate = value;
			}
		}

		public decimal TwoRate
		{
			get
			{
				return this.twoRate;
			}
			set
			{
				this.twoRate = value;
			}
		}

		public decimal ThreeRate
		{
			get
			{
				return this.threeRate;
			}
			set
			{
				this.threeRate = value;
			}
		}

		public decimal FourRate
		{
			get
			{
				return this.fourRate;
			}
			set
			{
				this.fourRate = value;
			}
		}

		public decimal FiveRate
		{
			get
			{
				return this.fiveRate;
			}
			set
			{
				this.fiveRate = value;
			}
		}

		public decimal SixRate
		{
			get
			{
				return this.sixRate;
			}
			set
			{
				this.sixRate = value;
			}
		}

		public string RotateRegion
		{
			get
			{
				return this.rotateRegion;
			}
			set
			{
				this.rotateRegion = value;
			}
		}

		public string OneMobile
		{
			get
			{
				return this.oneMobile;
			}
			set
			{
				this.oneMobile = value;
			}
		}

		public string TwoMobile
		{
			get
			{
				return this.twoMobile;
			}
			set
			{
				this.twoMobile = value;
			}
		}

		public string ThreeMobile
		{
			get
			{
				return this.threeMobile;
			}
			set
			{
				this.threeMobile = value;
			}
		}

		public string FourMobile
		{
			get
			{
				return this.fourMobile;
			}
			set
			{
				this.fourMobile = value;
			}
		}

		public string FiveMobile
		{
			get
			{
				return this.fiveMobile;
			}
			set
			{
				this.fiveMobile = value;
			}
		}

		public string SixMobile
		{
			get
			{
				return this.sixMobile;
			}
			set
			{
				this.sixMobile = value;
			}
		}

		public string OneName
		{
			get
			{
				return this.oneName;
			}
			set
			{
				this.oneName = value;
			}
		}

		public string TwoName
		{
			get
			{
				return this.twoName;
			}
			set
			{
				this.twoName = value;
			}
		}

		public string ThreeName
		{
			get
			{
				return this.threeName;
			}
			set
			{
				this.threeName = value;
			}
		}

		public string FourName
		{
			get
			{
				return this.fourName;
			}
			set
			{
				this.fourName = value;
			}
		}

		public string FiveName
		{
			get
			{
				return this.fiveName;
			}
			set
			{
				this.fiveName = value;
			}
		}

		public string SixName
		{
			get
			{
				return this.sixName;
			}
			set
			{
				this.sixName = value;
			}
		}

		public int IsWinOne
		{
			get
			{
				return this.isWinOne;
			}
			set
			{
				this.isWinOne = value;
			}
		}
	}
}
