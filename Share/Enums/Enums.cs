using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enums
{
    public enum BlogStatus
    {
        Published,
        Draft,
        Pending,
        Scheduled,
        Archived,
        Deleted,
        Private
    }
    public enum _Justify
    {
        start,
        center,
        end

    }
    public enum Stye
    {
        normal,
        italic,
        bold
    }
    public enum _ContentType
    {
        text,
        image,
        code
    }
}
