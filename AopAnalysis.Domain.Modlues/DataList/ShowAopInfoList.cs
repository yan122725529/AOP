using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopAnalysis.Domain.Modlues
{
    public class ShowAopInfoList : BaseDataList<ShowAopInfo>
    {
        #region 处理单条记录

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowdata"></param>
        /// <returns></returns>
        public override bool InsertRow(ShowAopInfo rowdata)
        {
            return true;
        }

        public override bool DeletetRow(ShowAopInfo rowdata)
        {
            return true;
        }

        public override bool UpdateRow(ShowAopInfo rowdata)
        {
            return true;
        }

        public override int SelectRow(ShowAopInfo rowdata)
        {
            return 0;
        }

        #endregion

        #region 处理多条记录

        public override bool InsertMutiRow()
        {
            return true;
        }

        public override bool DeletetMutiRow()
        {
            return true;
        }

        public override bool UpdateMutiRow()
        {
            return true;
        }

        public override int SelectMutiRow()
        {
            return 0;
        }

        #endregion
    }
}