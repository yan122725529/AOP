using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AopAnalysis.Domain.Modlues
{
    public class ShowAopInfo
    {
        private decimal _pr;
        private double _qty;
        private int _dateid;
        private string _codename;

        public decimal Pr
        {
            get { return _pr; }
            set { _pr = value; }
        }

        public double Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        public int Dateid
        {
            get { return _dateid; }
            set { _dateid = value; }
        }

        public string Codename
        {
            get { return _codename; }
            set { _codename = value; }
        }
    }

}

