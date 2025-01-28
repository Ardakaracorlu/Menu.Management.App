using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Management.App.Model.Response
{
    public class GetStartedStatusResponse
    {
        public bool Company { get; set; }
        public bool Restaurant { get; set; }
        public bool Menu { get; set; }
        public bool Qr { get; set; }
        public int Value { get; set; }
    }
}
