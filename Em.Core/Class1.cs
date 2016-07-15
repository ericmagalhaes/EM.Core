using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization.Configuration;

namespace Em.Core
{
    public interface IBus
    {
        
    }

    public interface IEventStore
    {
        
    }
    public abstract class Saga
    {
        
    }

    public class Message
    {
        public Guid RequestId { get; set; }
        public int SagaId { get; set; }
        public string Name { get; set; }
    }

    public class Command : Message
    {
        
    }

    public class Activity : Message
    {
        
    }
   
    
    public class RequestBooking : Command
    {
        
    }

    public class CancelBooking : Command
    {
        
    }
    public class BookingAssignment : DomainEvent
    {
        private string UserName { get; set; }
        public BookingAssignment(string userName)
        {
            UserName = userName;
        }
    }
    public class BookingSaga : Saga,
        IHandle<RequestBooking,Booking>{
        public Booking Handle(RequestBooking t)
        {
            throw new NotImplementedException();
        }
}

    public class DomainEvent
    {
        public DateTime TimeStamp { get; private set; }
        
        public DomainEvent()
        {
            TimeStamp = DateTime.Now;
        }
    }
   
    public abstract class Aggregate
    {
        public Guid Id { get; protected set; }
        public bool HasPendingChanges { get; set; }
        private readonly Queue<DomainEvent> _uncommittedEvents = new Queue<DomainEvent>();
        public IEnumerable<DomainEvent> GetUncommitedEvents()
        {
            return _uncommittedEvents;
        }
        /// <summary>
        /// Events not stored in eventsource
        /// </summary>
        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public void RaiseEvent(DomainEvent domainEvent)
        {
            _uncommittedEvents.Enqueue(domainEvent);
        }
     
       
    }

    public class BookingRepository :
        IRepositoryCommand<BookingRequested>
    {
        
    }

    public interface IRepositoryCommand <T>
    {
        void Handle(T t)
    }

    public class Booking : Aggregate
       
    {
    
        public Booking()
        {
            Id = Guid.NewGuid();
        }

        public static Booking Factory(RequestBooking requestbooking)
        {
            var bookingRequest = new BookingRequested(requestbooking);
            var booking = new Booking();
            booking.RaiseEvent(bookingRequest);
            return booking;
        }


    }

    public interface IHandle    <T, T1>
    {
        T1 Handle(T t);
    }

    public class BookingCancellation : DomainEvent
    {
        
    }
    public class BookingRequested :DomainEvent
    {
        public BookingRequested(RequestBooking message)
        {
            
        }
    }
}
