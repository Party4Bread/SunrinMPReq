using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SunrinMPReq.Models
{
    public class SongReqModel
    {
        public int Id { get; set; }
        public string RoomId { get; set; }
        public string SongName { get; set; }
        public string Artist { get; set; }
        public string Requester { get; set; }
        public string Link { get; set; }
    }
}
