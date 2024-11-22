using App.Application.Contracts.ServiceBus;
using App.Bus.Consumers;
using App.Domain.Const;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Domain.Options;
using MassTransit;

namespace App.Bus
{
	public static class BusExtension
	{
		public static void AddBusExt(this IServiceCollection services, IConfiguration configuration)
		{
			var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

			services.AddScoped<IServiceBus, ServiceBus>();
			services.AddMassTransit(x =>
			{
				x.AddConsumer<ProductAtEventConsumer>();
				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host(new Uri(serviceBusOption!.Url), h => { }); //27:48
					cfg.ReceiveEndpoint(ServiceBusConst.ProductAddedEventQueueName, e =>
					{
						e.ConfigureConsumer<ProductAtEventConsumer>(context);
					});
				});
			});
		}
	}
}
