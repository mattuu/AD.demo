using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace AD.Demo.Services.Tests
{
    [ExcludeFromCodeCoverage]
    public class DbContextAutoMoqAutoDataAttribute : AutoDataAttribute
    {
        public DbContextAutoMoqAutoDataAttribute()
            : base(CreateFixture)
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new DbContextCustomization())
                .Customize(new AutoMoqCustomization())
                .Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}