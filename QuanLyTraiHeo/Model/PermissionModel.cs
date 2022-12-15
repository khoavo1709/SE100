using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTraiHeo.Model
{
    public partial class PermissionModel
    {
        public bool isSelected { get; set; }
       public string ActionDetail { get; set; }
        public int  number { get; set; }
       public PermissionModel()
        {

        }
       public PermissionModel(bool isSelected, string actionDetail, int number)
        {
            this.number = number;
            this.isSelected = isSelected;
            ActionDetail = actionDetail;
        }
    }
}
