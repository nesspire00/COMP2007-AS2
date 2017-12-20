using COMP2007_AS2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace COMP2007_AS2.Models
{
    public class EFStaffRepository : IStaffRepository
    {
        EventStaffModel db = new EventStaffModel();

        public IQueryable<Staff> Staffs { get { return db.Staffs; } }

        public IEnumerable Positions { get => db.Positions; }

        public void Delete(Staff staff)
        {
            db.Staffs.Remove(staff);
            db.SaveChanges();
        }

        public Staff Save(Staff staff)
        {
            if (staff.staffId == 0)
            {
                db.Staffs.Add(staff);
            }
            else
            {
                db.Entry(staff).State = EntityState.Modified;
            }
            db.SaveChanges();

            return staff;
        }
    }
}