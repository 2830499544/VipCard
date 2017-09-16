using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class editMemberData : Page
	{
		protected HtmlInputText memname;

		protected HtmlAnchor boy;

		protected HtmlAnchor girl;

		protected HtmlImage imgShow;

		protected HtmlInputText mobile;

		protected HtmlInputText identityCard;

		protected HtmlGenericControl birthday;

		protected HtmlGenericControl address;

		protected HtmlSelect sltProvince;

		protected HtmlSelect sltCity;

		protected HtmlSelect sltCounty;

		protected HtmlSelect sltVillage;

		protected HtmlInputText detailAddress;

		protected HtmlInputText email;

		protected HtmlInputHidden txtMemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.birthday.InnerHtml = "1990-01-01";
					this.BindMemInfo(MemID);
					this.txtMemID.Value = MemID.ToString();
					if (base.Request.QueryString["imageurl"] != null)
					{
						this.imgShow.Src = base.Request.QueryString["imageurl"];
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
			this.memname.Value = modelMem.MemName;
			this.mobile.Value = modelMem.MemMobile;
			this.boy.Attributes.Remove("class");
			this.girl.Attributes.Remove("class");
			if (modelMem.MemSex)
			{
				this.boy.Attributes.Add("class", "line_btn active");
				this.girl.Attributes.Add("class", "line_btn");
			}
			else
			{
				this.girl.Attributes.Add("class", "line_btn active");
				this.boy.Attributes.Add("class", "line_btn");
			}
			if (modelMem.MemPhoto != null && modelMem.MemPhoto != "")
			{
				this.imgShow.Src = modelMem.MemPhoto.ToString();
			}
			else
			{
				this.imgShow.Src = "images/headimg.jpg";
			}
			string identity = modelMem.MemIdentityCard;
			this.identityCard.Value = modelMem.MemIdentityCard;
			this.email.Value = modelMem.MemEmail;
			string birth = "";
			if (identity.Length == 18)
			{
				birth = string.Concat(new string[]
				{
					identity.Substring(6, 4),
					"-",
					identity.Substring(10, 2),
					"-",
					identity.Substring(12, 2)
				});
			}
			this.birthday.InnerHtml = birth;
			PubFunction.BindSysAreaMobile(this.sltProvince, 0);
			if (modelMem.MemProvince != "")
			{
				this.sltProvince.Value = modelMem.MemProvince;
				if (this.sltProvince.Value != "" && this.sltProvince.Value != "请选择")
				{
					PubFunction.BindSysAreaMobile(this.sltCity, int.Parse(this.sltProvince.Value));
				}
				if (modelMem.MemCity != "")
				{
					this.sltCity.Value = modelMem.MemCity;
				}
				if (this.sltCity.Value != "" && this.sltCity.Value != "请选择")
				{
					PubFunction.BindSysAreaMobile(this.sltCounty, int.Parse(this.sltCity.Value));
				}
				if (modelMem.MemCounty != "")
				{
					this.sltCounty.Value = modelMem.MemCounty;
				}
				if (this.sltCounty.Value != "" && this.sltCounty.Value != "请选择")
				{
					PubFunction.BindSysAreaMobile(this.sltVillage, int.Parse(this.sltCounty.Value));
				}
				if (modelMem.MemVillage != "")
				{
					this.sltVillage.Value = modelMem.MemVillage;
				}
			}
			string memaddress = this.GetMemAddress(modelMem.MemProvince, modelMem.MemCity, modelMem.MemCounty, modelMem.MemVillage, modelMem.MemAddress);
			if (memaddress.Length > 14)
			{
				this.address.InnerHtml = memaddress.Substring(0, 14) + "...";
			}
			else
			{
				this.address.InnerHtml = memaddress;
			}
			this.boy.Attributes.Remove("class");
			this.boy.Attributes.Remove("girl");
			if (modelMem.MemSex)
			{
				this.boy.Attributes.Add("class", "line_btn active");
				this.girl.Attributes.Add("class", "line_btn");
			}
			else
			{
				this.girl.Attributes.Add("class", "line_btn active");
				this.boy.Attributes.Add("class", "line_btn");
			}
			this.detailAddress.Value = modelMem.MemAddress;
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
