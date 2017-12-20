using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace COMP2007_AS2.Models
{
    public class EFPositionsRepository : IPositionsRepository
    {
        EventStaffModel db = new EventStaffModel();

        public IQueryable<Position> Positions { get { return db.Positions; } }

        public void Delete(Position position)
        {
            db.Positions.Remove(position);
            db.SaveChanges();
        }

        public Position Save(Position position)
        {
            if (position.positionId == 0)
            {
                db.Positions.Add(position);
            }
            else
            {
                db.Entry(position).State = EntityState.Modified;
            }
            db.SaveChanges();

            return position;
        }
    }
}