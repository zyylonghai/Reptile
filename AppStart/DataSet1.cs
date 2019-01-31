using System.Data;

namespace AppStart
{


    partial class DataSet1
    {
        partial class TastMastDataTable
        {
            //public TastMastDataTable()
            //{

            //}
            public void FillData(DataTable dt)
            {
                if (this.Rows != null && this.Rows.Count > 0)
                    this.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newrow = this.NewRow();
                    foreach (DataColumn col in this.Columns)
                    {
                        newrow[col] = row[col.ColumnName];
                    }
                    this.Rows.Add(newrow);
                }
            }
        }
    }
}
