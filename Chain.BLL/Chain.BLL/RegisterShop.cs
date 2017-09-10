using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Chain.BLL
{
    public class RegisterShop
    {
        private readonly Chain.IDAL.SysSerialNumber dal_SerialNumber = new Chain.IDAL.SysSerialNumber();
        private readonly Chain.IDAL.SysShop dal_SysShop = new Chain.IDAL.SysShop();


        public RegisterShop()
        {

        }

        /// <summary>
        /// 序列号是否存在
        /// </summary>
        /// <param name="SerialNumber">序列号</param>
        /// <returns>1:(可以使用)2:（已经使用）3:（锁定）4:（锁定）5:(不存在)</returns>
        public int SerianNumberInfo(string SerialNumber)
        {
            int flag = 1;
            string strSql = string.Format("SerialNumber='{0}'", SerialNumber);
            DataTable table = dal_SerialNumber.GetList(strSql).Tables[0];
            if (table.Rows.Count > 0)
            {
                bool isUse = Convert.ToBoolean(table.Rows[0]["IsUse"]);
                bool isLock = Convert.ToBoolean(table.Rows[0]["IsLock"]);
                bool isCard = Convert.ToBoolean(table.Rows[0]["IsCard"]);
                if (isUse)
                    flag = 2;
                else if (isLock)
                    flag = 3;
                else if (!isCard)
                    flag = 4;
            }
            else
                flag =5;
            table.Clear();
            table.Dispose();
            return flag;
        }

        /// <summary>
        /// 注册使用
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="ShopID"></param>
        /// <returns></returns>
        public bool Register(string SN, int ShopID)
        {
            return dal_SerialNumber.Update_Register(SN, ShopID)==1?true:false;
        }

        /// <summary>
        /// 企业代码是否存在
        /// </summary>
        /// <param name="ShopCode"></param>
        /// <returns></returns>
        public bool ShopCodeExists( string ShopCode)
        {
            return dal_SysShop.Exists_ShopContactMan(ShopCode);
        }


       
    }
}
