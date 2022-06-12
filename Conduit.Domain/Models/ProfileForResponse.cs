﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Models
{
    public class ProfileForResponse
    {
        public string Username { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public bool Following { get; set; }
    }
}