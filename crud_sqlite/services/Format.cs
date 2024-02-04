using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace crud_sqlite.services
{
    public class Format
    {
        public void FormatDataGrid(DataGrid dataGrid)
        {
            for (int i = 0; i < dataGrid.Columns.Count; i += 1)
            {
                dataGrid.Columns[i].Width = 90;
            }
        }
    }
}
