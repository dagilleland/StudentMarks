using System;

namespace Common.Infrastructure.CQRS.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
