using Chain.BLL;
using Chain.Model;
using System;
using System.Data;

public class MEMPointUpdate
{
	private static decimal[] MEMRate = new decimal[15];

	private static int RateLevel;

	private static int NowRateLevel;

	private static bool RateType;

	private static string MEMName;

	private static string MEMCard;

	private static string PointOrderCode;

	private static int sumPoint = 0;

	private static Chain.BLL.Mem bll = new Chain.BLL.Mem();

	public static void MEMPointRate(Chain.Model.Mem mem, int point, string OrderAccount, int pointType, int UserID, int UserShopID)
	{
		if (PubFunction.curParameter.chkPointLevel)
		{
			Chain.Model.PointRate PointRateModel = new Chain.BLL.PointRate().GetPointRate();
			if (PointRateModel != null)
			{
				MEMPointUpdate.RateLevel = PointRateModel.PointRateLevel;
				MEMPointUpdate.RateType = PointRateModel.PointRateType;
				MEMPointUpdate.NowRateLevel = 1;
				MEMPointUpdate.MEMRate[0] = PointRateModel.MemLevel1;
				MEMPointUpdate.MEMRate[1] = PointRateModel.MemLevel2;
				MEMPointUpdate.MEMRate[2] = PointRateModel.MemLevel3;
				MEMPointUpdate.MEMRate[3] = PointRateModel.MemLevel4;
				MEMPointUpdate.MEMRate[4] = PointRateModel.MemLevel5;
				MEMPointUpdate.MEMRate[5] = PointRateModel.MemLevel6;
				MEMPointUpdate.MEMRate[6] = PointRateModel.MemLevel7;
				MEMPointUpdate.MEMRate[7] = PointRateModel.MemLevel8;
				MEMPointUpdate.MEMRate[8] = PointRateModel.MemLevel9;
				MEMPointUpdate.MEMRate[9] = PointRateModel.MemLevel10;
				MEMPointUpdate.MEMRate[10] = PointRateModel.MemLevel11;
				MEMPointUpdate.MEMRate[11] = PointRateModel.MemLevel12;
				MEMPointUpdate.MEMRate[12] = PointRateModel.MemLevel13;
				MEMPointUpdate.MEMRate[13] = PointRateModel.MemLevel14;
				MEMPointUpdate.MEMRate[14] = PointRateModel.MemLevel15;
				MEMPointUpdate.MEMCard = mem.MemCard;
				MEMPointUpdate.MEMName = mem.MemName;
				MEMPointUpdate.PointOrderCode = OrderAccount;
				Chain.Model.SysUser User = new Chain.BLL.SysUser().GetModel(UserID);
				if (User != null)
				{
					MEMPointUpdate.PointRateUpdate(mem, point, pointType, mem.MemID, User, OrderAccount);
					string remark = "";
					switch (pointType)
					{
					case -1:
						remark = string.Concat(new object[]
						{
							"单号：[",
							OrderAccount,
							"],会员商品消费退货,多级推荐扣除运营商积分：[",
							MEMPointUpdate.sumPoint,
							"]"
						});
						break;
					case 0:
					case 4:
						break;
					case 1:
						remark = string.Concat(new object[]
						{
							"单号：[",
							OrderAccount,
							"],会员商品消费,多级推荐扣除运营商积分：[",
							MEMPointUpdate.sumPoint,
							"]"
						});
						break;
					case 2:
						remark = string.Concat(new object[]
						{
							"单号：[",
							OrderAccount,
							"],会员快速消费,多级推荐扣除运营商积分：[",
							MEMPointUpdate.sumPoint,
							"]"
						});
						break;
					case 3:
						remark = string.Concat(new object[]
						{
							"单号：[",
							OrderAccount,
							"],会员充次,多级推荐扣除运营商积分：[",
							MEMPointUpdate.sumPoint,
							"]"
						});
						break;
					case 5:
						remark = string.Concat(new object[]
						{
							"单号：[",
							OrderAccount,
							"],会员注册,多级推荐扣除运营商积分：[",
							MEMPointUpdate.sumPoint,
							"]"
						});
						break;
					case 6:
						remark = string.Concat(new object[]
						{
							"单号：[",
							OrderAccount,
							"],会员手动增加积分,多级推荐扣除运营商积分：[",
							MEMPointUpdate.sumPoint,
							"]"
						});
						break;
					default:
						switch (pointType)
						{
						case 14:
							remark = string.Concat(new object[]
							{
								"单号：[",
								OrderAccount,
								"],会员充时,多级推荐扣除运营商积分：[",
								MEMPointUpdate.sumPoint,
								"]"
							});
							break;
						case 15:
							remark = string.Concat(new object[]
							{
								"单号：[",
								OrderAccount,
								"],会员充值,多级推荐扣除运营商积分：[",
								MEMPointUpdate.sumPoint,
								"]"
							});
							break;
						}
						break;
					}
					PubFunction.SetShopPoint(User.UserID, UserShopID, 1, MEMPointUpdate.sumPoint, remark, 2);
					MEMPointUpdate.sumPoint = 0;
				}
			}
		}
	}

