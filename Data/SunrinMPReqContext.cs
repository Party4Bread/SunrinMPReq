using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SunrinMPReq.Models;

namespace SunrinMPReq.Models
{
    public class SunrinMPReqContext : DbContext
    {
        public SunrinMPReqContext (DbContextOptions<SunrinMPReqContext> options)
            : base(options)
        {
        }

        public DbSet<SunrinMPReq.Models.RoomModel> RoomModel { get; set; }

        public DbSet<SunrinMPReq.Models.SongReqModel> SongReqModel { get; set; }
    }
}
