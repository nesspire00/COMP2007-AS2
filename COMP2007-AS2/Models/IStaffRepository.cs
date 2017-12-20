using COMP2007_AS2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2007_AS2.Models
{
    public interface IStaffRepository
    {
        IQueryable<Staff> Staffs { get; }
        IEnumerable Positions { get; }

        Staff Save(Staff staff);
        void Delete(Staff staff);
    }
}