	public static void PointRateUpdate(Chain.Model.Mem mem, int point, int pointType, int mymemID, Chain.Model.SysUser User, string OrderAccount)
	{
		if (MEMPointUpdate.NowRateLevel <= MEMPointUpdate.RateLevel && mem.MemRecommendID != 0)
		{
			Chain.Model.Mem TEMPMEM = MEMPointUpdate.bll.GetModel(mem.MemRecommendID);
			if (TEMPMEM != null)
			{
				int temp = 0;
				if (MEMPointUpdate.RateType)
				{
					temp = Convert.ToInt32(decimal.Truncate(point * (MEMPointUpdate.MEMRate[MEMPointUpdate.NowRateLevel - 1] / 100m)));
				}
				else
				{
					temp = Convert.ToInt32(MEMPointUpdate.MEMRate[MEMPointUpdate.NowRateLevel - 1]);
				}
				if (PubFunction.IsShopPoint(User.UserShopID, ref temp))
				{
					TEMPMEM.MemPoint += temp;
					int flag = MEMPointUpdate.bll.Update(TEMPMEM);
					MEMPointUpdate.sumPoint += temp;
					if (flag == 1)
					{
						PubFunction.UpdateMemLevel(TEMPMEM);
						Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();
						modelPoint.PointMemID = TEMPMEM.MemID;
						modelPoint.PointNumber = temp;
						modelPoint.PointChangeType = 9;
						modelPoint.PointShopID = TEMPMEM.MemShopID;
						modelPoint.PointCreateTime = DateTime.Now;
						modelPoint.PointUserID = TEMPMEM.MemUserID;
						modelPoint.PointOrderCode = MEMPointUpdate.PointOrderCode;
						modelPoint.PointGiveMemID = mymemID;
						switch (pointType)
						{
						case -1:
							modelPoint.PointRemark = string.Concat(new object[]
							{
								"卡号：",
								MEMPointUpdate.MEMCard,
								",姓名：",
								MEMPointUpdate.MEMName,
								"的会员商品消费退货，获得积分：[",
								temp,
								"]"
							});
							break;
						case 0:
						case 4:
							break;
						case 1:
							modelPoint.PointRemark = string.Concat(new object[]
							{
								"卡号：",
								MEMPointUpdate.MEMCard,
								",姓名：",
								MEMPointUpdate.MEMName,
								"的会员商品消费，获得积分：[",
								temp,
								"]"
							});
							break;
						case 2:
							modelPoint.PointRemark = string.Concat(new object[]
							{
								"卡号：",
								MEMPointUpdate.MEMCard,
								",姓名：",
								MEMPointUpdate.MEMName,
								"的会员快速消费，获得积分：[",
								temp,
								"]"
							});
							break;
						case 3:
							modelPoint.PointRemark = string.Concat(new object[]
							{
								"卡号：",
								MEMPointUpdate.MEMCard,
								",姓名：",
								MEMPointUpdate.MEMName,
								"的会员充次，获得积分：[",
								temp,
								"]"
							});
							break;
						case 5:
							modelPoint.PointRemark = string.Concat(new object[]
							{
								"卡号：",
								MEMPointUpdate.MEMCard,
								",姓名：",
								MEMPointUpdate.MEMName,
								"的会员注册，获得积分：[",
								temp,
								"]"
							});
							break;
						case 6:
							modelPoint.PointRemark = string.Concat(new object[]
							{
								"卡号：",
								MEMPointUpdate.MEMCard,
								",姓名：",
								MEMPointUpdate.MEMName,
								"的会员手动增加积分，获得积分：[",
								temp,
								"]"
							});
							break;
						default:
							switch (pointType)
							{
							case 14:
								modelPoint.PointRemark = string.Concat(new object[]
								{
									"卡号：",
									MEMPointUpdate.MEMCard,
									",姓名：",
									MEMPointUpdate.MEMName,
									"的会员充时增加积分，获得积分：[",
									temp,
									"]"
								});
								break;
							case 15:
								modelPoint.PointRemark = string.Concat(new object[]
								{
									"卡号：",
									MEMPointUpdate.MEMCard,
									",姓名：",
									MEMPointUpdate.MEMName,
									"的会员充值增加积分，获得积分：[",
									temp,
									"]"
								});
								break;
							}
							break;
						}
						Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
						bllPoint.Add(modelPoint);
						PubFunction.SaveSysLog(TEMPMEM.MemUserID, 3, "会员积分提成", "会员" + TEMPMEM.MemName + "积分提成成功", TEMPMEM.MemShopID, DateTime.Now, PubFunction.ipAdress);
					}
					MEMPointUpdate.NowRateLevel++;
					MEMPointUpdate.PointRateUpdate(TEMPMEM, point, pointType, mymemID, User, OrderAccount);
				}
			}
		}
	}

	public static DataTable GetSysGroupByID(int groupID)
	{
		Chain.BLL.SysGroup bllSysGroup = new Chain.BLL.SysGroup();
		DataTable dtSysGroup = bllSysGroup.GetSysGroupByID(groupID);
		DataTable result;
		if (dtSysGroup != null)
		{
			result = dtSysGroup;
		}
		else
		{
			result = null;
		}
		return result;
	}

	public static DataTable GetSysGroupByParentID(int groupID)
	{
		Chain.BLL.SysGroup bllSysGroup = new Chain.BLL.SysGroup();
		DataTable dtSysGroup = bllSysGroup.GetSysGroupByParentID(groupID);
		DataTable result;
		if (dtSysGroup != null)
		{
			result = dtSysGroup;
		}
		else
		{
			result = null;
		}
		return result;
	}
}
