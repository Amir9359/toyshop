using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.InfraStructure
{
    public static class EnumDescription
    {
        public static IEnumerable<string> getDiscription(Type type)
        {
            var desc = new List<string>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

                foreach (DescriptionAttribute fd in fds)
                {
                    desc.Add(fd.Description);
                }
            }
            return desc;
        }
    }
}
