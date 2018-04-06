using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public static class Extensions
    {
        public static DbContextOptionsBuilder UseEF6CompatibleValueGeneration(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IValueGenerationManager, EF6CompatibleValueGeneratorManager>();
            return optionsBuilder;
        }
    }
}

namespace Microsoft.EntityFrameworkCore.ChangeTracking.Internal
{
    public class EF6CompatibleValueGeneratorManager : ValueGenerationManager
    {
        private readonly IValueGeneratorSelector valueGeneratorSelector;
        private readonly IKeyPropagator keyPropagator;

        public EF6CompatibleValueGeneratorManager(IValueGeneratorSelector valueGeneratorSelector, IKeyPropagator keyPropagator)
            : base(valueGeneratorSelector, keyPropagator)
        {
            this.valueGeneratorSelector = valueGeneratorSelector;
            this.keyPropagator = keyPropagator;
        }

        public override InternalEntityEntry Propagate(InternalEntityEntry entry)
        {
            InternalEntityEntry chosenPrincipal = null;
            foreach (var property in FindPropagatingProperties(entry))
            {
                var principalEntry = keyPropagator.PropagateValue(entry, property);
                if (chosenPrincipal == null)
                    chosenPrincipal = principalEntry;
            }
            return chosenPrincipal;
        }

        public override void Generate(InternalEntityEntry entry)
        {
            var entityEntry = new EntityEntry(entry);
            foreach (var property in FindGeneratingProperties(entry))
            {
                var valueGenerator = GetValueGenerator(entry, property);
                SetGeneratedValue(entry, property, valueGenerator.Next(entityEntry), valueGenerator.GeneratesTemporaryValues);
            }
        }

        public override async Task GenerateAsync(InternalEntityEntry entry, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entityEntry = new EntityEntry(entry);
            foreach (var property in FindGeneratingProperties(entry))
            {
                var valueGenerator = GetValueGenerator(entry, property);
                SetGeneratedValue(entry, property, await valueGenerator.NextAsync(entityEntry, cancellationToken), valueGenerator.GeneratesTemporaryValues);
            }
        }

        static IEnumerable<IProperty> FindPropagatingProperties(InternalEntityEntry entry)
        {
            return entry.EntityType.GetProperties().Where(property => property.IsForeignKey());
        }

        static IEnumerable<IProperty> FindGeneratingProperties(InternalEntityEntry entry)
        {
            return entry.EntityType.GetProperties().Where(property => property.RequiresValueGenerator());
        }

        ValueGenerator GetValueGenerator(InternalEntityEntry entry, IProperty property)
        {
            return valueGeneratorSelector.Select(property, property.IsKey() ? property.DeclaringEntityType : entry.EntityType);
        }

        static void SetGeneratedValue(InternalEntityEntry entry, IProperty property, object generatedValue, bool isTemporary)
        {
            if (generatedValue == null) return;
            entry[property] = generatedValue;
            if (isTemporary)
                entry.MarkAsTemporary(property, true);
        }
    }
}