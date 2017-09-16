using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class memAddressInfo : Page
	{
		protected HtmlInputText memname;

		protected HtmlInputText mobile;

		protected HtmlGenericControl address;

		protected HtmlSelect sltProvince;

		protected HtmlSelect sltCity;

		protected HtmlSelect sltCounty;

		protected HtmlSelect sltVillage;

		protected HtmlInputText detailAddress;

		protected HtmlAnchor yes;

		protected HtmlAnchor no;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtAddressID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindSysAreaMobile(this.sltProvince, 0);
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.BindMemInfo(MemID);
					this.txtMemID.Value = MemID.ToString();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.BLL.MemAddress bllAddress = new Chain.BLL.MemAddress();
			if (base.Request.QueryString["AddressID"] != null)
			{
				int AddressID = int.Parse(base.Request.QueryString["AddressID"]);
				DataTable dt = bllAddress.GetList(string.Concat(new object[]
				{
					" MemID=",
					MemID,
					" and ID=",
					AddressID
				})).Tables[0];
				if (dt.Rows.Count > 0)
				{
					this.txtAddressID.Value = AddressID.ToString();
					Chain.Model.MemAddress modelAddress = bllAddress.GetModel(AddressID);
					this.memname.Value = modelAddress.MemName;
					this.mobile.Value = modelAddress.MemMobile;
					this.detailAddress.Value = modelAddress.MemDetailAddress;
					string memaddress = this.GetMemAddress(modelAddress.MemProvince, modelAddress.MemCity, modelAddress.MemCounty, modelAddress.MemVillage, modelAddress.MemDetailAddress);
					if (memaddress.Length > 14)
					{
						this.address.InnerHtml = memaddress.Substring(0, 14) + "...";
					}
					else
					{
						this.address.InnerHtml = memaddress;
					}
					int IsDefault = int.Parse(dt.Rows[0]["IsDefault"].ToString());
					this.yes.Attributes.Remove("class");
					this.no.Attributes.Remove("class");
					if (IsDefault == 1)
					{
						this.yes.Attributes.Add("class", "line_btn active");
						this.no.Attributes.Add("class", "line_btn");
					}
					else
					{
						this.no.Attributes.Add("class", "line_btn active");
						this.yes.Attributes.Add("class", "line_btn");
					}
					PubFunction.BindSysAreaMobile(this.sltProvince, 0);
					if (modelAddress.MemProvince != "")
					{
						this.sltProvince.Value = modelAddress.MemProvince;
						if (this.sltProvince.Value != "" && this.sltProvince.Value != "请选择")
						{
							PubFunction.BindSysAreaMobile(this.sltCity, int.Parse(this.sltProvince.Value));
						}
						if (modelAddress.MemCity != "")
						{
							this.sltCity.Value = modelAddress.MemCity;
						}
						if (this.sltCity.Value != "" && this.sltCity.Value != "请选择")
						{
							PubFunction.BindSysAreaMobile(this.sltCounty, int.Parse(this.sltCity.Value));
						}
						if (modelAddress.MemCounty != "")
						{
							this.sltCounty.Value = modelAddress.MemCounty;
						}
						if (this.sltCounty.Value != "" && this.sltCounty.Value != "请选择")
						{
							PubFunction.BindSysAreaMobile(this.sltVillage, int.Parse(this.sltCounty.Value));
						}
						if (modelAddress.MemVillage != "")
						{
							this.sltVillage.Value = modelAddress.MemVillage;
						}
					}
				}
			}
		}

		public string GetMemAddress(string province, string city, string county, string village, string address)
		{
			Chain.BLL.SysArea bllArea = new Chain.BLL.SysArea();
			string result = "";
			if (province != null && city != null && county != null && village != null)
			{
				if (province != "" && province != "请选择" && city != "" && city != "请选择" && county != "" && county != "请选择" && village != "" && village != "请选择" && address != "")
				{
					result = string.Concat(new string[]
					{
						bllArea.GetNameByID(int.Parse(province.ToString())),
						"省",
						bllArea.GetNameByID(int.Parse(city.ToString())),
						"市",
						bllArea.GetNameByID(int.Parse(county.ToString())),
						bllArea.GetNameByID(int.Parse(village.ToString())),
						address.ToString()
					});
				}
			}
			return result;
		}
	}
}
