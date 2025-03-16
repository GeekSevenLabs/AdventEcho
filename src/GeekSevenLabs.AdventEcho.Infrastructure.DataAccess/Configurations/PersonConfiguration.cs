using GeekSevenLabs.AdventEcho.Domain.Churches;
using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(person => person.Id);
        builder.Property(person => person.Id).ValueGeneratedOnAdd();
        
        builder.ComplexProperty(person => person.Name, nameBuilder =>
        {
            nameBuilder
                .Property(name => name.First)
                .HasMaxLength(100)
                .IsRequired();
            
            nameBuilder
                .Property(name => name.Last)
                .HasMaxLength(200)
                .IsRequired();
        });
        
        builder.ComplexProperty(person => person.Contact, contactBuilder =>
        {
            contactBuilder
                .Property(name => name.Email)
                .HasMaxLength(150)
                .IsRequired();
            
            contactBuilder
                .Property(name => name.Phone)
                .HasMaxLength(20)
                .IsRequired();
        });
        
        builder.ComplexProperty(person => person.Document, documentBuilder =>
        {
            documentBuilder.Property(document => document.Type).IsRequired();
            documentBuilder.Property(document => document.Number).HasMaxLength(20).IsRequired();
        });
        
        builder.HasIndex(person => person.Document.Number).IsUnique();
        
        builder.Property(person => person.ChurchId).IsRequired(false);
        
        builder
            .HasOne<Church>()
            .WithMany()
            .HasForeignKey(person => person.ChurchId);
    }
}