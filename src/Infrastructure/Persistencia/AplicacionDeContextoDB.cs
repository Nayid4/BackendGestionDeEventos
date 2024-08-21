using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistencia
{
    public class AplicacionDeContextoDB : DbContext, IAplicacionContextoDb, IUnitOfWork
    {


        private readonly IPublisher _publisher;

        public AplicacionDeContextoDB(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacionContextoDb).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var eventosDeDominio = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var resultado = await base.SaveChangesAsync(cancellationToken);

            foreach (var evento in eventosDeDominio)
            {
                await _publisher.Publish(evento, cancellationToken);
            }

            return resultado;
        }
    }
}
