using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopAnalysis.Domain.Modlues
{
   public abstract class  BaseDataList<T>
   {
       private List<T> CurList { get;set;}
  
       #region 处理单条记录
       /// <summary>
       /// 新增一条数据
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="rowdata"></param>
       /// <returns></returns>
       public abstract bool InsertRow(T rowdata);
       public abstract bool DeletetRow(T rowdata);
       public abstract bool UpdateRow(T rowdata);
       public abstract int SelectRow(T rowdata);

       #endregion

       #region 处理多条记录

       public abstract bool InsertMutiRow();
       public abstract bool DeletetMutiRow();
       public abstract bool UpdateMutiRow();
       public abstract int SelectMutiRow();
       #endregion
   } 
}
