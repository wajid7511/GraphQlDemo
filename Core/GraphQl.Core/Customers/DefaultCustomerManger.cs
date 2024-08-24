using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Abstractions.Customers.Dtos;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQl.Mongo.Database.DALs;
using GraphQl.Mongo.Database.Models;
using GraphQlDemo.API.Models;
using GraphQlDemo.Shared.Messaging;

namespace GraphQl.Core
{
    public class DefaultCustomerManager(
        CustomerDAL customerDAL,
        ProductDAL productDAL,
        IMapper mapper,
        IDateTimeProvider dateTimeProvider,
        IMessageProducer messageProducer
        ) : ICustomerManager
    {
        private readonly CustomerDAL _customerDAL = customerDAL ?? throw new ArgumentNullException(nameof(customerDAL));
        private readonly ProductDAL _productDAL = productDAL ?? throw new ArgumentNullException(nameof(productDAL));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IDateTimeProvider _dateTimeProvider =
                dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        private readonly IMessageProducer _messageProducer =
                messageProducer ?? throw new ArgumentNullException(nameof(messageProducer));

        public async ValueTask<CustomerDto?> AddCustomerAsync(
            CustomerInput input,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(input);

            var customer = _mapper.Map<Customer>(input);
            var dbAddResult = await _customerDAL.CreateCustomerAsync(customer);

            if (dbAddResult.IsSuccess && dbAddResult.Entity is not null)
            {
                return _mapper.Map<CustomerDto>(dbAddResult.Entity);
            }

            return null;
        }

        public async ValueTask<CustomerOrderDto?> AddCustomerOrderAsync(
            CustomerOrderInput input,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(input);

            var customerOrder = new CustomerOrder
            {
                CustomerId = input.CustomerId,
                OrderDate = _dateTimeProvider.UtcNow,
                Items = await GetProductsDetailsAsync(input, cancellationToken)
            };
            if (customerOrder.Items.Count == 0)
            {
                return null;
            }

            var orderResult = await _customerDAL.CreateCustomerOrderAsync(customerOrder);

            if (orderResult.IsSuccess && orderResult.Entity != null)
            {
                _messageProducer.SendMessage(new MessageDto()
                {
                    ReferenceId = orderResult.Entity.Id.ToString(),
                    MessageType = MessageType.Order
                });
                return _mapper.Map<CustomerOrderDto>(orderResult.Entity);
            }

            return null;
        }

        private async Task<List<CustomerOrderItem>> GetProductsDetailsAsync(
            CustomerOrderInput input,
            CancellationToken cancellationToken
        )
        {
            var productIds = input.Products.Select(p => p.Id).ToList();

            var productResult = await _productDAL.GetProducts_Async(
                p => productIds.Contains(p.Id),
                int.MaxValue,
                0,
                true
            );

            if (!productResult.IsSuccess || productIds.Count != (productResult.Data?.Count ?? 0))
            {
                var exceptionMessage = productResult.Exception?.ToString() ?? "No Exception";
                Console.WriteLine($"Unable to get products: {exceptionMessage}");
                return [];
            }

            var products = productResult.Data ?? new List<Product>();

            var items = new List<CustomerOrderItem>();

            foreach (var productInput in input.Products ?? [])
            {
                var product = products.FirstOrDefault(p => p.Id == productInput.Id);
                if (product != null)
                {
                    var customerOrderItem = new CustomerOrderItem
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        ImageUrl = product.ImageUrl,
                        GroceryName = product.Grocery?.Name ?? string.Empty,
                        Price = product.Price,
                        Quantity = productInput.Quantity
                    };

                    items.Add(customerOrderItem);
                }
            }

            return items;
        }
    }
}
