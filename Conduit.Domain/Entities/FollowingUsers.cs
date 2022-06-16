using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class FollowingUsers
    {
        public int UserId { get; set; }
        public int FollowingUserId { get; set; }
        public User User { get; set; }
        public User FollowingUser { get; set; }
    }
}
