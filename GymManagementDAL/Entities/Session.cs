using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Session : BaseEntity
    {
        #region Properties
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region Realtionships

        #region Session - Category

        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; } = null!;

        #endregion

        #endregion
    }
}
