using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2007_AS2.Models
{
    public interface IPositionsRepository
    {
        IQueryable<Position> Positions { get; }
        Position Save(Position position);
        void Delete(Position position);
    }
}
