using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vosplzen.sem2._2023.apiClient.Contracts
{
    public class FloodReport
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int StationId { get; set; }

        public DateTime TimeStamp { get; set; }

        public int Level { get; set; }  

    }
}
