using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Chain.Common.DEncrypt;

namespace Chain.BLL
{
    public class SysSerialNumber
    {
        private readonly Chain.IDAL.SysSerialNumber dal = new Chain.IDAL.SysSerialNumber();
        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
        {
            return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IsLock"></param>
        /// <returns></returns>
        public int Lock( int ID,int IsLock )
        {
            return dal.Update_Lock(ID, IsLock);
        }


        public int Card( int ID,int IsCard)
        {
            return dal.Update_Card(ID, IsCard);
        }


        public int MakeSN( int Number,int IsLock)
        {
            int flag = 0;
            for (int i = 0; i < Number; i++)
            {
                var uuid = Guid.NewGuid().ToString(); // 9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
                string sn = HashEncode.GetMD5(uuid).ToUpper();
                flag = dal.Insert_SN(sn, IsLock);
            }
            return flag;
        }


    }
}
