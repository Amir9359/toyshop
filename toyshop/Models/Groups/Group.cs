using System;
using toysite.Models;

namespace toyshop.Models.Groups
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SecondTitle { get; set; }
        public User Creator { get; set; }

    }
}