using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumers
{
	public class ProductAtEventConsumer : IConsumer<ProductAddedEvent>
	{
		public Task Consume(ConsumeContext<ProductAddedEvent> context)
		{
			var product = context.Message;
			Console.WriteLine($"Product added: {product.Name} with price {product.Price}");
			return Task.CompletedTask;
		}
	}
}
