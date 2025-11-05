using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Common.Mediator
{
    public class SimpleMediator :ICommandBus
    {
        private readonly IServiceProvider _provider;

        public SimpleMediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            dynamic handler = _provider.GetRequiredService(handlerType);
            return handler.Handle((dynamic)command, cancellationToken);
        }
    }

}
