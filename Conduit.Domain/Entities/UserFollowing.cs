﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class UserFollowing
    {
        public int UserId { get; set; }
        public User Usesr { get; set; }
        public int FollowingId { get; set; }
        public User Followings { get; set; }
    }
}