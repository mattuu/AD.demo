﻿using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.Xunit2;

namespace AD.Demo.TestUtils
{
    [ExcludeFromCodeCoverage]
    public class WebApiAutoMoqDataAttribute : AutoDataAttribute
    {
        public WebApiAutoMoqDataAttribute()
            : base(() => new Fixture().Customize(new WebApiAutoMoqCustomization()))
        {
        }
    }
}