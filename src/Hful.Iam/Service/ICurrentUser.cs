﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Service
{
    public interface ICurrentUser
    {
        Guid? Id { get; }

        string? UserName { get; }
    }
}
