using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using InventoryService.Events;
using InventoryService.Services;
using InventoryService.Models;

namespace InventoryService.Consumers
{
    public class TicketBookingEventConsumer : IConsumer<TicketBookingEvent>
    {
        public readonly IInventoryInterface _iInventoryInterface;
        public TicketBookingEventConsumer(IInventoryInterface iInventoryInterface)
        {
            _iInventoryInterface = iInventoryInterface;
        }
        public Task Consume(ConsumeContext<TicketBookingEvent> context)
        {
            try
            {
                Inventory inventory = _iInventoryInterface.ShowInventories().Where(a => a.FromPlace.ToLower() == context.Message.FromPlace.ToLower()
                                                                                        && a.ToPlace.ToLower() == context.Message.ToPlace.ToLower()
                                                                                        && a.FlightNumber == context.Message.FlightNumber)
                                                                            .FirstOrDefault();
                if (context.Message.SeatType == 1)//Business class
                {
                    inventory.BClassCount = inventory.BClassCount - context.Message.NumberOfTickets;
                    inventory.ModifiedBy = 1;
                    inventory.ModifiedOn = DateTime.Now;
                }
                else if (context.Message.SeatType == 0)//Non business class
                {
                    inventory.NBClassCount = inventory.NBClassCount - context.Message.NumberOfTickets;
                    inventory.ModifiedBy = 1;
                    inventory.ModifiedOn = DateTime.Now;
                }
                _iInventoryInterface.EditInventory(inventory);
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}
