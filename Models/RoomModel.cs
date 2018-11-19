using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SunrinMPReq.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomId { get; set; }
        public string RoomKey { get; set; }
    }
}
