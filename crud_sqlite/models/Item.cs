using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_sqlite.models
{
    public class Item
    {
        public int Id { get; set; }

        public int Measure_Id { get; set; }

        public int Category_Id { get; set; }

        public String Description { get; set; }

        public String Brand { get; set; }
    }
}
