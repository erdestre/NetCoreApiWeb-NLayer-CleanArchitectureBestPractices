using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Contracts.ServiceBus;
using App.Domain.Events;
using MassTransit;

namespace App.Bus
{
	public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
	{
		public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEventOrMessage
		{
				await publishEndpoint.Publish(@event, cancellationToken);
		}

		public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage
		{
			var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}")); //masstransit template

			await endpoint.Send(message, cancellationToken);
		}
	}
}
