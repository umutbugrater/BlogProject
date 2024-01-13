﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string ImageUrl { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Message2> MessageSender { get; set; }
        public List<Message2> MessageReceiver { get; set; }
    }
}
