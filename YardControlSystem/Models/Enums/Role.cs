using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models.Enums
{
    public enum Role
    {
        Manager,
        [Description("Vairuotojas")]
        Driver,
        Storekeeper
    }
}
