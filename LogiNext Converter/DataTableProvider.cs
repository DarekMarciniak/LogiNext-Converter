using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LogiNext_Converter
{
    public class DataTableProvider
    {
        public DataTable SummaryDataTable { get { return dtSummary; } }
        private DataTable dtSummary;

        public DataTableProvider()
        {
            InitializeDataTableSummary();
        }

        private void InitializeDataTableSummary()
        {
            dtSummary = new DataTable();

            dtSummary.Columns.Add("Driver", typeof(string));
            dtSummary.Columns.Add("Cash", typeof(decimal));
            dtSummary.Columns.Add("Card", typeof(decimal));
            dtSummary.Columns.Add("Online", typeof(decimal));
            dtSummary.Columns.Add("COD Other", typeof(decimal));
            dtSummary.Columns.Add("Total", typeof(decimal));
            dtSummary.Columns.Add("Order Count", typeof(decimal));
            dtSummary.Columns.Add("ND - Order Count", typeof(decimal));
            dtSummary.Columns.Add("ND - Total", typeof(decimal));

        }
    }
}
