﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Zeus.Demo.EntityFrameworkCore
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }
    }
}
