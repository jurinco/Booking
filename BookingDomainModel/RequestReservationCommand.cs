﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class RequestReservationCommand : IMessage
    {
        private readonly DateTime date;
        private readonly string email;
        private readonly string name;
        private readonly int quantity;
        private readonly Guid id;

        public RequestReservationCommand(DateTime date, string email, string name, int quantity)
        {
            this.date = date;
            this.email = email;
            this.name = name;
            this.quantity = quantity;
            this.id = Guid.NewGuid();
        }

        protected RequestReservationCommand(dynamic source)
        {
            this.date = source.Date;
            this.email = source.Email;
            this.name = source.Name;
            this.quantity = source.Quantity;
            this.id = source.Id;
        }

        public Envelope Envelop()
        {
            return new Envelope(this, "1");
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public string Email
        {
            get { return this.email; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Quantity
        {
            get { return this.quantity; }
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public class Quickening : IQuickening
        {
            public IEnumerable<IMessage> Quicken(dynamic envelope)
            {
                if (envelope.BodyType != typeof(RequestReservationCommand).Name.ToLowerInvariant())
                {
                    yield break;
                }
                yield return new RequestReservationCommand(envelope.Body);
            }
        }
    }
}