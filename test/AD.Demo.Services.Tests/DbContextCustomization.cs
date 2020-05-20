using System.Diagnostics.CodeAnalysis;
using AD.Demo.DataAccess;
using AutoFixture;
using Microsoft.EntityFrameworkCore;

namespace AD.Demo.Services.Tests
{
    [ExcludeFromCodeCoverage]
    public class DbContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var builder = new DbContextOptionsBuilder<TechTestContext>();
            builder.UseInMemoryDatabase("techTest");

            fixture.Inject(new TechTestContext(builder.Options));
        }
    }
}