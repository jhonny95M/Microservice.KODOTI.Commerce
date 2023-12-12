using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.EventHandlers.Commands
{
    public record ClientCreateCommand(string name):INotification;
}
